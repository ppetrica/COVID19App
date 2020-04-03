using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database
{
    /// <summary>
    /// SqlDataBaseManager has the role of ensuring the connection to the local .db database.
    /// It has the CRUD functionality.
    /// In order to use it, first set the databaseConnection.
    /// </summary>
    public class SqlDbManager : IDbManager
    {
        private SQLiteConnection _dbConnection;
        private SQLiteDataReader _dataReader;
        /// <summary>
        /// _dbConnection is the SQLiteConnection parameter for the database specified
        /// _dataReader is the SQLiteDataReader parameter which holds the result of a query
        /// </summary>
        public SqlDbManager()
        {
            _dbConnection = new SQLiteConnection();
        }

        public void SetDatabaseConnection(string databasePath)
        {
            //databasePath = @"..\..\..\resources\sql\covid.db";
            string connString = $"Data Source={databasePath}";
            _dbConnection = new SQLiteConnection(connString);
        }

        private void ExecuteQuery(String query)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(query, _dbConnection))
            {
                _dbConnection.Open();
                _dataReader = cmd.ExecuteReader();
                _dbConnection.Close();
            }
        }

        private void ExecuteNonQuery(String query)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(query, _dbConnection))
            {
                _dbConnection.Open();
                cmd.ExecuteNonQuery();
                _dataReader = null;
                _dbConnection.Close();
            }
        }

        public bool InsertCountry(string name, ushort code, string alpha, byte regionId)
        {
            String sql = $"INSERT INTO country (name, code, alpha, region_id) VALUES ('{name}', {code}, '{alpha}', {region_id});";
            
            try
            {
                ExecuteNonQuery(sql);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool InsertRegion(byte regionId, string regionName)
        {
            String sql = $"INSERT INTO region (region_id, region_name) VALUES ({regionId}, '{regionName}');";
            try
            {
                ExecuteNonQuery(sql);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool InsertDayInfo(string updateDate, int confirmed, int deaths, int recovered, int code)
        {
            String sql = $"INSERT INTO dayinfo (update_date, confirmed, deaths, recovered, code) VALUES (TO_DATE('{updateDate}', 'YYYY-MM-DD'), {confirmed}, {deaths}, {recovered}, {code});";
            try
            {
                ExecuteNonQuery(sql);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
