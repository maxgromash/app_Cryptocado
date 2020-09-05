using App5.Models;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App5.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuizPage : ContentPage
    {
        int countOfAnswers = 0;
        List<Test> Quiz { get; set; }
        public QuizPage(List<Test> quiz)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            Quiz = quiz;
            BindingContext = this;
            courseList.ItemsSource = Quiz;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Button but = (Button)sender;
            int index = int.Parse(((Label)((StackLayout)but.Parent).Children[0]).Text[0].ToString()) - 1;
            string correctAnswer = "";
            switch (Quiz[index].Correct)
            {
                case (1):
                    correctAnswer = Quiz[index].Ans1;
                    break;
                case (2):
                    correctAnswer = Quiz[index].Ans2;
                    break;
                case (3):
                    correctAnswer = Quiz[index].Ans3;
                    break;
                case (4):
                    correctAnswer = Quiz[index].Ans4;
                    break;
            }
            if (but.Text == correctAnswer)
            {
                ((Button)sender).BackgroundColor = Color.LightGreen;
                ((Button)sender).TextColor = Color.Black;
                foreach (var a in ((StackLayout)((Button)sender).Parent).Children)
                    a.IsEnabled = false;
                ((Button)sender).IsEnabled = true;
                ((Button)sender).Clicked -= Button_Clicked;
                countOfAnswers++;
            }
            else
            {
                ((Button)sender).BackgroundColor = Color.DarkRed;
            }

            if (countOfAnswers == Quiz.Count)
                GoBack();
        }
        async void GoBack()
        {
            Vibration.Vibrate();
            await DisplayAlert("Уведомление", "Подравляем, вы успешно завершили урок", "ок");
            await Navigation.PopToRootAsync();
        }

    }
}