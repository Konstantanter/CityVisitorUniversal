using CityVisitorUniversal.AppData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms.Xaml;

namespace CityVisitorUniversal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CitiesPage : Xamarin.Forms.ContentPage
    {
        
        public string TitleCities { get; set; }
         public string PathImage { get; set; }
         public ObservableCollection<City> CityList { get; set; }

        public CitiesPage()
        {
            InitializeComponent();
        }

        Regions tmpreg;
        public CitiesPage(Regions selectedRegion)
        {
            InitializeComponent();

            TitleCities = selectedRegion.Name;

            PathImage = selectedRegion.PathImage;
            tmpreg = selectedRegion;
            listViewCity.BindingContext = selectedRegion.ListCities;
            BindingContext = this;
        }
        async void ListView_OnItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if (e.Item is City selectedCity)
            {

                await Navigation.PushAsync(new CityPage(selectedCity, tmpreg));
            }
            ((Xamarin.Forms.ListView)sender).SelectedItem = null;

        }
    }
}