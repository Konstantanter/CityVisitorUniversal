using CityVisitorUniversal.AppData;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Svg.Skia;
using System;
using System.Collections.Generic;
using System.Xml;
using TouchTracking;
using Xamarin.Forms;
using SKPath = SkiaSharp.SKPath;

namespace CityVisitorUniversal
{



    public partial class MapsPage : ContentPage
    {


        List<SvgHelper> listSvgHelper = new List<SvgHelper>();
        SkiaSharp.SKPath parserSKPath(string str)
        {
            str = str.Replace("\"", "");
            str = str.Replace("d=", "");
            return SkiaSharp.SKPath.ParseSvgPathData(str);
        }
        public MapsPage()
        {
            InitializeComponent();
            random = new Random();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
            var tapGesture = new TapGestureRecognizer();
            XmlDocument svgDocument = new XmlDocument();
            svgDocument.Load(App.DataSVGPath);
            XmlNodeList pathNodes = svgDocument.GetElementsByTagName("path");

            foreach (XmlNode pathNode in pathNodes)
            {
                XmlAttribute idAttribute = pathNode.Attributes["id"];
                XmlAttribute dAttribute = pathNode.Attributes["d"];
                var skPath = parserSKPath(dAttribute.OuterXml);
                SvgHelper helper = new SvgHelper(pathNode.Attributes["id"].OuterXml, skPath);
                listSvgHelper.Add(helper);
            }
            listSvgHelper.RemoveAt(0);
            listSvgHelper.RemoveAt(0);
            listSvgHelper.RemoveAt(0);
            listSvgHelper.RemoveAt(0);
            listSvgHelper.RemoveAt(0);
            BindingContext = this;


        }
        SKPoint ConvertToPixel(TouchTrackingPoint pt)
        {
            return new SKPoint((float)(canvasView.CanvasSize.Width * pt.X / canvasView.Width),
                               (float)(canvasView.CanvasSize.Height * pt.Y / canvasView.Height));
        }
        void OnTouchEffectAction(object sender, TouchActionEventArgs args)
        {
            switch (args.Type)
            {
                case TouchActionType.Pressed:

                    var t1 = ConvertToPixel(args.Location);
                    int cur = 0;
                    //int cur = 1;
                    for (int i = 0; i < listSvgHelper.Count; i++)
                    {
                        if (listSvgHelper[i].TSKPath.Contains(t1.X, t1.Y))
                        {
                            cur = 1;
                            DisplayAlert("Информационное окно", $"Вы нажали на: {listSvgHelper[i].Name}", "ОК");
                        }
                    }

                    if (cur == 0)
                    {
                        DisplayAlert("Информационное окно", $"Вы нажали мимо региона", "ОК");
                    }
                    break;
            }
        }
        public void UpdateContent()
        {
            canvasView.InvalidateSurface();
        }
        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();
            // Загрузите SVG-файл
            using (SKSvg svg = new SKSvg())
            {
                svg.Load(App.DataSVGPath);

                // Масштабируйте SVG в соответствии с размерами холста
                float scaleX = info.Width / svg.Picture.CullRect.Width;
                float scaleY = info.Height / svg.Picture.CullRect.Height;
                SKMatrix matrix = SKMatrix.CreateScale(scaleX, scaleY);


                using (SKPaint paint = new SKPaint())
                {

                    //цвет
                    paint.Color = SKColors.Black;
                    // paint.ColorF = SKColors.Green;
                    //сглаживание
                    paint.IsAntialias = true;
                    //ширина обводки
                    paint.StrokeWidth = 1.5f;
                    //paint.
                    //paint.Color = new SKColor(50,50,50);
                    //paint.ColorF = new SKColor(66, 40, 55);
                    //стиль: SKPaintStyle.Stroke - линия, 
                    paint.Style = SKPaintStyle.Stroke;

                    for (int i = 0; i < listSvgHelper.Count; i++)
                    {
                        var transformPath = new SKPath();
                        transformPath.Rewind();

                        transformPath.AddPath(listSvgHelper[i].SKPath);
                        transformPath.Transform(matrix);
                        canvas.DrawPath(transformPath, paint);
                        listSvgHelper[i].TSKPath = transformPath;

                    }
                }
            }
        }



        public Random random;
        //private Color GetRandomColor()
        //{
        //    Color color = Color.FromRgb(random.Next(50, 100), random.Next(200, 255), random.Next(2, 255));
        //    return color;
        //}
        //public string ColorToHex(Color color)
        //{
        //    return string.Format("#{0:X6};", random.Next(0x1000000));
        //}

        //public string ColorToHex(Color color)
        //{
        //    string tmp = string.Format("{0:X2}{1:X2}{2:X2}", (int)(color.R * 255.0), (int)(color.G * 255.0), (int)(color.B * 255.0));
        //    return "#" + tmp.ToLower() + ";";
        //}
        private static void Change(ref string a, ref string b)
        {
            string temp = a;
            a = b;
            b = temp;
        }




    }

}
