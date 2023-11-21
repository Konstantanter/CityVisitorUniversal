using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CityVisitorUniversal.AppData
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Moins : TabbedPage
    {
      



        public Moins()
        {
            BarBackgroundColor = Color.Gray; // цвет фона toolbar
            BarTextColor = Color.White; // цвет текста на toolbar
            SelectedTabColor = Color.Red; // цвет выбранной вкладки

            InitializeComponent();
            Children.Add(new RegionsPage());
            Children.Add(new MapsPage());

            Children[0].Title = "Регионы России";
            Children[1].Title = "Карта России";

            // Получить ссылку на TabbedPage


            this.CurrentPageChanged += PageChanged;
        }

        void PageChanged(object sender, EventArgs args)
        {
            if (sender is MapsPage)
            {
                (Children[1] as MapsPage).UpdateContent();
            }
        }


    }
}