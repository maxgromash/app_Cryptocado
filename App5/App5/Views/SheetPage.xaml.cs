using App5.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App5.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SheetPage : ContentPage
    {
        /// <summary>
        /// Свойства и поля для корректного отображения курса
        /// </summary>
        Sheet CurrentSheet { get; set; }
        List<Sheet> Sheets { get; set; }
        List<Test> Quiz { get; set; }
        int CurrentPage { get; set; }

        /// <summary>
        /// Основной конструктор страницы
        /// </summary>
        /// <param name="sheets">Страницы</param>
        /// <param name="currentPage">Текущая страница</param>
        /// <param name="quiz">Тест</param>
        public SheetPage(List<Sheet> sheets, int currentPage, List<Test> quiz)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            CurrentPage = currentPage;
            Sheets = sheets;
            CurrentSheet = sheets[currentPage];
            BindingContext = CurrentSheet;
            number.Text = (currentPage + 1).ToString() + "/" + (sheets.Count);
            info.Text = CurrentSheet.Info;
            //Проверка: анимация или картинка
            if (CurrentSheet.Picture.Contains("json"))
            {
                animationViewd.Animation = CurrentSheet.Picture;
                animationViewd.IsVisible = true;
            }
            else
            {
                image.Source = CurrentSheet.Picture;
                animationViewd.IsVisible = false;
                image.IsVisible = true;
                
            }
            head.Text = CurrentSheet.Head;
            Quiz = quiz;
            if (currentPage + 1 == sheets.Count)
            {
                continueBut.Text = "Тест";
                continueBut.BackgroundColor = Color.IndianRed;
                continueBut.Clicked -= Button_Clicked_1;
                continueBut.Clicked += Button_Clicked_Quiz;
            }
            
        }

        /// <summary>
        /// Открытие текста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Clicked_Quiz(object sender, EventArgs e)
        {
            Navigation.PushAsync(new QuizPage(Quiz));
        }

        /// <summary>
        /// Возвращение на предыдущую страницу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Clicked(object sender, EventArgs e)
        {
            if (CurrentPage == 0)
                Navigation.PopToRootAsync();
            else
                Navigation.PopAsync();
        }

        /// <summary>
        /// Переход к следующей странице
        /// </summary>
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SheetPage(Sheets, CurrentPage + 1, Quiz));
        }

        private void number_Clicked(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }
    }
}