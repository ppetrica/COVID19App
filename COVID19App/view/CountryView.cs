using core;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Drawing;


namespace view
{
    /// <summary>
    /// Class responsible for creating a view with 
    /// statistics about COVID-19 effects on country level, 
    /// using the country info list.
    /// </summary>
    public class CountryView : IView
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="info">The information about COVID19 for each country.</param>
        public CountryView(IReadOnlyList<CountryInfoEx> info)
        {
            _countries = info;

            // country for wich stats are shown
            string countryCodeSearched = "ES";
            string countryNameSearched = string.Empty;

            // calculate the sum of active people per day on every region and other statistics
            int numberOfDays = 90; // infection rate evolution is followed on last {numberOfDays} days
            DateTime startDate = (new DateTime(2020, 1, 22)).AddDays(_countries[0].DaysInfo.Count - numberOfDays); // first day of provided info is 22.01.2020 
            long[] active = new long[numberOfDays];
            long[] deaths = new long[numberOfDays];
            long[] recovered = new long[numberOfDays];

            long totalConfirmed = 0;
            long totalDeaths = 0;
            long totalRecovered = 0;

            // information used for normalize 
            long mostConfirmed = -1;
            string mostConfirmedCountry = string.Empty;

            long mostDeaths = -1;
            string mostDeathsCountry = string.Empty;

            long mostRecovered = -1;
            string mostRecoveredCountry = string.Empty;


            foreach (CountryInfoEx country in _countries)
            {
                if (country.CountryCode == countryCodeSearched)
                {
                    totalConfirmed = country.Confirmed;
                    totalDeaths = country.Deaths;
                    totalRecovered = country.Recovered;
                    countryNameSearched = country.Name;

                    foreach (DayInfo day in country.DaysInfo)
                    {
                        int index = (day.Date.ToDateTime() - startDate).Days;
                        if (index < 0)
                        { // skip info from days that are before the chosen start date 
                            continue;
                        }
                        active[index] = day.Confirmed - day.Deaths - day.Recovered;
                        deaths[index] = day.Deaths;
                        recovered[index] = day.Recovered;
                    }
                }


                // for normalize
                if (country.Confirmed > mostConfirmed)
                {
                    mostConfirmedCountry = country.Name;
                    mostConfirmed = country.Confirmed;
                }

                if (country.Deaths > mostDeaths)
                {
                    mostDeathsCountry = country.Name;
                    mostDeaths = country.Deaths;
                }

                if (country.Recovered > mostRecovered)
                {
                    mostRecoveredCountry = country.Name;
                    mostRecovered = country.Recovered;

                }
            }


            // build the chart with numbers of infected people 
            StackedAreaSeries recoveredStackedArea = new StackedAreaSeries
            {
                Title = "Recovered",
                Values = GetRegionChartValues(recovered, startDate),
                LineSmoothness = 0
            };
            StackedAreaSeries deathsStackedArea = new StackedAreaSeries
            {
                Title = "Deaths",
                Values = GetRegionChartValues(deaths, startDate),
                LineSmoothness = 0
            };

            StackedAreaSeries activeStackedArea = new StackedAreaSeries
            {
                Title = "Active",
                Values = GetRegionChartValues(active, startDate),
                LineSmoothness = 0
            };

            recoveredStackedArea.Fill = System.Windows.Media.Brushes.MediumSeaGreen;
            deathsStackedArea.Fill = System.Windows.Media.Brushes.Crimson;
            activeStackedArea.Fill = System.Windows.Media.Brushes.Gold;

            _chartRegions.Series = new SeriesCollection
            {
               deathsStackedArea,
               activeStackedArea,
               recoveredStackedArea,
            };


            _chartRegions.AxisX.Add(new Axis
            {
                LabelFormatter = value => new DateTime((long)value).ToString("dd-MMM")
            });
            _chartRegions.AxisY.Add(new Axis
            {
                Title = "Number of people",
                LabelFormatter = value => ((long)value).ToString("N0")
            });
            _chartRegions.LegendLocation = LegendLocation.Right;

            // build the solid gauge for active infected people rate
            _gaugeInfectionRate.From = 0;
            _gaugeInfectionRate.To = Math.Log(mostConfirmed);
            _gaugeInfectionRate.Value = Math.Log(totalConfirmed);
            _gaugeInfectionRate.Base.LabelsVisibility = Visibility.Hidden;
            _gaugeInfectionRate.LabelFormatter = value =>
            {
                value = Math.Exp(value);
                if (value > 1000000)
                    return $"  {Math.Round(value / 1000000.0, 2)}M \n infected";
                else
                    if (value > 1000)
                    return $"  {Math.Round(value / 1000.0, 2)}K \n infected";
                else
                    return $"    {value}\n infected";
            };
            _gaugeInfectionRate.Base.GaugeActiveFill = new LinearGradientBrush
            {
                GradientStops = new GradientStopCollection
                {
                    new GradientStop(Colors.DarkOrange, 0),
                    new GradientStop(Colors.Gold, 0.5),
                    new GradientStop(Colors.Yellow, 1)
                }
            };

            // build the solid gauge for death rate
            _gaugeDeathRate.From = 0;
            _gaugeDeathRate.To = Math.Log(mostDeaths);
            _gaugeDeathRate.Value = Math.Log(totalDeaths);
            _gaugeDeathRate.Base.LabelsVisibility = Visibility.Hidden;
            _gaugeDeathRate.LabelFormatter = value =>
            {
                value = Math.Exp(value);
                if (value > 1000000)
                    return $"  {Math.Round(value / 1000000.0, 2)}M \n deaths";
                else
                    if (value > 1000)
                    return $"  {Math.Round(value / 1000.0, 2)}K \n deaths";
                else
                    return $"     {value}\n deaths";
            };
            _gaugeDeathRate.Base.GaugeActiveFill = new LinearGradientBrush
            {
                GradientStops = new GradientStopCollection
                {
                    new GradientStop(Colors.Firebrick, 0),
                    new GradientStop(Colors.Crimson, 0.5),
                    new GradientStop(Colors.Red, 1)
                }
            };

            // build the solid gauge for recovery rate
            _gaugeRecoveryRate.From = 0;
            _gaugeRecoveryRate.To = Math.Log(mostRecovered);
            _gaugeRecoveryRate.Value = Math.Log(totalRecovered);
            _gaugeRecoveryRate.Base.LabelsVisibility = Visibility.Hidden;
            _gaugeRecoveryRate.LabelFormatter = value =>
            {
                value = Math.Exp(value);
                if (value > 1000000)
                    return $"  {Math.Round(value / 1000000.0, 2)}M \nrecovred";
                else
                    if (value > 1000)
                    return $"  {Math.Round(value / 1000.0, 2)}K \nrecovered";
                else
                    return $"     {value}\nrecovered";
            };
            _gaugeRecoveryRate.Base.GaugeActiveFill = new LinearGradientBrush
            {
                GradientStops = new GradientStopCollection
                {
                    new GradientStop(Colors.DarkGreen, 0),
                    new GradientStop(Colors.MediumSeaGreen, 0.5),
                    new GradientStop(Colors.Chartreuse, 1)
                }
            };

            // buid the page layout as a table layout
            _layoutPanel.Dock = DockStyle.Fill;
            _layoutPanel.RowCount = 3;
            _layoutPanel.ColumnCount = 3;
            _layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            _layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 60));
            _layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 30));
            _layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            _layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            _layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            _layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));

            // row 0
            Font fontTitle = new Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            _layoutPanel.Controls.Add(new Label
            {
                Text = countryNameSearched,
                AutoSize = true,
                Font = fontTitle,
                Anchor = AnchorStyles.None
            }, 1, 0);


            // row 1

            _layoutPanel.Controls.Add(_chartRegions, 0, 1);
            _layoutPanel.SetColumnSpan(_chartRegions, 3);
            _chartRegions.Dock = DockStyle.Fill;

            // row 2 
            _layoutPanel.Controls.Add(_gaugeInfectionRate, 0, 2);
            _gaugeInfectionRate.Anchor = AnchorStyles.None;

            _layoutPanel.Controls.Add(_gaugeDeathRate, 1, 2);
            _gaugeDeathRate.Anchor = AnchorStyles.None;

            _layoutPanel.Controls.Add(_gaugeRecoveryRate, 2, 2);
            _gaugeRecoveryRate.Anchor = AnchorStyles.None;

            // row 3
            Font font = new Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            _layoutPanel.Controls.Add(new Label
            {
                Text = (mostConfirmed > 1000000 ? $"Reference value: {Math.Round(mostConfirmed / 1000000.0, 2)}M ({mostConfirmedCountry})" :
                                                  $"Reference value: {Math.Round(mostConfirmed / 1000.0, 2)}K ({mostConfirmedCountry})"),
                AutoSize = true,
                Font = font,
                Anchor = AnchorStyles.None
            }, 0, 3);

            _layoutPanel.Controls.Add(new Label
            {
                Text = (mostDeaths > 1000000 ? $"Reference value: {Math.Round(mostDeaths / 1000000.0, 2)}M ({mostDeathsCountry})" :
                                           $"Reference value: {Math.Round(mostDeaths / 1000.0, 2)}K ({mostDeathsCountry})"),
                AutoSize = true,
                Font = font,
                Anchor = AnchorStyles.None
            }, 1, 3);

            _layoutPanel.Controls.Add(new Label
            {
                Text = (mostRecovered > 1000000 ? $"Reference value: {Math.Round(mostRecovered / 1000000.0, 2)}M ({mostRecoveredCountry})" :
                                                  $"Reference value: {Math.Round(mostRecovered / 1000.0, 2)}K ({mostRecoveredCountry})"),
                AutoSize = true,
                Font = font,
                Anchor = AnchorStyles.None
            }, 2, 3);

            _page.Padding = new Padding(30);
            _page.Controls.Add(_layoutPanel);
        }

        /// <summary>
        /// Returns the generated tab page control to be added in a form
        /// </summary>
        /// <returns>Generated tab page</returns>
        public TabPage GetPage()
        {
            return _page;
        }

        private ChartValues<DateTimePoint> GetRegionChartValues(long[] region, DateTime startDate)
        {
            ChartValues<DateTimePoint> values = new ChartValues<DateTimePoint>();

            for (int i = 0; i < region.Length; ++i)
                values.Add(new DateTimePoint(startDate.AddDays(i), region[i]));

            return values;
        }

        private IReadOnlyList<CountryInfoEx> _countries;
        private LiveCharts.WinForms.CartesianChart _chartRegions = new LiveCharts.WinForms.CartesianChart();
        private LiveCharts.WinForms.SolidGauge _gaugeInfectionRate = new LiveCharts.WinForms.SolidGauge();
        private LiveCharts.WinForms.SolidGauge _gaugeDeathRate = new LiveCharts.WinForms.SolidGauge();
        private LiveCharts.WinForms.SolidGauge _gaugeRecoveryRate = new LiveCharts.WinForms.SolidGauge();
        private TableLayoutPanel _layoutPanel = new TableLayoutPanel();
        private TabPage _page = new TabPage("Country statistics");
    }
}
