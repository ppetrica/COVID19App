import json
import _sqlite3

region_dict = {
    "Europe": 1,
    "Asia": 2,
    "Americas": 3,
    "Africa": 4,
    "Oceania": 5,
    "Other": 6
}

class Country:
    def __init__(self, name, code, alpha, region):
        self.name = name
        self.code = code
        self.alpha = alpha
        self.region = region
        self.region = region_dict.get(region)


def main():
    with open("country.json", "r") as json_file:
        data = json.load(json_file)
        country_list = []
        for country in data:
            name = country["name"]
            alpha = country["alpha-2"]
            code = country["country-code"]
            region = country["region"]
            if not region:
                region = "Other"  # Antarctica does not have a region
            country_list.append(Country(name, code, alpha, region))

        with open("insert_countries_and_regions.sql", "w") as sql_file:
            sql_file.write("DELETE FROM country;" + "\n")
            sql_file.write("DELETE FROM region;" + "\n\n")

            for reg_key in region_dict:
                sql_file.write(f'INSERT INTO region VALUES ({region_dict.get(reg_key)}, "{reg_key}");' + "\n")
            sql_file.write("\n")

            for country in country_list:
                # print(country.region)
                sql_file.write(f'INSERT INTO country VALUES ("{country.name}", {country.code}, "{country.alpha}", {country.region});\n')


if __name__ == "__main__":
    main()
