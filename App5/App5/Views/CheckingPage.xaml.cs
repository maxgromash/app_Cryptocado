using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App5.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckingPage : ContentPage
    {
        public CheckingPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            image.Source = Data.DataCourses.tasks[App.user.CurrentTask].TaskStart;

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        /// <summary>
        /// Ответ "Купить"
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Данные</param>
        private void GiveAnswer(object sender, EventArgs e)
        {
            if (image.Source.ToString() == "File: " + Data.DataCourses.tasks[App.user.CurrentTask].TaskStart)
            {
                if (((Button)sender).Text == "Купить")
                    if (Data.DataCourses.tasks[App.user.CurrentTask].Answer)
                        ((Button)sender).BackgroundColor = Color.Green;
                    else
                    {
                        Vibration.Vibrate();
                        ((Button)sender).BackgroundColor = Color.Red;
                    }
                else
                    if (!Data.DataCourses.tasks[App.user.CurrentTask].Answer)
                        ((Button)sender).BackgroundColor = Color.Green;
                    else
                    {
                        Vibration.Vibrate();
                    Vibration.Vibrate(); 
                    ((Button)sender).BackgroundColor = Color.Red;
                    }

                image.Source = Data.DataCourses.tasks[App.user.CurrentTask].TaskFinish;
                if (App.user.CurrentTask == Data.DataCourses.tasks.Count-1)
                    App.user.CurrentTask = 0;
                else App.user.CurrentTask ++;
                App.SaveData();
            }
            else
            {
                buy.BackgroundColor = Color.White;
                sell.BackgroundColor = Color.White;
                image.Source = Data.DataCourses.tasks[App.user.CurrentTask].TaskStart;
            }
        }
    }
}