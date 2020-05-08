CREATE TABLE region (
region_id    NUMBER(1) NOT NULL,
region_name  VARCHAR2(8) NOT NULL,
CONSTRAINT region_pk PRIMARY KEY ( region_id ));


CREATE TABLE country (
name       VARCHAR2(30) NOT NULL,
code       NUMBER(3) NOT NULL,
alpha      CHAR(2) NOT NULL,
region_id  NUMBER(1) NOT NULL,
population NUMBER(10),
CONSTRAINT country_pk PRIMARY KEY (code),
FOREIGN KEY (region_id) REFERENCES region (region_id) ON DELETE NO ACTION ON UPDATE NO ACTION);


CREATE TABLE dayinfo (
update_date  DATE NOT NULL,
confirmed    NUMBER(7),
deaths       NUMBER(6),
recovered    NUMBER(7),
code         NUMBER(3) NOT NULL,
CONSTRAINT dayinfo PRIMARY KEY (code, update_date),
CONSTRAINT fk_country_dayingo FOREIGN KEY ( code ) REFERENCES country ( code ) ON DELETE NO ACTION ON UPDATE NO ACTION);