using App5.Models;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App5.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dictionary : ContentPage
    {
        public Dictionary()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            list.ItemsSource = App.words;
        }

        void Handle_SearchButtonPressed(object sender, System.EventArgs e)
        {
            var newList = App.words.Where(c => c.Word.ToLower().Contains(CountriesSearchBar.Text.ToLower()));
            list.ItemsSource = newList;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void list_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new DefinitionPage((Definition)e.Item));
        }
    }
}