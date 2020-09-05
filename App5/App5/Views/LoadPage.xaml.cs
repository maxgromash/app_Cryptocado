using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace App5.Views
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadPage : ContentPage
    {
        public static Random rand = new Random();
        public LoadPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            DoIt();
        }
        async void DoIt()
        {
            //Сделано, для корректного отображения анимации
            Image image = new Image();
            await image.RotateTo(0, 2450);
            if (Data.DataCourses.coins[5].Max24 == 0)
            {
                await image.RotateTo(0, 1400);
            }

            //Data.DataCourses.UpdatePrices();
            //Настроим страницы со вкладками
            var tabbedPage = new Xamarin.Forms.TabbedPage();
            tabbedPage.BarTextColor = Color.Black;
            tabbedPage.BarBackgroundColor = Color.White;
            tabbedPage.SelectedTabColor = Color.Black;
            tabbedPage.On<Android>().SetOffscreenPageLimit(2).SetIsSwipePagingEnabled(false);
            tabbedPage.On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            Profile.logoNumber = rand.Next(1, 9);

            //Создадим необходимые вкладки и задаим им заголовки и иконок
            var articlesPage = new NavigationPage(new Articles());
            articlesPage.Title = "Термины";
            articlesPage.IconImageSource = "articles.png";

            var coursesPage = new NavigationPage(new Courses());
            coursesPage.Title = "Обучение";
            coursesPage.IconImageSource = "courses.png";

            var chartsPage = new NavigationPage(new Charts());
            chartsPage.Title = "Торги";
            chartsPage.IconImageSource = "mycourses.png";

            var profilePage = new NavigationPage(new Profile());
            profilePage.Title = "Профиль";
            profilePage.IconImageSource = "profile.png";

            //добавим страницы во вкладки
            tabbedPage.Children.Add(profilePage);
            tabbedPage.Children.Add(chartsPage);
            tabbedPage.Children.Add(coursesPage);
            tabbedPage.Children.Add(articlesPage);

            Xamarin.Forms.Application.Current.MainPage = tabbedPage;
        }
    }
}
