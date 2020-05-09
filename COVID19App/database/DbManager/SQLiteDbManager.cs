using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;


namespace database
{
    /// <summary>
    /// SqlDataBaseManager has the role of ensuring the connection to the local .db database.
    /// It has the CRUD functionality.
    /// In order to use it, first set the databaseConnection.
    /// If tables dayinfo, country and region not in the database System.Data.SQLite.SQLiteException is thrown.
    /// </summary>
    public class SQLiteDbManager : IDbManager
    {
        private SQLiteConnection _dbConnection;
        private SQLiteDataReader _dataReader;
        /// <summary>
        /// _dbConnection is the SQLiteConnection parameter for the database specified
        /// _dataReader is the SQLiteDataReader parameter which holds the result of a query
        /// </summary>
        public SQLiteDbManager()
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
        /// Clear data from the table from the database.
        /// </summary>
        /// <param name="tableName">Table Name to be cleared</param>
        /// <returns>True if the table is cleared successfully else return False</returns>
        public void ClearTable(string tableName)
        {
            _dbConnection.Open();
            try
            {
                var sql = new SQLiteCommand($"DELETE FROM {tableName};", _dbConnection);
                sql.ExecuteNonQuery();
            }
            finally
            {
                _dbConnection.Close();
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
        public void InsertCountry(string name, ushort code, string alpha, byte regionId, long population)
        {
            _dbConnection.Open();
            try
            {
                var sql = new SQLiteCommand("INSERT OR REPLACE INTO country (name, code, alpha, region_id, population) VALUES (@name, @code, @alpha, @regionId, @population);", _dbConnection);
                sql.Parameters.AddWithValue("@name", name);
                sql.Parameters.AddWithValue("@code", code);
                sql.Parameters.AddWithValue("@alpha", alpha);
                sql.Parameters.AddWithValue("@regionId", regionId);
                sql.Parameters.AddWithValue("@population", population);
                sql.ExecuteNonQuery();
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        /// <summary>
        /// Inserting a continent in the region table from the database.
        /// </summary>
        /// <param name="regionId">The UNIQUE region id of the continent</param>
        /// <param name="regionName">The continent name</param>
        /// <returns>True if the insertion was successful else return False</returns>
        public void InsertRegion(byte regionId, string regionName)
        {
            _dbConnection.Open();
            try
            {
                var sql = new SQLiteCommand("INSERT OR REPLACE INTO region (region_id, region_name) VALUES (@regionId, @regionName);", _dbConnection);
                sql.Parameters.AddWithValue("@regionId", regionId);
                sql.Parameters.AddWithValue("@regionName", regionName);
                sql.ExecuteNonQuery();
            }
            finally
            {
                _dbConnection.Close();
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
        public void InsertDayInfo(string updateDate, int confirmed, int deaths, int recovered, int code)
        {
            
            _dbConnection.Open();
            try
            {
                var sql = new SQLiteCommand(
                    "INSERT INTO dayinfo (update_date, confirmed, deaths, recovered, code) VALUES (@updateDate, @confirmed, @deaths, @recovered, @code);",
                    _dbConnection);
                sql.Parameters.AddWithValue("@updateDate", updateDate);
                sql.Parameters.AddWithValue("@confirmed", confirmed);
                sql.Parameters.AddWithValue("@deaths", deaths);
                sql.Parameters.AddWithValue("@recovered", recovered);
                sql.Parameters.AddWithValue("@code", code);
                sql.ExecuteNonQuery();
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        /// <summary>
        /// Inserting a Day Infos about COVID-19 in thedayinfo table from the database.
        /// </summary>
        /// <param name="updateDate">The date in the format "YYYY-MM-DD"</param>
        /// <param name="confirmed">The number of confirmed cases of COVID-19</param>
        /// <param name="deaths">The number of deaths cases of COVID-19</param>
        /// <param name="recovered">The number of recovered cases of COVID-19</param>
        /// <param name="code">The code of the country</param>
        /// <returns>True if the insertion was successful else return False</returns>
        public void InsertDayInfos(List<Tuple<string, int, int, int, int>> dayInfoList)
        {
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            using (var command = _dbConnection.CreateCommand())
            {
                command.CommandText =
                    "INSERT INTO dayinfo (update_date, confirmed, deaths, recovered, code) VALUES (@updateDate, @confirmed, @deaths, @recovered, @code);";
                var updateDate = command.CreateParameter();
                updateDate.ParameterName = "@updateDate";
                command.Parameters.Add(updateDate);

                var confirmed = command.CreateParameter();
                confirmed.ParameterName = "@confirmed";
                command.Parameters.Add(confirmed);

                var deaths = command.CreateParameter();
                deaths.ParameterName = "@deaths";
                command.Parameters.Add(deaths);

                var recovered = command.CreateParameter();
                recovered.ParameterName = "@recovered";
                command.Parameters.Add(recovered);

                var code = command.CreateParameter();
                code.ParameterName = "@code";
                command.Parameters.Add(code);

                foreach (var dayInfo in dayInfoList)
                {
                    updateDate.Value = dayInfo.Item1;
                    confirmed.Value = dayInfo.Item2;
                    deaths.Value = dayInfo.Item3;
                    recovered.Value = dayInfo.Item4;
                    code.Value = dayInfo.Item5;
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SQLiteException)
                    {
                        Console.WriteLine("Duplicate information for info day table. Country : " + dayInfo.Item5 + ", Date : " + dayInfo.Item1);
                    }
                }
                transaction.Commit();
            }
            _dbConnection.Close();

        }

        /// <param name="id">The id of the country</param>
        /// <returns>The country name of the specified id if it exists in the database else returns NULL</returns>
        public string GetRegionNameById(int id)
        {
            _dbConnection.Open();

            try
            {
                var sql = new SQLiteCommand("SELECT region_name FROM region WHERE region_id=@id;", _dbConnection);
                sql.Parameters.AddWithValue("@id", id);
                _dataReader = sql.ExecuteReader();
                if (_dataReader.Read())
                {
                    return _dataReader.GetString(0);
                }
                else
                {
                    throw new ObjectNotFoundException("Region Id specified not found in the database");
                }
            }
            finally
            {
                _dataReader.Close();    //the most import line of code
                _dbConnection.Close();
            }
        }

        /// <summary>
        /// Getting the id of the country  from the country table in the database
        /// Throws ObjectNotFoundException if country name is not in the database
        /// </summary>
        /// <param name="countryName">The country name</param>
        /// <returns>The id of the country if it exists in the database else returns -1</returns>
        public int GetCountryIdByName(string countryName)
        {
            _dbConnection.Open();
            try
            {
                var sql = new SQLiteCommand("SELECT code FROM country WHERE name = @countryName", _dbConnection);
                sql.Parameters.AddWithValue("@countryName", countryName);
                _dataReader = sql.ExecuteReader();
                if (_dataReader.Read())
                {
                    return _dataReader.GetInt32(0);
                }
                else
                {
                    throw new ObjectNotFoundException("Country Name specified not found in the database");
                }
            }
            finally
            {
                _dataReader.Close(); //the most important line of code
                _dbConnection.Close();
            }
        }

        /// <summary>
        /// Getting the country info from the country table in the database
        /// </summary>
        /// <param name="countryId">The country Id</param>
        /// <returns>A tuple which hold the name, the alphanumeric code of the country the id of the continent where this country is located and the population</returns>
        public Tuple<string, string, int, long> GetCountryInfoById(int countryId)
        {
            _dbConnection.Open();
            try
            {
                var sql = new SQLiteCommand("SELECT name, alpha, region_id, population FROM country WHERE code=@countryId;", _dbConnection);
                sql.Parameters.AddWithValue("@countryId", countryId);
                _dataReader = sql.ExecuteReader();
                if (_dataReader.Read())
                {
                    return new Tuple<string, string, int, long>(_dataReader.GetString(0), _dataReader.GetString(1), _dataReader.GetInt32(2), _dataReader.GetInt64(3));
                }
                else
                {
                    throw new ObjectNotFoundException("Country Id specified not found in the database");
                }
            }
            finally
            {
                _dataReader.Close(); //the most important line of code
                _dbConnection.Close();
            }
        }

        /// <summary>
        /// Getting the information of COVID-19 from the dayinfo table in the database
        /// </summary>
        /// <param name="countryId">The country id</param>
        /// <returns>List of tuples which hold the day in format string "YYYY-MM-DD", the number of confirmed cases,
        /// the number of deaths and the number of recovered cases known in the respective day in the specified country. Returns null if an exception occured.</returns>
        public List<Tuple<string, int, int, int>> GetCovidInfoByCountryId(int countryId)
        {
            _dbConnection.Open();
            try
            {
                var sql = new SQLiteCommand("SELECT update_date, confirmed, deaths, recovered FROM dayinfo WHERE code=@countryId;", _dbConnection);
                sql.Parameters.AddWithValue("@countryId", countryId);
                _dataReader = sql.ExecuteReader();
                var dayListCovidInfo = new List<Tuple<string, int, int, int>>();
                if (_dataReader.Read())
                {
                    dayListCovidInfo.Add(Tuple.Create(_dataReader.GetString(0), _dataReader.GetInt32(1), _dataReader.GetInt32(2), _dataReader.GetInt32(3)));
                    while (_dataReader.Read())
                    {
                        dayListCovidInfo.Add(Tuple.Create(_dataReader.GetString(0), _dataReader.GetInt32(1), _dataReader.GetInt32(2), _dataReader.GetInt32(3)));
                    }

                    return dayListCovidInfo;
                }
                else
                {
                    throw new ObjectNotFoundException("Country Info for id specified not found in the database");
                }
            }
            finally
            {
                _dataReader.Close(); //the most important line of code
                _dbConnection.Close();
            }
        }

        /// <returns>A list of countries id which holds data in the dayinfo table. If an exception occured returns null</returns>
        public List<int> GetCountryCodes()
        {
            _dbConnection.Open();
            try
            {
                var sql = new SQLiteCommand("SELECT DISTINCT code FROM dayinfo;", _dbConnection);
                var dayListCovidInfo = new List<int>();
                _dataReader = sql.ExecuteReader();
                if (_dataReader.Read())
                {
                    dayListCovidInfo.Add(_dataReader.GetInt32(0));
                    while (_dataReader.Read())
                    {
                        dayListCovidInfo.Add(_dataReader.GetInt32(0));
                    }
                    return dayListCovidInfo;
                }
                else
                {
                    throw new ObjectNotFoundException("Not a single country was found.");
                }
            }
            finally
            {
                _dataReader.Close(); //the most important line of code
                _dbConnection.Close();
            }
        }

        public string GetRegionNameByCountryId(int countryId)
        {
            _dbConnection.Open();
            try
            {
                var sql = new SQLiteCommand("SELECT region_name FROM region WHERE region_id = " +
                                            "(SELECT region_id FROM country WHERE code = @countryId)", _dbConnection);
                sql.Parameters.AddWithValue("@countryId", countryId);
                _dataReader = sql.ExecuteReader();
                if (_dataReader.Read())
                {
                    return _dataReader.GetString(0);
                }
                else
                {
                    throw new ObjectNotFoundException("Country not found in the database");
                }
            }
            finally
            {
                _dataReader.Close(); //the most important line of code
                _dbConnection.Close();
            }
        }

        /// <summary>
        /// Getting the most current date of the data from the database
        /// Throws ObjectNotFoundException if there is no data in the database
        /// </summary>
        /// <returns>A tuple of 3 integer: year, month and day</returns>
        public string GetTheMostRecentDate()
        {
            _dbConnection.Open();
            try
            {

                var sql = new SQLiteCommand("SELECT MAX(update_date) FROM dayinfo", _dbConnection);
                _dataReader = sql.ExecuteReader();
                //isDbNull check if the specified column is not null
                //this query returns null if there is no data in the db
                if (_dataReader.Read() && !_dataReader.IsDBNull(0)) 
                {
                    return _dataReader.GetString(0);
                }
                else
                {
                    throw new ObjectNotFoundException("Data not found in the database");
                }
            }
            finally
            {
                _dataReader.Close(); //the most important line of code
                _dbConnection.Close();
            }
        }
    }
}
