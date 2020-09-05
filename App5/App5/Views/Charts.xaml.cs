using App5.Models;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App5.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Charts : ContentPage
    {
        string url = "https://s.tradingview.com/widgetembed/?frameElementId=tradingview_7e467&symbol=BITFINEX%3ABTCUSD&interval=30&saveimage=1&toolbarbg=f1f3f6&studies=%5B%5D&hideideas=1&theme=White&style=1&timezone=America%2FNew_York&studies_overrides=%7B%7D&overrides=%7B%7D&enabled_features=%5B%5D&disabled_features=%5B%5D&locale=en&utm_source=www.kitco.com&utm_medium=widget&utm_campaign=chart&utm_term=BITFINEX%3ABTCUSD";
        public Charts()
        {
            InitializeComponent();
            coinList.ItemsSource = Data.DataCourses.coins;
            operationsList.ItemsSource = App.user.Operations;
            BindingContext = this;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        /// <summary>
        /// 
        /// </summary>
        private void coinList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            string currenturl = url;
            Coin coin = (Coin)e.Item;
            string s = "BTC";
            if (e.ItemIndex != 0)
            {
                s =  coin.Name;
                currenturl = url.Replace("BTC", s);
            }
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                Navigation.PushAsync(new ChartPage(currenturl, coin));
            }
            else
            {
                DisplayAlert("Интернет", "Для торгов и обновления курсов необходим Инернет!", "ок");
            }
        }

        /// <summary>
        /// Обновление курсов
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Данные</param>
        async private void Button_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                activity.IsRunning = true;
                activity.IsVisible = true;
                try
                {
                    await Task.Run(() => Data.DataCourses.UpdatePrices());
                }
                catch
                {
                    activity.IsRunning = false;
                    activity.IsVisible = false;
                    await DisplayAlert("Интернет", $"При обновление курса что-то пошло не так. Проверьте  соединение с интернетом.", "ок");
                    return;
                }
                activity.IsRunning = false;
                activity.IsVisible = false;
            }
            else
            {
                await DisplayAlert("Интернет", "Для торгов и обновления курсов необходим Инернет!", "ок");
            }
        }

        /// <summary>
        /// Вызов меню с подсказками
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            DisplayAlert("Информация", "Для просмотра графика и покупки необходимо нажать на одну из валют из списка. После этого откроется график и появится кнопка \"Оперции\".", "ок");
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CheckingPage());
        }
    }
}