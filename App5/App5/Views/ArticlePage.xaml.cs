using App5.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App5.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArticlePage : ContentPage
    {
        /// <summary>
        /// Основной конструктор
        /// </summary>
        /// <param name="arc">Статья для отображения</param>
        public ArticlePage(Article arc)
        {
            InitializeComponent();
            cards.ItemsSource = arc.Cards;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        /// <summary>
        /// Ообработчик кнопки "назад"
        /// </summary>
        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }
    }
}