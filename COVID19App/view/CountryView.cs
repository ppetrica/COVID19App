/*************************************************************************
 *                                                                        *
 *  File:        CountryView.cs                                           *
 *  Copyright:   (c) 2020, Pascal Dragos                                  *
 *  E-mail:      dragos.pascal@student.tuiasi.ro                          *
 *  Description: This class represents the country specific View          *
 *  of the application.                                                   *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using Core;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Drawing;

/// <summary>
/// This module manages map, charts, gauges display on the screen.
/// </summary>
namespace View
{ 
    /// <summary>
    /// Class responsible for creating a View with 
    /// statistics about COVID-19 effects on country level, 
    /// using the country info list.
    /// </summary>
    public class CountryView : IView, IMapObserver
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="info">The information about COVID19 for each country.</param>
        public CountryView(IReadOnlyList<CountryInfoEx> info)
        {
            const long animationSpeedGraph = 0;
            const long animationSpeedGauge = 5000000;

            IReadOnlyList<CountryInfoEx> countries = info;

            // information used for normalize 
            CountryInfoEx _mostConfirmed;
            CountryInfoEx _mostDeaths;
            CountryInfoEx _mostRecovered;

            _startDate = countries[0].DaysInfo[0].Date.ToDateTime();
            NumberOfDays = countries[0].DaysInfo.Count;

            _mostConfirmed = Utils.MaxElement(countries, (CountryInfoEx a, CountryInfoEx b) => a.Confirmed > b.Confirmed);
            _mostDeaths = Utils.MaxElement(countries, (CountryInfoEx a, CountryInfoEx b) => a.Deaths > b.Deaths);
            _mostRecovered = Utils.MaxElement(countries, (CountryInfoEx a, CountryInfoEx b) => a.Recovered > b.Recovered);

            _confirmed = new long[NumberOfDays];
            _deaths = new long[NumberOfDays];
            _recovered = new long[NumberOfDays];

            // build the chart with numbers of infected people 
            _stackedAreaRecovered = new StackedAreaSeries
            {
                Title = "Recovered",
                Values = GetRegionChartValues(new long[1], _startDate),
                LineSmoothness = 0
            };

            _stackedAreaDead = new StackedAreaSeries
            {
                Title = "Deaths",
                Values = GetRegionChartValues(new long[1], _startDate),
                LineSmoothness = 0
            };
            
            _stackedAreaConfirmed = new StackedAreaSeries
            {
                Title = "Active",
                Values = GetRegionChartValues(new long[1], _startDate),
                LineSmoothness = 0
            };

            _stackedAreaRecovered.Fill = System.Windows.Media.Brushes.MediumSeaGreen;
            _stackedAreaDead.Fill = System.Windows.Media.Brushes.Crimson;
            _stackedAreaConfirmed.Fill = System.Windows.Media.Brushes.Gold;
            _cartesianChartRegions.Series = new SeriesCollection
            {
               _stackedAreaDead,
               _stackedAreaConfirmed,
               _stackedAreaRecovered,
            };

            _cartesianChartRegions.AnimationsSpeed = new TimeSpan(animationSpeedGraph);
            _solidGaugeDeathRate.AnimationsSpeed = new TimeSpan(animationSpeedGauge);
            _solidGaugeInfectionRage.AnimationsSpeed = new TimeSpan(animationSpeedGauge);
            _solidGaugeRecoveryRate.AnimationsSpeed = new TimeSpan(animationSpeedGauge);

            _cartesianChartRegions.AxisX.Add(new Axis
            {
                LabelFormatter = value => new DateTime((long)value).ToString("dd-MMM")
            });
            _cartesianChartRegions.AxisY.Add(new Axis
            {
                Title = "Number of people",
                LabelFormatter = value => ((long)value).ToString("N0")
            });
            
            _cartesianChartRegions.LegendLocation = LegendLocation.Right;

            // build the solid gauge for confirmed infected people rate
            _solidGaugeInfectionRage.From = 0;
            _solidGaugeInfectionRage.To = Math.Log(_mostConfirmed.Confirmed);
            _solidGaugeInfectionRage.Value = 0;
            _solidGaugeInfectionRage.Base.LabelsVisibility = Visibility.Hidden;
            _solidGaugeInfectionRage.LabelFormatter = value => value.ToString();
            _solidGaugeInfectionRage.Base.GaugeActiveFill = new LinearGradientBrush
            {
                GradientStops = new GradientStopCollection
                {
                    new GradientStop(Colors.DarkOrange, 0),
                    new GradientStop(Colors.Gold, 0.5),
                    new GradientStop(Colors.Yellow, 1)
                }
            };

            // build the solid gauge for death rate
            _solidGaugeDeathRate.From = 0;
            _solidGaugeDeathRate.To = Math.Log(_mostDeaths.Deaths);
            _solidGaugeDeathRate.Value = 0;
            _solidGaugeDeathRate.Base.LabelsVisibility = Visibility.Hidden;
            _solidGaugeDeathRate.LabelFormatter = value => value.ToString();
            _solidGaugeDeathRate.Base.GaugeActiveFill = new LinearGradientBrush
            {
                GradientStops = new GradientStopCollection
                {
                    new GradientStop(Colors.Firebrick, 0),
                    new GradientStop(Colors.Crimson, 0.5),
                    new GradientStop(Colors.Red, 1)
                }
            };

            // build the solid gauge for recovery rate
            _solidGaugeRecoveryRate.From = 0;
            _solidGaugeRecoveryRate.To = Math.Log(_mostRecovered.Recovered);
            _solidGaugeRecoveryRate.Value = 0;
            _solidGaugeRecoveryRate.Base.LabelsVisibility = Visibility.Hidden;
            _solidGaugeRecoveryRate.LabelFormatter = value => value.ToString();
            _solidGaugeRecoveryRate.Base.GaugeActiveFill = new LinearGradientBrush
            {
                GradientStops = new GradientStopCollection
                {
                    new GradientStop(Colors.DarkGreen, 0),
                    new GradientStop(Colors.MediumSeaGreen, 0.5),
                    new GradientStop(Colors.Chartreuse, 1)
                }
            };

            // buid the page layout as a table layout
            _tableLayoutPanel.Dock = DockStyle.Fill;
            _tableLayoutPanel.RowCount = 3;
            _tableLayoutPanel.ColumnCount = 3;
            
            _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 60));
            _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 30));
            _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            
            _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));

            // row 0
            Font fontTitle = new Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            _labelTitle = new Label
            {
                Text = "",
                AutoSize = true,
                Font = fontTitle,
                Anchor = AnchorStyles.None
            };
            _tableLayoutPanel.Controls.Add(_labelTitle, 1, 0);

            // row 1
            _tableLayoutPanel.Controls.Add(_cartesianChartRegions, 0, 1);
            _tableLayoutPanel.SetColumnSpan(_cartesianChartRegions, 3);
            
            _cartesianChartRegions.Dock = DockStyle.Fill;

            // row 2 
            _tableLayoutPanel.Controls.Add(_solidGaugeInfectionRage, 0, 2);
            _solidGaugeInfectionRage.Anchor = AnchorStyles.None;

            _tableLayoutPanel.Controls.Add(_solidGaugeDeathRate, 1, 2);
            _solidGaugeDeathRate.Anchor = AnchorStyles.None;

            _tableLayoutPanel.Controls.Add(_solidGaugeRecoveryRate, 2, 2);
            _solidGaugeRecoveryRate.Anchor = AnchorStyles.None;

            // row 3
            Font font = new Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            _tableLayoutPanel.Controls.Add(new Label
            {
                Text = (_mostConfirmed.Confirmed > 1000000 ? $"Reference value: {Math.Round(_mostConfirmed.Confirmed / 1000000.0, 2)}M ({_mostConfirmed.Name})" :
                                                  $"Reference value: {Math.Round(_mostConfirmed.Confirmed / 1000.0, 2)}K ({_mostConfirmed.Name})"),
                AutoSize = true,
                Font = font,
                Anchor = AnchorStyles.None
            }, 0, 3);

            _tableLayoutPanel.Controls.Add(new Label
            {
                Text = (_mostDeaths.Deaths > 1000000 ? $"Reference value: {Math.Round(_mostDeaths.Deaths / 1000000.0, 2)}M ({_mostDeaths.Name})" :
                                           $"Reference value: {Math.Round(_mostDeaths.Deaths / 1000.0, 2)}K ({_mostDeaths.Name})"),
                AutoSize = true,
                Font = font,
                Anchor = AnchorStyles.None
            }, 1, 3);

            _tableLayoutPanel.Controls.Add(new Label
            {
                Text = (_mostRecovered.Recovered > 1000000 ? $"Reference value: {Math.Round(_mostRecovered.Recovered / 1000000.0, 2)}M ({_mostRecovered.Name})" :
                                                  $"Reference value: {Math.Round(_mostRecovered.Recovered / 1000.0, 2)}K ({_mostRecovered.Name})"),
                AutoSize = true,
                Font = font,
                Anchor = AnchorStyles.None
            }, 2, 3);

            _tabPage.Padding = new Padding(30);
            _tabPage.Controls.Add(_tableLayoutPanel);

            UpdateChart(_mostConfirmed);
        }

        /// <summary>
        /// Returns the generated tab page control to be added in a form
        /// </summary>
        /// <returns>Generated tab page</returns>
        public TabPage GetPage()
        {
            return _tabPage;
        }

        /// <summary>
        /// Changes the active tab and statistics to match selected country
        /// </summary>
        /// <returns>Generated tab page</returns>
        public void OnClick(CountryInfoEx country)
        {
            UpdateChart(country);

            TabControl parent = _tabPage.Parent as TabControl;
            parent.SelectedIndex = 2;
        }


        private ChartValues<DateTimePoint> GetRegionChartValues(long[] region, DateTime _startDate)
        {
            ChartValues<DateTimePoint> values = new ChartValues<DateTimePoint>();

            for (int i = 0; i < region.Length; ++i)
                values.Add(new DateTimePoint(_startDate.AddDays(i), region[i]));

            return values;
        }

        /// <summary>
        /// This method will be called every time a new country is choosed from global View
        /// </summary>
        /// <param name="country"></param>
        private void UpdateChart(CountryInfoEx country)
        {


            foreach (DayInfo day in country.DaysInfo)
            {
                int index = (day.Date.ToDateTime() - _startDate).Days;
                if (index < 0)
                { // skip info from days that are before the chosen start date 
                    continue;
                }
                _confirmed[index] = day.Confirmed - day.Deaths - day.Recovered;
                _deaths[index] = day.Deaths;
                _recovered[index] = day.Recovered;
            }

            _stackedAreaDead.Values = GetRegionChartValues(_deaths, _startDate);
            _stackedAreaRecovered.Values = GetRegionChartValues(_recovered, _startDate);
            _stackedAreaConfirmed.Values = GetRegionChartValues(_confirmed, _startDate);

            _solidGaugeInfectionRage.Value = Math.Log(country.Confirmed);
            _solidGaugeDeathRate.Value = Math.Log(country.Deaths);
            _solidGaugeRecoveryRate.Value = Math.Log(country.Recovered);

            _solidGaugeInfectionRage.LabelFormatter = value =>
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
            
            _solidGaugeRecoveryRate.LabelFormatter = value =>
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
            
            _solidGaugeDeathRate.LabelFormatter = value =>
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

            _labelTitle.Text = country.Name;
        }

        private readonly int NumberOfDays; // infection rate evolution is followed on last {NUMBER_OF_DAYS} days
        private DateTime _startDate;

        private long[] _confirmed;
        private long[] _deaths;
        private long[] _recovered;

        private TableLayoutPanel _tableLayoutPanel = new TableLayoutPanel();
        private TabPage _tabPage = new TabPage("Country statistics");
        private Label _labelTitle;
     
        private LiveCharts.WinForms.CartesianChart _cartesianChartRegions = new LiveCharts.WinForms.CartesianChart();
        private LiveCharts.WinForms.SolidGauge _solidGaugeInfectionRage = new LiveCharts.WinForms.SolidGauge();
        private LiveCharts.WinForms.SolidGauge _solidGaugeDeathRate = new LiveCharts.WinForms.SolidGauge();
        private LiveCharts.WinForms.SolidGauge _solidGaugeRecoveryRate = new LiveCharts.WinForms.SolidGauge();

        private StackedAreaSeries _stackedAreaRecovered;
        private StackedAreaSeries _stackedAreaDead;
        private StackedAreaSeries _stackedAreaConfirmed;
    }
}