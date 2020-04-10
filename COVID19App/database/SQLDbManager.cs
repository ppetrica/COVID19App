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

        /// <param name="databasePath">Database Path</param>
        public void SetDatabaseConnection(string databasePath)
        {
            string connString = $"Data Source={databasePath}";
            _dbConnection = new SQLiteConnection(connString);
        }

        /// <summary>
        /// Executing a query which is expected to return a result.
        /// It is recommended to be used when getting the data from the database.
        /// </summary>
        /// <param name="query">Query to be executed</param>
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

        /// <summary>
        /// Executing a query which is expected to NOT return a result.
        /// It is recommended to be used when deleting, inserting or updating.
        /// </summary>
        /// <param name="query"></param>
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

        /// <summary>
        /// Clear data from the table from the database.
        /// </summary>
        /// <param name="tableName">Table Name to be cleared</param>
        /// <returns>True if the table is cleared successfully else return False</returns>
        public bool ClearTable(string tableName)
        {
            var sql = $"DELETE FROM {tableName};";
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

        /// <summary>
        /// Inserting a country item in the country Table.
        /// </summary>
        /// <param name="name">Name of the country</param>
        /// <param name="code">Numerical UNIQUE code of the country</param>
        /// <param name="alpha">The alphanumeric code of the country</param>
        /// <param name="regionId">The region id of the continent where this country is located</param>
        /// <returns>True if the insertion wa successful else return False</returns>
        public bool InsertCountry(string name, ushort code, string alpha, byte regionId)
        {
            var sql = $"INSERT INTO country (name, code, alpha, region_id) VALUES ('{name}', {code}, '{alpha}', {regionId});";
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

        /// <summary>
        /// Inserting a continent in the region table from the database.
        /// </summary>
        /// <param name="regionId">The UNIQUE region id of the continent</param>
        /// <param name="regionName">The continent name</param>
        /// <returns>True if the insertion was successful else return False</returns>
        public bool InsertRegion(byte regionId, string regionName)
        {
            var sql = $"INSERT INTO region (region_id, region_name) VALUES ({regionId}, '{regionName}');";
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

        /// <summary>
        /// Inserting a Day Info about COVID-19 in thedayinfo table from the database.
        /// </summary>
        /// <param name="updateDate">The date in the format "YYYY-MM-DD"</param>
        /// <param name="confirmed">The number of confirmed cases of COVID-19</param>
        /// <param name="deaths">The number of deaths cases of COVID-19</param>
        /// <param name="recovered">The number of recovered cases of COVID-19</param>
        /// <param name="code">The code of the country</param>
        /// <returns>True if the insertion was successful else return False</returns>
        public bool InsertDayInfo(string updateDate, int confirmed, int deaths, int recovered, int code)
        {
            var sql = $"INSERT INTO dayinfo (update_date, confirmed, deaths, recovered, code) VALUES ('{updateDate}', {confirmed}, {deaths}, {recovered}, {code});";
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

        /// <param name="id">The id of the country</param>
        /// <returns>The country name of the specified id if it exists in the database else returns NULL</returns>
        public string GetRegionNameById(int id)
        {
            var sql = $"SELECT region_name FROM region WHERE region_id={id};";
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

        /// <summary>
        /// Getting the id of the country  from the country table in the database
        /// </summary>
        /// <param name="countryName">The country name</param>
        /// <returns>The id of the country if it exists in the database else returns 0</returns>
        public int GetCountryIdByName(string countryName)
        {
            var sql = $"SELECT code FROM country WHERE name='{countryName}'";
            try
            {
                ExecuteQuery(sql);
                if (_dataReader.Read())
                {
                    var temp = _dataReader.GetInt32(0);
                    _dataReader.Close();    //the most important line of code
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

        /// <summary>
        /// Getting the country info from the country table in the database
        /// </summary>
        /// <param name="countryId">The country Id</param>
        /// <returns>A tuple which hold the name, the alphanumeric code of the country and the id of the continent where this country is located</returns>
        public Tuple<string, string, int> GetCountryInfoById(int countryId)
        {
            var sql = $"SELECT name, alpha, region_id FROM country WHERE code={countryId};";
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

        /// <summary>
        /// Getting the information of COVID-19 from the dayinfo table in the database
        /// </summary>
        /// <param name="countryId">The country id</param>
        /// <returns>List of tuples which hold the day in format string "YYYY-MM-DD", the number of confirmed cases,
        /// the number of deaths and the number of recovered cases known in the respective day in the specified country</returns>
        public List<Tuple<string, int, int, int>> GetCovidInfoByCountryId(int countryId)
        {
            var dayListCovidInfo = new List<Tuple<string, int, int, int>>();
            var sql = $"SELECT update_date, confirmed, deaths, recovered FROM dayinfo WHERE code={countryId};";
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

        /// <returns>A list of countries id which holds data in the dayinfo table</returns>
        public List<int> GetCountriesId()
        {
            var dayListCovidInfo = new List<int>();
            var sql = $"SELECT DISTINCT code FROM dayinfo;";
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
