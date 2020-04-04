using System;
using System.Collections.Generic;
using System.Data;
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
            string connString = $"Data Source={databasePath}";
            _dbConnection = new SQLiteConnection(connString);
        }

        private void ExecuteQuery(String query)
        {
            if (_dbConnection.State == ConnectionState.Open)
            {
                _dbConnection.Close();
            }
            _dbConnection.Open();
            using (SQLiteCommand cmd = new SQLiteCommand(query, _dbConnection))
            {
                _dataReader = cmd.ExecuteReader();
            }
        }

        private void ExecuteNonQuery(String query)
        {
            if (_dbConnection.State == ConnectionState.Open)
            {
                _dbConnection.Close();
            }
            _dbConnection.Open();

            using (SQLiteCommand cmd = new SQLiteCommand(query, _dbConnection))
            {
                
                cmd.ExecuteNonQuery();
                _dbConnection.Close();
            }
        }

        public bool ClearTable(string tableName)
        {
            String sql = $"DELETE FROM {tableName};";

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

        public bool InsertCountry(string name, ushort code, string alpha, byte regionId)
        {
            String sql = $"INSERT INTO country (name, code, alpha, region_id) VALUES ('{name}', {code}, '{alpha}', {regionId});";
            
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
            String sql = $"INSERT INTO dayinfo (update_date, confirmed, deaths, recovered, code) VALUES ('{updateDate}', {confirmed}, {deaths}, {recovered}, {code});";
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

        public string GetRegionNameById(int id)
        {
            String sql = $"SELECT region_name FROM region WHERE region_id={id};";
            try
            {
                ExecuteQuery(sql);
                if (_dataReader.Read())
                {
                    var temp =  _dataReader.GetString(0);
                    _dataReader.Close();    //the most import line of code
                    _dbConnection.Close();
                    return temp;
                }
                _dbConnection.Close();
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int GetCountryIdByName(string countryName)
        {
            String sql = $"SELECT code FROM country WHERE name='{countryName}'";
            try
            {
                ExecuteQuery(sql);
                if (_dataReader.Read())
                {
                    var temp = _dataReader.GetInt32(0);
                    _dataReader.Close();    //the most import line of code
                    _dbConnection.Close();
                    return temp;
                }
                _dbConnection.Close();
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public Tuple<string, string, int> GetCountryInfoById(int countryId)
        {
            String sql = $"SELECT name, alpha, region_id FROM country WHERE code={countryId};";
            try
            {
                ExecuteQuery(sql);
                if (_dataReader.Read())
                {
                    var temp = new Tuple<string, string, int>(_dataReader.GetString(0), _dataReader.GetString(1), _dataReader.GetInt32(2));
                    _dbConnection.Close();
                    return temp;
                }
                _dbConnection.Close();
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Tuple<string, int, int, int>> GetCovidInfoByCountryId(int countryId)
        {
            var dayListCovidInfo = new List<Tuple<string, int, int, int>>();
            String sql = $"SELECT update_date, confirmed, deaths, recovered FROM dayinfo WHERE code={countryId};";
            try
            {
                ExecuteQuery(sql);
                while (_dataReader.Read())
                {
                    dayListCovidInfo.Add(Tuple.Create(_dataReader.GetString(0), _dataReader.GetInt32(1), _dataReader.GetInt32(2), _dataReader.GetInt32(3)));
                }
                _dataReader.Close();
                _dbConnection.Close();
                return dayListCovidInfo;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<int> GetCountriesId()
        {
            var dayListCovidInfo = new List<int>();
            String sql = $"SELECT DISTINCT code FROM dayinfo;";
            try
            {
                ExecuteQuery(sql);
                while (_dataReader.Read())
                {
                    dayListCovidInfo.Add(_dataReader.GetInt32(0));
                }
                _dataReader.Close();
                _dbConnection.Close();
                return dayListCovidInfo;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
