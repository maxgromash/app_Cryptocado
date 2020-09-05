using App5.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App5.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChartPage : ContentPage
    {
        Coin coin;
        /// <summary>
        /// Основной конструктор
        /// </summary>
        /// <param name="url">Ссылка на график</param>
        /// <param name="coin">Монета</param>
        public ChartPage(string url, Coin coin)
        {
            this.coin = coin;
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            trade.Source = url;
        }

        //Навигация
        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        private void but2_Clicked(object sender, EventArgs e)
        {
            if (App.user.Balance < 1 && App.user.Amounts[coin.Index] == 0)
            {
                DisplayAlert("Уведомление", $"У вас недостаточно средств для покупки и продажи данной валюты. \n Продайте другую валюту.", "ок");
            }
            else
            {
                Navigation.PushAsync(new BuySellPage(coin));
            }
        }
    }
}