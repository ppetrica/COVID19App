CREATE TABLE country (
    name       VARCHAR2(30) NOT NULL,
    code       NUMBER(3) NOT NULL,
    alpha      CHAR(2) NOT NULL,
    region_id  NUMBER(1) NOT NULL
);

ALTER TABLE country ADD CONSTRAINT country_pk PRIMARY KEY ( code );

CREATE TABLE dayinfo (
    update_date  DATE NOT NULL,
    confirmed    NUMBER(7),
    deaths       NUMBER(6),
    recovered    NUMBER(7),
    code         NUMBER(3) NOT NULL
);

ALTER TABLE dayinfo ADD CONSTRAINT covid_pk PRIMARY KEY ( code,
                                                          update_date );

CREATE TABLE region (
    region_id    NUMBER(1) NOT NULL,
    region_name  VARCHAR2(8) NOT NULL
);

ALTER TABLE region ADD CONSTRAINT region_pk PRIMARY KEY ( region_id );

ALTER TABLE dayinfo
    ADD CONSTRAINT fk_country_covid FOREIGN KEY ( code )
        REFERENCES country ( code );

ALTER TABLE country
    ADD CONSTRAINT fk_region_contry FOREIGN KEY ( region_id )
        REFERENCES region ( region_id );

