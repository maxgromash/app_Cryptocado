using App5.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App5.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DefinitionPage : ContentPage
    {
        public DefinitionPage(Definition definition)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            name.Text = definition.Word;
            def.Text = "    " + definition.Def;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}