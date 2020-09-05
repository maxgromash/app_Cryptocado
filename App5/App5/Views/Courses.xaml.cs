using App5.Data;
using Xamarin.Forms;

namespace App5.Views
{
    public partial class Courses : ContentPage
    {
        public Courses()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            courseList.ItemsSource = DataCourses.Courses;
        }

        async void GoToCourse(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new CoursePage(e.Item));
        }
    }
}