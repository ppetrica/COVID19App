# run like this
# py execute_sql script_filename.sql database_name.db

import sqlite3
import sys


def get_script_and_database():
    argc = len(sys.argv)
    print(argc)
    print(sys.argv)
    if argc == 2:
        script_filename = sys.argv[1]
        database_name = "covid.db"
    elif argc == 3:
        script_filename = sys.argv[1]
        database_name = sys.argv[2]
    else:
        script_filename = "insert_countries_and_regions.sql"
        database_name = "covid.db"
    return script_filename, database_name


def main():
    (script_filename, database_name) = get_script_and_database()
    print(script_filename)
    print(database_name)

    try:
        connection = sqlite3.connect(database_name)
        cursor = connection.cursor()

        try:

            print("Reading Script...")
            scriptFile = open(script_filename, 'r')
            script = scriptFile.read()
            scriptFile.close()
            print(script)

            print("Running Script...")
            cursor.executescript(script)
            print("Script Done ...")

            connection.commit()
            print("Changes committed ...")

        except Exception as e:
            print(type(e))
            print(e)

        finally:
            connection.close()
            print("Connection Closed ...")

    except Exception:
        print("Error opening the database")


if __name__ == "__main__":
    main()

