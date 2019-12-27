using System;
using System.Data;
using WeatherAPI;
using DAL;
using WeatherAppWinForms;
using System.Threading.Tasks;

namespace BL
{
    public class Converter
    {
        JsonParser jsonParser = new JsonParser();
        CityList cityList = new CityList();
        CityData cityData = new CityData();
        DataAccess dataAccess = new DataAccess();

        public CityData ConvertCityFromDB()
        {
            DataTable tab = GetCityFromDB();
            DataRow row = tab.Rows[0];
            cityList = jsonParser.Parsing(row["CityName"].ToString());
            cityData = ConvertClasses(cityList);
            cityData.Id = Convert.ToInt32(row["Id"]);
            return cityData;
        }

        public CityData ConvertCityFromWinForms(string name)
        {
            cityList = jsonParser.Parsing(name);
            if (cityList == null)
            {
                cityData = null;

            }
            else
            {
                cityData = ConvertClasses(cityList);
                cityData.Id = 1;
            }
            return cityData;
        }

        public DataTable GetCityFromDB()
        {
            DataTable tab = dataAccess.GetCityData();
            return tab;
        }

        public CityData ConvertClasses(CityList cList)
        {
            cityData.Id = cList.city.id;
            cityData.CityName = cList.city.name;
            cityData.CountryName = cList.city.country;
            cityData.Latitude = cList.city.coord.lat.ToString();
            cityData.Longtitude = cList.city.coord.lon.ToString();
            for (int i = 0; i < 8; i++)
            {
                cityData.RainTime.Add(cList.list[i].dt_txt);
                cityData.Temp.Add(cList.list[i].main.temp.ToString());
                cityData.MaxTemp.Add(cList.list[i].main.temp_max.ToString());
                cityData.MinTemp.Add(cList.list[i].main.temp_min.ToString());
                cityData.RainIn3H.Add(cList.list[i].rain?.rain3h?.ToString());
                cityData.Main.Add(cList.list[i].weather[0].main);
                cityData.Description.Add(cList.list[i].weather[0].description);
            }

            return cityData;
        } 

        public string GetCityName()
        {
            string city;
            DataTable tab = new DataTable();
            tab = GetCityFromDB();
            DataRow row = tab.Rows[0];
            city = row["CityName"].ToString().Trim();
            return city;
        }

        public async Task<bool> EditDB(CityData city)
        {
            bool result = await dataAccess.EditCityData(city);
            return result;
        }
    }
}
