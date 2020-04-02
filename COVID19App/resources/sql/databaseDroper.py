import sqlite3

conn = sqlite3.connect("covid.db")
c = conn.cursor()

c.execute("""
    DROP TABLE region;
    """)

c.execute("""
    DROP TABLE country;
    """)

c.execute("""
    DROP TABLE dayinfo;
    """)

