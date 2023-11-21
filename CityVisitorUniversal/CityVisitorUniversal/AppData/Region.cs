using CityVisitorUniversal.ResLol;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace CityVisitorUniversal.AppData
{
    /// <summary>
    /// Регион Российской Федерации
    /// </summary>
    public class Regions : INotifyPropertyChanged
    {
        /// <summary>
        /// ИД Региона
        /// </summary>
        public int IdRegion { get; set; }
        /// <summary>
        /// Конструкция для базы данных
        /// </summary>
        public static int Id { get; set; }
        /// <summary>
        /// Имя региона 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ИД на карте для зарисовки
        /// </summary>
        public string IdRegionsMaps { get; set; }
        /// <summary>
        /// Изображение
        /// </summary>
        public ImageSource Image { get; set; }
        /// <summary>
        /// Путь к изображению
        /// </summary>
        public string PathImage { get; set; }
        /// <summary>
        /// Список городов региона
        /// </summary>
        public ObservableCollection<City> ListCities { get; set; } = new ObservableCollection<City>();
        /// <summary>
        /// Отрисовка процента посещения
        /// </summary>
        /// <param name="res"></param>
        public void SetVisitPercentage(double res)
        {
            VisitPercentage = string.Format("{0} %", Math.Round(res, 2));
        }
        /// <summary>
        /// Процесс регистрации города в регионе
        /// </summary>
        /// <param name="city"></param>
        public void AddCities(City city)
        {
            city.IdRegionsMaps = IdRegionsMaps;
            city.Regionid = IdRegion;
            ListCities.Add(city);
        }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="name">Имя региона</param>
        /// <param name="pathImg">путь к изображению (герб)</param>
        public Regions(string name, string pathImg)
        {
            Name = name;
            PathImage = pathImg;
            Image = pathImg;
            Id++;
            IdRegion = Id;
        }
        /// <summary>
        /// Функкция зарисовки региона на карте
        /// </summary>
        /// <param name="path">ИД региона на карте</param>
        /// <param name="color">Цвет расскраски</param>
        private void PaintRegionOnMaps(string path, Color color)
        {
            //получаем текст карты
            string text = System.IO.File.ReadAllText(App.DataSVGPath);
            //задаем текст поиска по нашему иду региона на карте
            string searchString = $"id=\"{path}\" />";
            //ищем первое вхождение
            int index = text.IndexOf(searchString);
            //обрезаем 100 символов
            string tmp = text.Substring(index - 100, 100);
            //теперь делаем более точный поиск
            searchString = "style";
            //находим нужный индекс
            int index1 = tmp.IndexOf(searchString);
            //обрезаем
            string tmp2 = tmp.Substring(index1 - searchString.Length + 17, searchString.Length + 2);
            //находим индекс предыдущего цвета
            int index2 = tmp.IndexOf(tmp2);
            //формируем итоговую карту
            string newLol = text.Substring(0, index - 100 + index2);
            System.IO.File.WriteAllText(App.DataSVGPath, string.Format("{0}{1}{2}", newLol, ColorToHex(color) + "fill-rule:nonzero;stroke:#202020;stroke-width:10.23px\"\n", text.Substring(index)));
        }
        /// <summary>
        /// Преобразовываем нужный нам цвет в формат представления HEX
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public string ColorToHex(Color color)
        {
            string tmp = string.Format("{0:X2}{1:X2}{2:X2}", (int)(color.R * 255.0), (int)(color.G * 255.0), (int)(color.B * 255.0));
            return "#" + tmp.ToLower() + ";";
        }
       
        /// <summary>
        /// Функция сохранения изменений в регионе
        /// </summary>
        public void SaveReg(City refreshCity)
        {
            //задаем индекс
            int cur = 0;
            foreach (City city in ListCities)
            {
                if (city.State != State.NotVisited)
                {
                    //если есть хот один посещенный город то индекс не равен 0
                    cur = 1;
                    break;
                }
            }
            //проверяем наличие посещенных городов
            if (cur != 0)
            {
                //Закрашиваем регион на карте цветом (как посещенный)
                PaintRegionOnMaps(IdRegionsMaps, Color.Green);
            }
            else
            {
                //Закрашиваем как непосещенный
                PaintRegionOnMaps(IdRegionsMaps, Color.White);
            }
            //создаем экземпляр класса для хранения метаданных в базе данных
            RegionsDB reg1 = new RegionsDB(this);
            //обновляем данные в базе данных
            App.Db.UpdateRegion(reg1);


            //делаем обновление для всех городов региона

          
            CityDB city1 = new CityDB(refreshCity, reg1.Id);
            city1.SaveCity();
        
        }
        public void AddCities(List<CityDB> cities)
        {
            foreach (CityDB CityDB in cities)
            {
                ListCities.Add(new City(CityDB));
            }
        }
        public Regions(RegionsDB reg)
        {
            IdRegion = reg.Id;
            Name = reg.Name;
            PathImage = reg.PathImage;
            Image = reg.PathImage;
            VisitPercentage = reg.VisitPercentage;
            IdRegionsMaps = reg.IdRegionsMaps;

        }
        public Regions() { }

        private string _visitPercentage { get; set; }
        /// <summary>
        /// Процент посещения
        /// </summary>
        public string VisitPercentage
        {
            get => _visitPercentage;
            set
            {
                _visitPercentage = value;
                OnPropertyChanged("VisitPercentage");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
