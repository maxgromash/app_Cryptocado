using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App5.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Articles : ContentPage
    {
        /// <summary>
        /// Основнгой коструктор
        /// </summary>
        public Articles()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        //Обработка нажатий на статьи
        async private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await ((Frame)sender).FadeTo(0, 200);
            int a = (int)((Frame)sender).MinimumHeightRequest;
            await Navigation.PushAsync(new ArticlePage(Data.DataCourses.Articles[a]));
        }

        //Установка базовой прозрачности (анимация открытия ставит 0)
        protected override void OnAppearing()
        {
            a1.Opacity = 0.85;
            a2.Opacity = 0.85;
            a3.Opacity = 0.85;
            a4.Opacity = 0.85;
            a5.Opacity = 0.85;
            a6.Opacity = 0.85;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Dictionary());
        }
    }
}