using Android.Content.Res;
using Android.Content;
using Android.Graphics;
using Android.Hardware.Lights;
using Android.Views;
using Android.Widget;
using CityVisitorUniversal.Svg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Xamarin.Forms;
using Android.Util;
using static Android.Resource;

namespace CityVisitorUniversal
{
    public class View1 : ContentView
    {
        private float mDx;
        private float mDy;


        private bool autoSize;
        private int deltaX;
        private int deltaY;
        private Bitmap mBitmap;
        private Bitmap mBitmapHelper;
        private int mBorderColor;
        private float mBorderThickness;
        private Canvas mCanvas;
        private Canvas mCanvasHelper;
        private OnInteractiveViewClickListener mClickListener;
        private float mCurrentScale;
        private int mCurrentScaleState;
        private int mCurrentScaledOffsetX;
        private int mCurrentScaledOffsetY;

        private bool mFinalScaled;
        private GestureDetector mGestureDetector;


        private Paint mPaint;

        private ScaleGestureDetector mScaleDetector;
        private int mScrollX;
        private int mScrollY;
        private Scroller mScroller;
        private int mWaterColor;
        private int mLandColor;
        private List<LandForMap> mLands = new List<LandForMap>();

        int getFillColor(SVGPath svgPath)
        {
            int i = this.mLandColor;
            foreach (LandForMap landForMap in this.mLands)
            {
                if (landForMap.code.Equals(svgPath.getId()))
                {
                    return landForMap.color;
                }
            }
            return i;
        }
        public interface OnInteractiveViewClickListener
        {
            void onClick(string id);
        }
        public void setOnInteractiveClickListener(OnInteractiveViewClickListener listener)
        {
            this.mClickListener = listener;
        }

        string JavaStyleSubstring(string s, int beginIndex, int endIndex)
        {
            // simulates Java substring function
            int len = endIndex - beginIndex;
            return s.Substring(beginIndex, len);
        }
        private void parseDrawData(string pathData, SVGPath svgPath)
        {
            string replaceAll = Regex.Replace(pathData, "\t|\n", "");
            char c = 'M';
            int i = 1;
            int i2 = 0;
            foreach (char c2 in replaceAll)
            {
                if (!char.IsLetter(c2) || i2 <= 0)
                {
                    i2++;
                }
                else
                {
                    string lol = JavaStyleSubstring(replaceAll, i, i2);

                    lol = lol.Replace(",", " ");
                    svgPath.addSVGPathSegment(new SVGPathSegment(c, SVGUtil.ParseCoordinates(lol)));
                    c = c2;
                    i = i2 + 1;
                    i2 = i;
                }
            }
        }

        float mOffsetX;
        float mOffsetY;
        float mWidth;
        float mHeight;
        List<SVGPath> mPaths = new List<SVGPath>();
        public void finalScale()
        {
            Matrix matrix = new Matrix();
            float width = (float) Width;
            float height = (float) Height;
            float f = this.mWidth;
            float f2 = width / f;
            float f3 = this.mHeight;
            if (f < f3)
            {
                f2 = height / f3;
            }
            if (f2 * f3 > height)
            {
                f2 = height / f3;
            }
            this.mDx = width - (f * f2);
            this.mDy = height - (f3 * f2);
            matrix.SetScale(f2, f2);
            matrix.PostTranslate(((-this.mOffsetX) * f2) + (this.mDx / 2.0f), ((-this.mOffsetY) * f2) + (this.mDy / 2.0f));
            SVGUtil svgUtil = new SVGUtil();
            foreach (SVGPath sVGPath in this.mPaths)
            {
                if (sVGPath.getSVGPathSegments().Count() > 0)
                {
                    
                    sVGPath.setPaths(svgUtil.createFinalPaths(sVGPath.getSVGPathSegments()));
                    foreach (Android.Graphics.Path path in sVGPath.getPaths())
                    {
                        
                        path.Transform(matrix);
                    }
                }
            }
            this.mFinalScaled = true;
            this.mCurrentScale = 1.0f;

            //invalidate();
        }
        //public View(Context context, IAttributeSet attrs)
        //{
           
           
        //    this.mOffsetX = 0.0f;
        //    this.mOffsetY = 0.0f;
        //    this.mDx = 0.0f;
        //    this.mDy = 0.0f;
        //    this.mCurrentScaleState = 0;
        //    this.mCurrentScaledOffsetX = 0;
        //    this.mCurrentScaledOffsetY = 0;
        //    this.mCurrentScale = 1.0f;
        //    this.mFinalScaled = false;
        //    this.deltaX = 0;
        //    this.deltaY = 0;
        //    this.mPaint = new Paint();
        //    //this.mGestureDetector = new GestureDetector(context, new GestureListener());
        //    //this.mScaleDetector = new ScaleGestureDetector(context, new ScaleListener());
        //    this.mScroller = new Scroller(context);
        //    TypedArray obtainStyledAttributes = context.ObtainStyledAttributes(attrs, R.styleable.SVGInteractiveView);
        //    this.autoSize = obtainStyledAttributes.GetBoolean(0, true);
        //    obtainStyledAttributes.Recycle();
        //}
        


       
 

        public View1()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(App.DataSVGPath);
            List<string> split = new List<string>();
            split = ((XmlElement)doc.GetElementsByTagName("svg")[0]).GetAttribute("viewBox").Split(' ').ToList();
            mOffsetX = (float)Convert.ToDouble(split[0]);
            mOffsetY = (float)Convert.ToDouble(split[1]);
            mWidth = (float)Convert.ToDouble(split[2]);
            mHeight = (float)Convert.ToDouble(split[3]);
            XmlNodeList elements = doc.GetElementsByTagName("path");
            for (int i = 0; i < elements.Count; i++)
            {
                SVGPath sVGPath = new SVGPath();
                XmlElement element = (XmlElement)elements.Item(i);
                sVGPath.setId(element.GetAttribute("id"));
                sVGPath.setFillColor(getFillColor(sVGPath));
                parseDrawData(element.GetAttribute("d"), sVGPath);
                mPaths.Add(sVGPath);

            }

        }
    }
}