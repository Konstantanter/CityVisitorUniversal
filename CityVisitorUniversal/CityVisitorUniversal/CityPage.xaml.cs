using CityVisitorUniversal.AppData;
using CityVisitorUniversal.ResLol;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CityVisitorUniversal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CityPage : Xamarin.Forms.ContentPage
    {


        //public event EventHandler<EventArgs> CityVisitedChanged;

        public string CoatOfArmsImage { get; set; }

        public string NameCity { get; set; }
        public string NameReg { get; set; }
        City city;
        Regions myReg;
        public CityPage(City selectedCity, AppData.Regions region)
        {
     

            InitializeComponent();
            myReg = region;

            NameCity = $"город {selectedCity.Name}";
            NameReg = region.Name;
            CoatOfArmsImage = selectedCity.ImagePath;
             //BindingContext = selectedCity;
            city = selectedCity;


            State state = city.State;
            if(state == State.NotVisited)
            {
                picker.Title = "Не посещен";
            }
            if (state == State.Visited)
            {
                picker.Title = "Полноценно";
            }
            if (state == State.VisitedTransit)
            {
                picker.Title = "Проездом";
            }
            if (city.DataVisited != null)
            {
                datePicker.Date = DateTime.Parse(city.DataVisited);
            }
            Switcher.IsToggled = city.Magned;
            Switcher.Toggled += OnToggled;
            //picker.SelectedIndex = 0;

            datePicker.DateSelected += DatePicker_DateSelected;
            datePicker.MaximumDate = DateTime.Now;
            // Создание коллекции изображений
       
            // Добавьте больше изображений, если это необходимо

            // Привязка коллекции изображений к элементу управления ListView
            
            BindingContext = this;
        }

        void OnToggled(object sender, ToggledEventArgs e)
        {
            city.Magned = Switcher.IsToggled;
            CityDB CityDB = new CityDB(city, myReg.IdRegion);
            CityDB.SaveCity();
        }
        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            city.DataVisited = datePicker.Date.ToShortDateString();
            CityDB CityDB = new CityDB(city, myReg.IdRegion);
            CityDB.SaveCity();
        }
        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = picker.SelectedIndex;
            if (i == 0)
            {
                city.State = State.Visited;
            }
            if (i == 1)
            {
                city.State = State.VisitedTransit;
            }
            if (i == 2)
            {
                city.State = State.NotVisited;
            }
            city.DataVisited = datePicker.Date.ToShortDateString();
            int cur = myReg.ListCities.Count(a => a.State != State.NotVisited);
            double a1 = cur, a2 = myReg.ListCities.Count;
            myReg.SetVisitPercentage(a1 / a2 * 100.0);
            myReg.SaveReg(city);
        }

        private void SettingsIcon_Tapped(object sender, EventArgs e)
        {
            // Здесь можно добавить код для обработки нажатия на иконку настроек
            // Например, открыть страницу настроек или выполнить соответствующие действия
        }

        private void PhotoIcon_Tapped(object sender, EventArgs e)
        {
            // Здесь можно добавить код для обработки нажатия на иконку настроек
            // Например, открыть страницу настроек или выполнить соответствующие действия
        }

        private void GalleryIcon_Tapped(object sender, EventArgs e)
        {
            // Здесь можно добавить код для обработки нажатия на иконку настроек
            // Например, открыть страницу настроек или выполнить соответствующие действия
        }

        private void VideoIcon_Tapped(object sender, EventArgs e)
        {
            // Здесь можно добавить код для обработки нажатия на иконку настроек
            // Например, открыть страницу настроек или выполнить соответствующие действия
        }
    }
}