using App5.Models;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace App5.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuySellPage : ContentPage
    {
        Coin coin;
        public BuySellPage(Coin coin) 
        {
            this.coin = coin;
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            cryptImage.Source = coin.Logo;
            slider.Minimum = 0;

            if (App.user.Balance >= 1)
            {
                max.Text = App.user.Balance.ToString();
                slider.Maximum = App.user.Balance;
                slider.Value = (int)App.user.Balance / 2;
            }
            else
            {
                toggle.IsToggled = true;
                slider.Value = slider.Maximum / 2.0;
                slider.ValueChanged -= slider_ValueChanged;
                slider.ValueChanged += slider_ValueChanged2;
            }
        }

        /// <summary>
        /// Вернуть к началу
        /// </summary>
        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }

        /// <summary>
        /// Вернуться к графику
        /// </summary>
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        /// <summary>
        /// Обработка продажи валюты
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Данные</param>
        async private void sell_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {  
                await DisplayAlert("Уведомление", $"Нет доступа к интернету! Восстановите соединение и попробуйте снова.", "ок");
                await Navigation .PopToRootAsync();
            }
            if (slider.Value == 0)
            {
                await DisplayAlert("Уведомление", $"Выставите ненулевое значение", "ок");
                return;
            }

            activity.IsRunning = true;
            activity.IsVisible = true;
            ((Button)sender).IsEnabled = false;

            try
            {
                await Task.Run(() => Data.DataCourses.UpdatePrices());
            }
            catch (Exception)
            {
                ((Button)sender).IsEnabled = true;
                activity.IsRunning = false;
                activity.IsVisible = false;
                await DisplayAlert("Интернет", $"При обновление курса что-то пошло не так. Проверьте  соединение с интернетом.", "ок");
                return;
            }

            int x = (int)(slider.Value * Data.DataCourses.coins[coin.Index].Lastprice);
            App.user.Balance += x;
            App.user.Amounts[coin.Index] -= slider.Value;
            double y = ((int)(slider.Value * 10000.0)) / 10000.0;
            //Сохранение оперции в историю
            App.user.Operations.Insert(0, new Operation("Продажа", coin.Name, y, x));
            App.SaveData();
            //Обновление инфографики
            Profile.UpdateOrNot = true;

            activity.IsRunning = false;
            activity.IsVisible = false;
            Vibration.Vibrate();
            await DisplayAlert("Уведомление", $"Валюта продана.\nБаланс: {App.user.Balance}", "ок");
            ((Button)sender).IsEnabled = true;
            await Navigation.PopToRootAsync();
        }

        /// <summary>
        /// Покупка валюты
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Данные</param>
        async private void Button_Clicked_3(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Уведомление", $"Нет доступа к интернету! Восстановите соединение и попробуйте снова.", "ок");
                await Navigation.PopToRootAsync();
                return;
            }
            if ((int)slider.Value == 0)
            {
                await DisplayAlert("Уведомление", $"Выставите ненулевое значение", "ок");
                return;
            }

            ((Button)sender).IsEnabled = false;
            activity.IsRunning = true;
            activity.IsVisible = true;

            try
            {
                await Task.Run(() => Data.DataCourses.UpdatePrices());
            }
            catch (Exception)
            {
                ((Button)sender).IsEnabled = true;
                activity.IsRunning = false;
                activity.IsVisible = false;
                await DisplayAlert("Интернет", $"При обновление курса что-то пошло не так. Проверьте  соединение с интернетом.", "ок");
                return;
            }

            double amount = ((int)slider.Value) / Data.DataCourses.coins[coin.Index].Lastprice;
            App.user.Amounts[coin.Index] += amount;
            App.user.Balance -= (int)slider.Value;
            amount = ((int)(amount * 10000.0)) / 10000.0;
            //Сохранение оперции в историю
            App.user.Operations.Insert(0, new Operation("Покупка", coin.Name, amount, (int)slider.Value));
            App.SaveData();
            //Обновление инфографики
            Profile.UpdateOrNot = true;

            activity.IsRunning = false;
            activity.IsVisible = false;
            Vibration.Vibrate();
            await DisplayAlert("Уведомление", $"Валюта куплена.\nБаланс: {App.user.Balance}", "ок");
            ((Button)sender).IsEnabled = true;
            await Navigation.PopToRootAsync();
        }
        
        /// <summary>
        /// Смена режима покупка\продажа
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Данные</param>
        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if (((Switch)sender).IsToggled == false)
            {
                if (App.user.Balance >= 1)
                {
                    buy.BorderWidth = 3;
                    sell.BorderWidth = 1;
                    buy.IsEnabled = true;
                    sell.IsEnabled = false;
                    slider.ValueChanged -= slider_ValueChanged2;
                    slider.ValueChanged += slider_ValueChanged;

                    max.Text = App.user.Balance.ToString();
                    slider.Maximum = App.user.Balance;
                    slider.Value = slider.Maximum / 2.0;
                    currentImage.Source = "usd.png";
                   
                }
                else
                {
                    DisplayAlert("Уведомление", "Недостаточно валюты для покупки!", "ок");
                    ((Switch)sender).IsToggled = true;
                }
            }
            else
            {
                if (App.user.Amounts[coin.Index] != 0)
                {
                    buy.BorderWidth = 1;
                    sell.BorderWidth = 3;
                    buy.IsEnabled = false;
                    sell.IsEnabled = true;
                    slider.ValueChanged -= slider_ValueChanged;
                    slider.ValueChanged += slider_ValueChanged2;

                    double a = App.user.Amounts[coin.Index];
                    max.Text = $"{a:f4}";
                    slider.Maximum = App.user.Amounts[coin.Index];
                    slider.Value = slider.Maximum / 2.0;
                    currentImage.Source = coin.Logo;
                }
                else
                {
                    DisplayAlert("Уведомление", "Недостаточно валюты для продажи!", "ок");
                    ((Switch)sender).IsToggled = false;
                }
            }
        }

        /// <summary>
        /// Обработчик ползунка при продаже
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Данные</param>
        private void slider_ValueChanged2(object sender, ValueChangedEventArgs e)
        {
            crypto.Text = $"{e.NewValue:F4}";
            int btc = (int)((e.NewValue) * Data.DataCourses.coins[coin.Index].Lastprice);
            usd.Text = $"{btc}";
        }

        /// <summary>
        /// Обработчик ползунка при покупке
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Данные</param>
        private void slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            usd.Text = ((int)e.NewValue).ToString();
            double btc = ((int)e.NewValue) / coin.Lastprice;
            crypto.Text = $"{btc:f6}";
        }
    }
}