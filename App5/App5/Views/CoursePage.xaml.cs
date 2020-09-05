using App5.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App5.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursePage : ContentPage
    {
        Course CurCourse;
        public CoursePage(object e)
        {
            InitializeComponent();
            CurCourse = (Course)e;
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = CurCourse;
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SheetPage(CurCourse.Sheets, 0, CurCourse.Quiz));
        }

        async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}