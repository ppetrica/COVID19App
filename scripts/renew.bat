python execute_sql.py drop_tables.sql
python execute_sql.py create_tables.sql
python execute_sql.py insert_countries_and_regions.sql

copy covid.db ..\COVID19App\resources\sql /Y
del covid.db