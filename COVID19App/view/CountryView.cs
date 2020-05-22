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

            // build the chart with numbers of infected people 
            _recoveredStackedArea = new StackedAreaSeries
            {
                Title = "Recovered",
                Values = GetRegionChartValues(new long[1], _startDate),
                LineSmoothness = 0
            };
            _deathsStackedArea = new StackedAreaSeries
            {
                Title = "Deaths",
                Values = GetRegionChartValues(new long[1], _startDate),
                LineSmoothness = 0
            };
            _confirmedStackedArea = new StackedAreaSeries
            {
                Title = "Active",
                Values = GetRegionChartValues(new long[1], _startDate),
                LineSmoothness = 0
            };

            _recoveredStackedArea.Fill = System.Windows.Media.Brushes.MediumSeaGreen;
            _deathsStackedArea.Fill = System.Windows.Media.Brushes.Crimson;
            _confirmedStackedArea.Fill = System.Windows.Media.Brushes.Gold;
            _chartRegions.Series = new SeriesCollection
            {
               _deathsStackedArea,
               _confirmedStackedArea,
               _recoveredStackedArea,
            };

            _chartRegions.AnimationsSpeed = new TimeSpan(animationSpeedGraph);
            _gaugeDeathRate.AnimationsSpeed = new TimeSpan(animationSpeedGauge);
            _gaugeInfectionRate.AnimationsSpeed = new TimeSpan(animationSpeedGauge);
            _gaugeRecoveryRate.AnimationsSpeed = new TimeSpan(animationSpeedGauge);

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

            // build the solid gauge for confirmed infected people rate
            _gaugeInfectionRate.From = 0;
            _gaugeInfectionRate.To = Math.Log(_mostConfirmed.Confirmed);
            _gaugeInfectionRate.Value = 0;
            _gaugeInfectionRate.Base.LabelsVisibility = Visibility.Hidden;
            _gaugeInfectionRate.LabelFormatter = value => value.ToString();
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
            _gaugeDeathRate.To = Math.Log(_mostDeaths.Deaths);
            _gaugeDeathRate.Value = 0;
            _gaugeDeathRate.Base.LabelsVisibility = Visibility.Hidden;
            _gaugeDeathRate.LabelFormatter = value => value.ToString();
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
            _gaugeRecoveryRate.To = Math.Log(_mostRecovered.Recovered);
            _gaugeRecoveryRate.Value = 0;
            _gaugeRecoveryRate.Base.LabelsVisibility = Visibility.Hidden;
            _gaugeRecoveryRate.LabelFormatter = value => value.ToString();
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
            _labelTitle = new Label
            {
                Text = "",
                AutoSize = true,
                Font = fontTitle,
                Anchor = AnchorStyles.None
            };
            _layoutPanel.Controls.Add(_labelTitle, 1, 0);

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
                Text = (_mostConfirmed.Confirmed > 1000000 ? $"Reference value: {Math.Round(_mostConfirmed.Confirmed / 1000000.0, 2)}M ({_mostConfirmed.Name})" :
                                                  $"Reference value: {Math.Round(_mostConfirmed.Confirmed / 1000.0, 2)}K ({_mostConfirmed.Name})"),
                AutoSize = true,
                Font = font,
                Anchor = AnchorStyles.None
            }, 0, 3);

            _layoutPanel.Controls.Add(new Label
            {
                Text = (_mostDeaths.Deaths > 1000000 ? $"Reference value: {Math.Round(_mostDeaths.Deaths / 1000000.0, 2)}M ({_mostDeaths.Name})" :
                                           $"Reference value: {Math.Round(_mostDeaths.Deaths / 1000.0, 2)}K ({_mostDeaths.Name})"),
                AutoSize = true,
                Font = font,
                Anchor = AnchorStyles.None
            }, 1, 3);

            _layoutPanel.Controls.Add(new Label
            {
                Text = (_mostRecovered.Recovered > 1000000 ? $"Reference value: {Math.Round(_mostRecovered.Recovered / 1000000.0, 2)}M ({_mostRecovered.Name})" :
                                                  $"Reference value: {Math.Round(_mostRecovered.Recovered / 1000.0, 2)}K ({_mostRecovered.Name})"),
                AutoSize = true,
                Font = font,
                Anchor = AnchorStyles.None
            }, 2, 3);

            _page.Padding = new Padding(30);
            _page.Controls.Add(_layoutPanel);

            UpdateChart(_mostConfirmed);
        }

        /// <summary>
        /// Returns the generated tab page control to be added in a form
        /// </summary>
        /// <returns>Generated tab page</returns>
        public TabPage GetPage()
        {
            return _page;
        }

        /// <summary>
        /// Changes the active tab and statistics to match selected country
        /// </summary>
        /// <returns>Generated tab page</returns>
        public void OnClick(CountryInfoEx country)
        {
            UpdateChart(country);

            TabControl parent = _page.Parent as TabControl;
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
            long[] confirmed = new long[NumberOfDays];
            long[] deaths = new long[NumberOfDays];
            long[] recovered = new long[NumberOfDays];

            foreach (DayInfo day in country.DaysInfo)
            {
                int index = (day.Date.ToDateTime() - _startDate).Days;
                if (index < 0)
                { // skip info from days that are before the chosen start date 
                    continue;
                }
                confirmed[index] = day.Confirmed - day.Deaths - day.Recovered;
                deaths[index] = day.Deaths;
                recovered[index] = day.Recovered;
            }

            _deathsStackedArea.Values = GetRegionChartValues(deaths, _startDate);
            _recoveredStackedArea.Values = GetRegionChartValues(recovered, _startDate);
            _confirmedStackedArea.Values = GetRegionChartValues(confirmed, _startDate);

            _gaugeInfectionRate.Value = Math.Log(country.Confirmed);
            _gaugeDeathRate.Value = Math.Log(country.Deaths);
            _gaugeRecoveryRate.Value = Math.Log(country.Recovered);

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

            _labelTitle.Text = country.Name;
        }

       
        private readonly int NumberOfDays; // infection rate evolution is followed on last {NUMBER_OF_DAYS} days
        private DateTime _startDate;
        private DateTime _todayDate;

        private TableLayoutPanel _layoutPanel = new TableLayoutPanel();
        private TabPage _page = new TabPage("Country statistics");
        private Label _labelTitle;
     
        private LiveCharts.WinForms.CartesianChart _chartRegions = new LiveCharts.WinForms.CartesianChart();
        private LiveCharts.WinForms.SolidGauge _gaugeInfectionRate = new LiveCharts.WinForms.SolidGauge();
        private LiveCharts.WinForms.SolidGauge _gaugeDeathRate = new LiveCharts.WinForms.SolidGauge();
        private LiveCharts.WinForms.SolidGauge _gaugeRecoveryRate = new LiveCharts.WinForms.SolidGauge();
        private StackedAreaSeries _recoveredStackedArea;
        private StackedAreaSeries _deathsStackedArea;
        private StackedAreaSeries _confirmedStackedArea;
    }
}