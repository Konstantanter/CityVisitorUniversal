


using SkiaSharp;
using SkiaSharp.Views.Forms;
using Svg.Skia;
using System;
using Xamarin.Forms;

namespace CityVisitorUniversal
{

    public partial class MapsPage : ContentPage
    {
        public static readonly BindableProperty ImageHeightProperty = BindableProperty.Create(nameof(ImageHeight), typeof(double), typeof(MapsPage), default(double));



        public double ImageHeight
        {
            get => (double)GetValue(ImageHeightProperty);
            set => SetValue(ImageHeightProperty, value);
        }
        public string ImgSource { get; set; }
        public string SourceImg { get; set; }
        public MapsPage()
        {



            InitializeComponent();


            //Img1.Source = ImageSource.FromFile(pa thToRead);
            random = new Random();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;


            // Извлечение всех путей из SVG

            //Img1.Source = 

            //var pinchGesture = new PinchGestureRecognizer();
            //pinchGesture.PinchUpdated += OnPinchUpdated;
            //Img1.GestureRecognizers.Add(pinchGesture);
            // Загрузка SVG-файла
            //var svgImageSource = new SvgImageSource
            //{
            //    SvgAssembly = typeof(YourPage).Assembly,
            //    SvgPath = "YourNamespace.your_image.svg"
            //};
            //svgImage.Source = svgImageSource;
            //(Application.Current.MainPage as TabbedPage)?.Children[1].Appearing += OnAppearing ;
            BindingContext = this;

        }

        public void UpdateContent()
        {
            canvasView.InvalidateSurface();
        }
        //void Function(string path, Color color)
        //{




        //    string text = System.IO.File.ReadAllText(App.DataSVGPath);


        //    string searchString = $"id=\"{path}\" />";
        //    int index = text.IndexOf(searchString);

        //    string tmp = text.Substring(index - 100, 100);

        //    string tmp1 = "style";

        //    int index1 = tmp.IndexOf(tmp1);

        //    string tmp2 = tmp.Substring(index1 - tmp1.Length + 17, tmp1.Length + 2);
        //    int index2 = tmp.IndexOf(tmp2);



        //    // MessageBox.Show(text.Substring(index - 100 + index2, index2));

        //    string newLol = text.Substring(0, index - 100 + index2);


        //    newLol = String.Format("{0}{1}{2}", newLol, ColorToHex(color).ToLower() + "fill-rule:nonzero;stroke:#202020;stroke-width:10.23px\"\n", text.Substring(index));



        //    System.IO.File.WriteAllText(App.DataSVGPath, newLol);



        //    canvasView.InvalidateSurface();
        //}



       // private readonly float currentScale = 1f;





        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            //var canvas = e.Surface.Canvas;
            ////canvas.Clear();


            //// Рисуем карту России из SVG
            ////canvas.DrawPicture(canvasView.Picture);

            //// Добавляем обработчики событий для нажатий на регионы
            //canvasView.Touch += (touchSender, touchEvent) =>
            //{
            //    // Определяем, на какой регион нажал пользователь
            //    SKPoint touchPoint = touchEvent.Location;
            //    if (IsPointInRegion1(touchPoint))
            //    {
            //        // Пользователь нажал на регион 1
            //    }

            //    // И так далее для остальных регионов
            //};
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            // Загрузите SVG-файл
            using (SKSvg svg = new SKSvg())
            {
                svg.Load(App.DataSVGPath);

                // Масштабируйте SVG в соответствии с размерами холста
                float scaleX = info.Width / svg.Picture.CullRect.Width;
                float scaleY = info.Height / svg.Picture.CullRect.Height;
                SKMatrix matrix = SKMatrix.CreateScale(scaleX, scaleY);




                //foreach (var t in svg.Model.Commands)
                //{
                //    if(t is ShimSkiaSharp.DrawPathCanvasCommand)
                //    {
                //        var l = (t as ShimSkiaSharp.DrawPathCanvasCommand);
                //        list.Add(l.Path);
                //    }
                //}

                // Отобразите SVG на холсте
                canvas.Clear();
                canvas.DrawPicture(svg.Picture, ref matrix);

            }
            //DisplayAlert("fwe", "fgsdfgfdsg", "hguifdsag");
        }
        private void OnCanvasViewTouch(object sender, SKTouchEventArgs e)
        {
            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:

                    float curE = e.Location.X;

                    float curE1 = e.Location.X;
                    //foreach(var t in list)
                    //{
                    //    if(t.Commands.Contains()
                    //}
                    // координаты можно считать из события
                    // это свойства (e.Location.X, e.Location.Y);
                    // ваш код
                    break;
            }
        }
        //private void OnTouch(object sender, SKTouchEventArgs e)
        //{
        //    switch (e.ActionType)
        //    {
        //        case SKTouchAction.Pressed:

        //            // координаты можно считать из события
        //            // это свойства (e.Location.X, e.Location.Y);
        //            // ваш код
        //            break;
        //        case SKTouchAction.Moved:
        //            break;
        //        case SKTouchAction.Released:
        //            break;
        //        case SKTouchAction.Cancelled:
        //            break;
        //    }

        //    // запрашиваем перерисовку
        //    if (e.InContact)
        //        ((SKCanvasView)sender).InvalidateSurface();

        //    // событие обработано
        //    e.Handled = true;
        //}
        //private bool IsPointInRegion1(SKPoint point)
        //{
        //    SKPath regionPath = new SKPath();
        //    // Добавление точек и линий в путь для представления контура региона

        //    bool isPointInside = regionPath.Contains(point);
        //}
        public Random random;
        private Color GetRandomColor()
        {
            Color color = Color.FromRgb(random.Next(50, 100), random.Next(200, 255), random.Next(2, 255));
            return color;
        }
        public string ColorToHex(Color color)
        {
            return string.Format("#{0:X6};", random.Next(0x1000000));
        }

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
        //private void OnColorRegionClicked(object sender, EventArgs e)
        //{



        //    Function($"path74", GetRandomColor());

        //    //Img1.Source = null; // Сбросить источник данных
        //    //Img1.Source = ImageSource.FromFile(pathToRead); 


        //    // Img1.In
        //}



        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            ImageHeight = height / 2;
        }


    }

}
