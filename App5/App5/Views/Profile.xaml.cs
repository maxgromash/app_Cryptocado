using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;
using SkiaSharp;
using System.Collections.Generic;

namespace App5.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage, INotifyPropertyChanged
    {

        public static int logoNumber;
        string difference;
        public string Difference
        {
            get
            {
                return difference;
            }
            set
            {
                difference = value;
                OnPropertyChanged("Difference");
            }
        }

        public static bool UpdateProfit = false;
        public static bool UpdateOrNot = false;
        public Profile()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            Avatar.Source = "ava" + logoNumber.ToString() + ".png";
            quot.Text = Data.DataCourses.qouts[logoNumber - 1];


            //Привязка балансов
            Balanc.BindingContext = App.user;
            Binding a = new Binding("Balance", BindingMode.TwoWay);
            Balanc.SetBinding(Label.TextProperty, a);

            SumBalanc.BindingContext = App.user;
            Binding u = new Binding("SumBalance", BindingMode.TwoWay);
            SumBalanc.SetBinding(Label.TextProperty, u);

            differ.BindingContext = this;
            Binding c = new Binding("Difference", BindingMode.TwoWay);
            differ.SetBinding(Label.TextProperty, c);

            //Создание инфографики
            UpdateCharts();
        }

        /// <summary>
        /// Метод обновляет инфографику по валютам
        /// </summary>
        void UpdateCharts()
        {
            string[] Colors = { "#522922", "#5B3C52", "#315E6E", "#347756", "#877F34", "#D5785B" };

            var entries = new List<Microcharts.Entry>();

          

            for (int i = 0; i < 6; i++)
            {
                float x = (float)(App.user.Amounts[i] * Data.DataCourses.coins[i].Lastprice);

                if (x != 0)
                {
                    entries.Add(new Microcharts.Entry(x)
                    {
                        Label = Data.DataCourses.coins[i].Name,
                        ValueLabel = (((int)x).ToString()) + "$",
                        Color = SKColor.Parse(Colors[i])
                    });
                }
            }

            int k = 0;
            for (int i = 0; i < 6; i++)
            {
                if (App.user.Amounts[i] != 0)
                    k++;
            }

            var entries2 = new List<Microcharts.Entry>();

            for (int i = 0; i < 6; i++)
            {
                float x = (float)(App.user.Amounts[i] * Data.DataCourses.coins[i].Lastprice);

                if (x != 0)
                {
                    int percent = ((int)(x / (App.user.SumBalance - App.user.Balance) * 1000)) / 10;
                    if (k == 1) percent = 100;
                    entries2.Add(new Microcharts.Entry(x)
                    {
                        Label = Data.DataCourses.coins[i].Name,
                        ValueLabel = percent.ToString() + "%",
                        Color = SKColor.Parse(Colors[i])
                    });
                    if (percent == 0)
                        entries2[i].ValueLabel = "<1%";
                }
            }

            if (entries.Count == 0)
            {
                chartRadar.IsVisible = false;
                chartDonut.IsVisible = false;
                info.IsVisible = true;
            }
            else
            {
                chartRadar.IsVisible = true;
                chartDonut.IsVisible = true;
                info.IsVisible = false;

                chartDonut.Chart = new DonutChart()
                {
                    Entries = entries
                };
                chartDonut.Chart.LabelTextSize = 45;


                chartRadar.Chart = new RadarChart()
                {
                    Entries = entries2
                };
                chartRadar.Chart.LabelTextSize = 55;
            }
        }

        void UpdateProfitMethod()
        {
            double current = App.user.SumBalance;
            double sm = 0;
            for (int i = 0; i < 6; i++)
            {
                sm += App.user.Amounts[i] * Data.DataCourses.coins[i].Lastprice;
            }
            sm += App.user.Balance;

            App.user.SumBalance = (int)sm;

            if (App.UpdateDiffer)
            {
                if (sm > current)
                {
                    differ.TextColor = Color.Green;
                    Difference = "(+" + ((int)sm - (int)current).ToString() + "$)";
                }
                if ((int)sm == (int)current)
                {
                    Difference = "";
                }
                if (sm < current)
                {
                    differ.TextColor = Color.Red;
                    Difference = "(-" + ((-1) * ((int)sm - (int)current)).ToString() + "$)";
                }
                App.UpdateDiffer = false;
            }

        }

        protected override void OnAppearing()
        {
            if (App.InternetProblem)
            {
                App.InternetProblem = false;
                DisplayAlert("Интернет", "Для торгов и обновления курсов необходим Инернет!", "ок");
            }
            if (UpdateOrNot)
            {
                UpdateCharts();
                UpdateOrNot = false;
            }
            if (UpdateProfit)
            {
                UpdateProfitMethod();
                UpdateProfit = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}