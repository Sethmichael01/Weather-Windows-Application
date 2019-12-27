using System;
using System.Windows.Forms;
using System.Globalization;
using BL;
using System.Threading.Tasks;

namespace WeatherAppWinForms
{
    public partial class WeatherForm : Form
    {
        public WeatherForm()
        {
            InitializeComponent();
        }

        private void WeatherForm_Load(object sender, EventArgs e)
        {
            Converter converter = new Converter();
            CityData cityData = new CityData();
            cityData = converter.ConvertCityFromDB();
            txtId.Text = cityData.Id.ToString();
            txtCity.Text = cityData.CityName;
            txtCountry.Text = cityData.CountryName;
            txtLat.Text = cityData.Latitude;
            txtLong.Text = cityData.Longtitude;

            for (int i = 0; i < 8; i++)
            {
                DateTime time = DateTime.Parse(cityData.RainTime[i],
                       null, DateTimeStyles.AssumeUniversal);
                ListViewItem item = listView1.Items.Add(time.DayOfWeek.ToString());
                item.SubItems.Add(time.ToShortTimeString());
                item.SubItems.Add(cityData.Temp[i]);
                item.SubItems.Add(cityData.MaxTemp[i]);
                item.SubItems.Add(cityData.MinTemp[i]);
                item.SubItems.Add(cityData.Main[i]);
                item.SubItems.Add(cityData.Description[i]);
                item.SubItems.Add(cityData.RainIn3H[i]);
            }
        }

        private async void btnForecast_ClickAsync(object sender, EventArgs e)
        {
            bool isRain = false;
            listView1.Items.Clear();
            CityData cityData = new CityData();
            Converter converter = new Converter();
            string name = txtLocation.Text;
            if (String.IsNullOrEmpty(name))
            {
                MessageBox.Show("Input name of city!");
            }
            else
            {
                string cityName = converter.GetCityName();
                cityData = converter.ConvertCityFromWinForms(name);
                if (cityData == null)
                {
                    MessageBox.Show("Wrong name of city!");
                }
                else
                {
                    txtId.Text = cityData.Id.ToString();
                    if (cityName == name)
                    {
                        DateTime startTime = DateTime.Parse(cityData.RainTime[0],
                                   null, DateTimeStyles.AssumeUniversal);
                        txtCity.Text = cityData.CityName;
                        txtCountry.Text = cityData.CountryName;
                        txtLat.Text = cityData.Latitude;
                        txtLong.Text = cityData.Longtitude;

                        for (int i = 0; i < 8; i++)
                        {
                            DateTime time = DateTime.Parse(cityData.RainTime[i],
                                null, DateTimeStyles.AssumeUniversal);
                            ListViewItem item = listView1.Items.Add(time.DayOfWeek.ToString());
                            item.SubItems.Add(time.ToShortTimeString());
                            item.SubItems.Add(cityData.Temp[i]);
                            item.SubItems.Add(cityData.MaxTemp[i]);
                            item.SubItems.Add(cityData.MinTemp[i]);
                            item.SubItems.Add(cityData.Main[i]);
                            item.SubItems.Add(cityData.Description[i]);
                            item.SubItems.Add(cityData.RainIn3H[i]);
                        }
                    }
                    else
                    {
                        txtCity.Text = cityData.CityName;
                        txtCountry.Text = cityData.CountryName;
                        txtLat.Text = cityData.Latitude;
                        txtLong.Text = cityData.Longtitude;

                        for (int i = 0; i < 8; i++)
                        {
                            DateTime time = DateTime.Parse(cityData.RainTime[i],
                                null, DateTimeStyles.AssumeUniversal);
                            ListViewItem item = listView1.Items.Add(time.DayOfWeek.ToString());
                            item.SubItems.Add(time.ToShortTimeString());
                            item.SubItems.Add(cityData.Temp[i]);
                            item.SubItems.Add(cityData.MaxTemp[i]);
                            item.SubItems.Add(cityData.MinTemp[i]);
                            item.SubItems.Add(cityData.Main[i]);
                            item.SubItems.Add(cityData.Description[i]);
                            item.SubItems.Add(cityData.RainIn3H[i]);
                            if (cityData.RainIn3H[i] != null)
                            {
                                isRain = true;
                            }
                        }

                        if (isRain == true)
                        {
                            MessageBox.Show("It will rain soon!");
                        }
                        bool result = await converter.EditDB(cityData);
                        if (!result)
                        {
                            MessageBox.Show("Error");
                        }
                    }
                }
            }
                        
        }
    }
}
    

