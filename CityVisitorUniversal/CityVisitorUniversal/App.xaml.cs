using CityVisitorUniversal.ResLol;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CityVisitorUniversal.AppData;

using System.IO;
using Android.Content.Res;

using System.Collections.Generic;
using System.Linq;

using System.Xml;
using CityVisitorUniversal.Svg;
using Java.Lang;
using System.Text.RegularExpressions;
using Android.Graphics;
using Android.OS;
using Android.Views;
using static Android.Resource;
using Android.Widget;
using Svg;
using System.Drawing.Drawing2D;


namespace CityVisitorUniversal
{
    public partial class App : Application
    {
        static DataBase db;


        public static string DataBasePath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "Citiesbase.db3");
        public static string DataSVGPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "Test3.svg");
        // public static string DataSVGPath1 = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Test3_1.svg");

        public static DataBase Db
        {
            get
            {
                if (db == null)
                {
                    db = new DataBase(DataBasePath);
                }
                return db;
            }
        }
       
        public App()
        {
            InitializeComponent();
            System.IO.File.Delete(DataSVGPath);
            //System.IO.File.Delete(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Test3_1.svg"));
            //System.IO.File.Delete(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Test3_1.svg"));
            //DeleteDataBase();

            if (!System.IO.File.Exists(DataSVGPath))
            {
                string content;
                AssetManager assets = Android.App.Application.Context.Assets;

                using (StreamReader sr = new StreamReader(assets.Open("Test4.svg")))
                {
                    content = sr.ReadToEnd();
                }
                System.IO.File.WriteAllText(DataSVGPath, content,System.Text.Encoding.UTF8);
            }


            SvgDocument svg =  SvgDocument.Open(DataSVGPath);
            PointF point = new PointF(10f, 10f);
            string svg1 = System.IO.File.ReadAllText(DataSVGPath);

            //// Получаем координаты клика
            //PointF clickPoint = new PointF(x, y);


            GraphicsPath graphicsPath = svgMLQZToGraphicsPath(svg1);
           // var region = GetRegionByCoordinates(point.X, point.Y);

            //string str = System.IO.File.ReadAllText(DataSVGPath, System.Text.Encoding.UTF8);
            //List<string> split = new List<string>();
            //using (var parse = new SVGDocument(str, "."))
            //{
            //    split = ((Aspose.Svg.SVGElement)parse.GetElementsByTagName("svg")[0]).GetAttribute("viewBox").Split(' ').ToList();

            //    // Work with the document here...
            //}
            //SVGDocument svg = new SVGDocument();
            //svg.LoadFromXaml(DataSVGPath);
            //List<string> split = new List<string>();
            //using (var stream = new FileStream(DataSVGPath, FileMode.Open, FileAccess.Read))
            //{
            //    // Initialize an SVG document from the stream
            //    using (var parse = new SVGDocument(stream, "."))
            //    {
            //        split = ((Aspose.Svg.SVGElement)parse.GetElementsByTagName("svg")[0]).GetAttribute("viewBox").Split(' ').ToList();
            //        // Work with the document
            //    }
            //}
            //using (var parse = new SVGDocument(DataSVGPath))
            //{
            //    String[] split = ((Aspose.Svg.SVGElement)parse.GetElementsByTagName("svg")[0]).GetAttribute("viewBox").Split(' ');
            //}
            //Aspose.Svg.SVGDocument parse = new Aspose.Svg.SVGDocument()



            // String[] split = (parse.GetElementsByTagName("svg").Item(0) as Element).Att

            // SvgDocument svg =  SvgDocument.Open(DataSVGPath);

            int cur=0;
            //MainPage = new MapsPage();
            //MainPage = new NavigationPage(new Moins());
            //Regions Adygea = new Regions("Республика Адыгея", "Adygea.png");
            //Adygea.IdRegionsMaps = "path16";
            //City Adygeisk = new City("Адыгейск", "Adygeisk.png");
            //MainPage = new CityPage(Adygeisk, Adygea);
        }

        private GraphicsPath svgMLQZToGraphicsPath(string svgString)
        {
            System.Drawing.Drawing2D.GraphicsPath graphicsPath = new System.Drawing.Drawing2D.GraphicsPath();
            float[] x = new float[4];
            float[] y = new float[4];
            string prev = "";
            string[] splits = svgString.Split(' ');
            for (int s = 0; s < splits.Length; s++)
            {
                if (splits[s].Substring(0, 1) == "M")
                {
                    x[0] = float.Parse(splits[s].Substring(1).Replace('.', ','));
                    y[0] = float.Parse(splits[s + 1].Replace('.', ','));
                    s++;
                    prev = "M";
                    graphicsPath.StartFigure();
                }
                else if (splits[s].Substring(0, 1) == "L")
                {
                    x[1] = float.Parse(splits[s].Substring(1).Replace('.', ','));
                    y[1] = float.Parse(splits[s + 1].Replace('.', ','));

                    graphicsPath.AddLine(x[0], y[0], x[1], y[1]);
                    x[0] = x[1]; // x[1] = new float();
                    y[0] = y[1]; //y[1] = new float();
                    s++;
                    prev = "L";
                }
                else if (splits[s].Substring(0, 1) == "Q")
                {
                    x[1] = x[0] + (2 / 3) * (float.Parse(splits[s].Substring(1).Replace('.', ',')) - x[0]);
                    y[1] = y[0] + (2 / 3) * (float.Parse(splits[s + 1].Replace('.', ',')) - y[0]);
                    x[3] = float.Parse(splits[s + 2].Replace('.', ','));
                    y[3] = float.Parse(splits[s + 3].Replace('.', ','));
                    x[2] = x[3] + (2 / 3) * (float.Parse(splits[s].Substring(1).Replace('.', ',')) - y[3]);
                    y[2] = y[3] + (2 / 3) * (float.Parse(splits[s + 1].Replace('.', ',')) - y[3]);
                    graphicsPath.AddBezier(x[0],y[0], x[1], y[1], x[2], y[2], x[3], y[3]);
                    //graphicsPath.AddBezier(new PointF(x[0], y[0]), new PointF(x[1], y[1]), new PointF(x[2], y[2]), new PointF(x[3], y[3]));
                    x[0] = x[3];
                    y[0] = y[3];
                    s = s + 3;
                    prev = "Q";
                }
                else if (splits[s].Substring(0, 1) == "Z")
                {
                    graphicsPath.CloseFigure();
                    if (splits[s].Length >= 2 && splits[s].Substring(0, 2) == "ZM")
                    {
                        x[0] = float.Parse(splits[s].Substring(2).Replace('.', ','));
                        y[0] = float.Parse(splits[s + 1].Replace('.', ','));
                        s++;
                        graphicsPath.StartFigure();
                        prev = "M";
                    }
                }
                else
                {
                    string ok = @"^[a-zA-Z]*$";
                    if (!Regex.IsMatch(splits[s + 1].Substring(0, 1), ok))
                    {
                        string replace = prev + splits[s + 1];
                        splits[s + 1] = replace;
                    }
                }
            }
            return graphicsPath;
        }
        //private string GetRegionByCoordinates(int x, int y, SvgDocument document)
        //{
            

        //    //foreach(var svgElem in document.Children)
        //    //{
        //    //    if(s)
        //    //}
        //    //// Проходим по всем элементам карты и ищем тот, на который был сделан клик
        //    //foreach (var svgElement in document.Children)
        //    //{
        //    //    // Проверяем, является ли элемент регионом
        //    //    if (svgElement is SvgPath path)
        //    //    {
        //    //        // Создаем графический путь из контура региона
        //    //        GraphicsPath graphicsPath = 

        //    //        // Проверяем, видима ли точка клика внутри контура региона
        //    //        if (graphicsPath.IsVisible(clickPoint))
        //    //        {
        //    //            // Возвращаем идентификатор региона (или другую информацию) в зависимости от вашей структуры данных
        //    //            return path.ID; // Например, если у вас есть идентификаторы регионов
        //    //        }
        //    //    }
        //    //}

        //    //// Если ни один регион не был найден, возвращаем null или пустую строку
        //    //return string.Empty;
        //}


        public static void DeleteDataBase()
        {
            System.IO.File.Delete(DataBasePath);
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

    }
}
