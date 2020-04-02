import sqlite3

conn = sqlite3.connect("covid.db")
c = conn.cursor()

c.execute("""
CREATE TABLE region (
    region_id    NUMBER(1) NOT NULL,
    region_name  VARCHAR2(8) NOT NULL,
    CONSTRAINT region_pk PRIMARY KEY ( region_id )
);
    """)

c.execute("""
    CREATE TABLE country (
    name       VARCHAR2(30) NOT NULL,
    code       NUMBER(3) NOT NULL,
    alpha      CHAR(2) NOT NULL,
    region_id  NUMBER(1) NOT NULL,
    CONSTRAINT country_pk PRIMARY KEY (code),
    FOREIGN KEY (region_id) REFERENCES region (region_id) ON DELETE NO ACTION ON UPDATE NO ACTION)
""")

c.execute("""
CREATE TABLE dayinfo (
    update_date  DATE NOT NULL,
    confirmed    NUMBER(7),
    deaths       NUMBER(6),
    recovered    NUMBER(7),
    code         NUMBER(3) NOT NULL,
    CONSTRAINT dayinfo PRIMARY KEY (code, update_date),
    CONSTRAINT fk_country_dayingo FOREIGN KEY ( code )
         REFERENCES country ( code ) ON DELETE NO ACTION ON UPDATE NO ACTION)
    """)


# c.execute("""
#  ALTER TABLE country ADD CONSTRAINT country_pk PRIMARY KEY ( code );
#     """)

# c.execute("""
# ALTER TABLE dayinfo ADD CONSTRAINT covid_pk PRIMARY KEY ( code,
#                                                           update_date );
#     """)


# c.execute("""
# ALTER TABLE region ADD CONSTRAINT region_pk PRIMARY KEY ( region_id );
#     """)


# c.execute("""
# ALTER TABLE dayinfo
#     ADD CONSTRAINT fk_country_covid FOREIGN KEY ( code )
#         REFERENCES country ( code );
#     """)
#
#
# c.execute("""
# ALTER TABLE country
#     ADD CONSTRAINT fk_region_contry FOREIGN KEY ( region_id )
#         REFERENCES region ( region_id );
#     """)




