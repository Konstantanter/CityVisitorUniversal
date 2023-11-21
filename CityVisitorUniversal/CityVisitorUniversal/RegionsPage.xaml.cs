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
    public partial class RegionsPage : Xamarin.Forms.ContentPage
    {
        public ObservableCollection<Regions> RegionList { get; set; }



        public RegionsPage()
        {
      
            InitializeComponent();

            //App.DeleteDataBase();
            RegionList = new ObservableCollection<Regions>();
            if (!System.IO.File.Exists(App.DataBasePath))
                InitCities();
            Paintdata();
        }

        public async void Paintdata()
        {
            var regions = await App.Db.GetRegions();

            foreach (var region in regions)
            {
                Regions newReg = new Regions(region);

                var newListCities = await App.Db.GetCitiesFromRegion(region.Id);
                newReg.AddCities(newListCities);
                RegionList.Add(newReg);
            }
            listViewRegion.BindingContext = RegionList;
            BindingContext = this;
        }
        //protected override void OnAppearing()
        //{


        //    int cur = RegionList.Count();

        //    //var regions = await App.Db.GetRegions();

        //    //int cur = regions.Count();
        //    ////RegionList = new ObservableCollection<Regions>(regions);
        //    base.OnAppearing();
        //}
        async void ListView_OnItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if (e.Item is Regions selectedRegion)
            {
               
                await Navigation.PushAsync(new CitiesPage(selectedRegion));
                // Выполните необходимые действия с выбранным регионом (selectedRegion)
            }

    // Сбросьте выделение элемента списка
    ((Xamarin.Forms.ListView)sender).SelectedItem = null;
            //if (e.SelectedItem == null)
            //    return;

            //var selectedRegion = (Region)e.SelectedItem;
            //await Navigation.PushAsync(new CitiesPage(selectedRegion));

            //((ListView)sender).SelectedItem = null;
        }
    }
}