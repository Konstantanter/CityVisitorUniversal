using Android.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityVisitorUniversal.Svg
{
    public class SVGUtil
    {
        static string JavaStyleSubstring(string s, int beginIndex, int endIndex)
        {
            // simulates Java substring function
            int len = endIndex - beginIndex;
            return s.Substring(beginIndex, len);
        }
        public static List<float> ParseCoordinates(string data)
        {
            
            List<float> floatList = new List<float>();
            if (string.IsNullOrEmpty(data))
            {
                return floatList;
            }
            int i = 0;
            int i2 = 0;
            string lol;

            foreach(string lols in data.Split(' '))
            {
                if (lols != "") 
                floatList.Add(float.Parse(lols));
            }
            //for (int j = 0; j < data.Length; j++)
            //{
            //    if (data[j] == ',')
            //    {
            //        lol = JavaStyleSubstring(data, i, i2);
            //        floatList.Add(float.Parse(lol));
            //        i = i2 + 1;
            //        i2 = i;
            //    }
            //    else if (data[j] != '-' || i2 > 0)
            //    {
            //        i2++;
            //    }
            //    else
            //    {
            //        lol = JavaStyleSubstring(data, i, i2);
            //        floatList.Add(float.Parse(lol));
            //        int i3 = i2;
            //        i2++;
            //        i = i3;
            //    }
            //}
            //lol = JavaStyleSubstring(data, i, i2);
            //floatList.Add(float.Parse(lol));
            return floatList;
        }


        public static void scaleCoordinates(List<SVGPathSegment> segments, int viewWidth, int viewHeight, float deltaX, float deltaY)
        {
            float f;
            float f2 = viewWidth;
            float f3 = f2 / deltaX;
            float f4 = viewHeight;
            float f5 = f4 / deltaY;
            if (deltaX > f2 || deltaY > f4)
            {
                if (deltaX > deltaY)
                {
                    f5 = deltaY / deltaX;
                }
                else if (deltaX < deltaY)
                {
                    f3 = deltaX / deltaY;
                }
            }
            float f6 = 0.0f;
            if (deltaX > deltaY)
            {
                f = (f2 - (deltaY * f5)) / 2.0f;
            }
            else
            {
                f = 0.0f;
                f6 = (f4 - (deltaX * f3)) / 2.0f;
            }
            foreach (SVGPathSegment sVGPathSegment in segments)
            {
                List<float> coordinates = sVGPathSegment.getCoordinates();
                char command = sVGPathSegment.getCommand();
                if (command == 'C')
                {
                    coordinates[0] = (float)Convert.ToDouble((coordinates[0] * f3) + f6);
                    coordinates[1] = (float)Convert.ToDouble((coordinates[1] * f5) + f);
                    coordinates[2] = (float)Convert.ToDouble((coordinates[2] * f3) + f6);
                    coordinates[3] = (float)Convert.ToDouble((coordinates[3] * f5) + f);
                    coordinates[4] = (float)Convert.ToDouble((coordinates[4] * f3) + f6);
                    coordinates[5] = (float)Convert.ToDouble((coordinates[5] * f5) + f);

                }
                else if (command == 'H')
                {
                    coordinates[0] = (float)Convert.ToDouble(coordinates[0] * f3 + f6);
                }
                else if (command == 'S')
                {
                    coordinates[0] = (float)Convert.ToDouble((coordinates[0] * f3) + f6);
                    coordinates[1] = (float)Convert.ToDouble((coordinates[1] * f5) + f);
                    coordinates[2] = (float)Convert.ToDouble((coordinates[2] * f3) + f6);
                    coordinates[3] = (float)Convert.ToDouble((coordinates[3] * f5) + f);

                }
                else if (command == 'V')
                {
                    coordinates[0] = (float)Convert.ToDouble((coordinates[0] * f5) + f);
                }
                else if (command == 'c')
                {
                    coordinates[0] = (float)Convert.ToDouble((coordinates[0] * f3));
                    coordinates[1] = (float)Convert.ToDouble((coordinates[1] * f5));
                    coordinates[2] = (float)Convert.ToDouble((coordinates[2] * f3));
                    coordinates[3] = (float)Convert.ToDouble((coordinates[3] * f5));
                    coordinates[4] = (float)Convert.ToDouble((coordinates[4] * f3));
                    coordinates[5] = (float)Convert.ToDouble((coordinates[5] * f5));
                }
                else if (command == 'h')
                {
                    coordinates[0] = (float)Convert.ToDouble((coordinates[0] * f3));
                }
                else if (command == 's')
                {
                    coordinates[0] = (float)Convert.ToDouble((coordinates[0] * f3));
                    coordinates[1] = (float)Convert.ToDouble((coordinates[1] * f5));
                    coordinates[2] = (float)Convert.ToDouble((coordinates[2] * f3));
                    coordinates[3] = (float)Convert.ToDouble((coordinates[3] * f5));

                }
                else if (command == 'v')
                {
                    coordinates[0] = (float)Convert.ToDouble((coordinates[0] * f5));
                }
                else if (command == 'L' || command == 'M')
                {
                    coordinates[0] = (float)Convert.ToDouble((coordinates[0] * f3) + f6);
                    coordinates[1] = (float)Convert.ToDouble(((coordinates[1] * f5) + f));
                }
                else if (command == 'l' || command == 'm')
                {
                    coordinates[0] = (float)Convert.ToDouble((coordinates[0] * f3));
                    coordinates[1] = (float)Convert.ToDouble((coordinates[1] * f5));
                }
            }
        }

        public static void translateToTopLeftCorner(List<SVGPathSegment> segments, SVGPathInfo pathInfo)
        {
            foreach (SVGPathSegment sVGPathSegment in segments)
            {
                List<float> coordinates = sVGPathSegment.getCoordinates();
                char command = sVGPathSegment.getCommand();
                if (command == 'C')
                {
                    float floatValue = coordinates[0];
                    float floatValue2 = coordinates[1];
                    float floatValue3 = coordinates[2];
                    float floatValue4 = coordinates[3];
                    float floatValue5 = coordinates[4];
                    float floatValue6 = coordinates[5];
                    coordinates[0] = floatValue - pathInfo.leftPoint.X;
                    coordinates[1] = floatValue2 - pathInfo.topPoint.Y;
                    coordinates[2] = floatValue3 - pathInfo.leftPoint.X;
                    coordinates[3] = floatValue4 - pathInfo.topPoint.Y;
                    coordinates[4] = floatValue5 - pathInfo.leftPoint.X;
                    coordinates[5] = floatValue6 - pathInfo.topPoint.Y;
                }
                else if (command == 'H')
                {
                    coordinates[0] = coordinates[0] - pathInfo.leftPoint.X;
                }
                else if (command != 'S')
                {
                    if (command == 'V')
                    {
                        coordinates[0] = coordinates[0] - pathInfo.topPoint.Y;
                    }
                    else if (command == 'L' || command == 'M')
                    {
                        float floatValue7 = coordinates[0];
                        float floatValue8 = coordinates[1];
                        coordinates[0] = floatValue7 - pathInfo.leftPoint.X;
                        coordinates[1] = floatValue8 - pathInfo.topPoint.Y;
                    }
                }
                float floatValue9 = coordinates[0];
                float floatValue10 = coordinates[1];
                float floatValue11 = coordinates[2];
                float floatValue12 = coordinates[3];
                coordinates[0] = floatValue9 - pathInfo.leftPoint.X;
                coordinates[1] = floatValue10 - pathInfo.topPoint.Y;
                coordinates[2] = floatValue11 - pathInfo.leftPoint.X;
                coordinates[3] = floatValue12 - pathInfo.topPoint.Y;
            }
        }

        public List<Path> createFinalPaths(List<SVGPathSegment> segments)
        {
            Path path = new Path();
            PointF pointF = new PointF(0.0f, 0.0f);
            List<Path> arrayList = new List<Path>();
            foreach (SVGPathSegment sVGPathSegment in segments)
            {
                List<float> coordinates = sVGPathSegment.getCoordinates();
                char command = sVGPathSegment.getCommand();
                if (command == 'C')
                {
                    path.CubicTo(coordinates[0], coordinates[1], coordinates[2], coordinates[3], coordinates[4], coordinates[5]);
                    executePointCurveLineTo(pointF, coordinates, true);
                }
                else if (command == 'H')
                {
                    path.LineTo(coordinates[0], pointF.Y);
                    executePointHorizontalLineTo(pointF, coordinates, true);
                }
                else if (command == 'S')
                {
                    path.CubicTo(coordinates[0], coordinates[1], coordinates[2], coordinates[3], pointF.X, pointF.Y);
                    executePointBezyeCurveLineTo(pointF, coordinates, true);
                }
                else if (command == 'V')
                {
                    path.LineTo(pointF.X, coordinates[0]);
                    executePointVerticalLineTo(pointF, coordinates, true);
                }
                else
                {
                    if (command != 'Z')
                    {
                        if (command == 'c')
                        {
                            path.CubicTo(pointF.X + coordinates[0], coordinates[1] + pointF.Y, coordinates[2]+ pointF.X, coordinates[3] + pointF.Y, coordinates[4] + pointF.X, coordinates[5] + pointF.Y);
                            executePointCurveLineTo(pointF, coordinates, false);
                        }
                        else if (command == 'h')
                        {
                            path.LineTo(coordinates[0]+ pointF.X, pointF.Y);
                            executePointHorizontalLineTo(pointF, coordinates, false);
                        }
                        else if (command == 's')
                        {
                            path.CubicTo(pointF.X + coordinates[0], coordinates[1] + pointF.Y, coordinates[2] + pointF.X, coordinates[3] + pointF.Y, pointF.X, pointF.Y);
                            executePointBezyeCurveLineTo(pointF, coordinates, false);
                        }
                        else if (command == 'v')
                        {
                            path.LineTo(pointF.X, coordinates[0]+ pointF.Y);
                            executePointVerticalLineTo(pointF, coordinates, false);
                        }
                        else if (command != 'z')
                        {
                            if (command == 'L')
                            {
                                path.LineTo(coordinates[0], coordinates[1]);
                                executePointLineTo(pointF, coordinates, true);
                            }
                            else if (command == 'M')
                            {
                                path.MoveTo(coordinates[0], coordinates[1]);
                                executePointMoveTo(pointF, coordinates, true);
                            }
                            else if (command == 'l')
                            {
                                path.LineTo(coordinates[0] + pointF.X, coordinates[1] + pointF.Y);
                                executePointLineTo(pointF, coordinates, false);
                            }
                            else if (command == 'm')
                            {
                                path.MoveTo(coordinates[0] + pointF.X, coordinates[1] + pointF.Y);
                                executePointMoveTo(pointF, coordinates, false);
                            }
                        }
                    }
                    path.Close();
                }
            }
            path.Close();
            arrayList.Add(path);
            return arrayList;
        }

        public static void executePointMoveTo(PointF point, List<float> coordinates, bool isAbsolute)
        {
            float floatValue = coordinates[0];
            float floatValue2 = coordinates[1];
            if (isAbsolute)
            {
                point.X = floatValue;
                point.Y = floatValue2;
                return;
            }
            point.X += floatValue;
            point.Y += floatValue2;
        }

        public static void executePointLineTo(PointF point, List<float> coordinates, bool isAbsolute)
        {
            executePointMoveTo(point, coordinates, isAbsolute);
        }

        /* loaded from: classes.dex */
        public class SVGPathInfo
        {
            public float deltaX;
            public float deltaY;
            public PointF leftPoint;
            public PointF topPoint;

            public SVGPathInfo(PointF topPoint, PointF leftPoint, float deltaX, float deltaY)
            {
                this.topPoint = topPoint;
                this.leftPoint = leftPoint;
                this.deltaX = deltaX;
                this.deltaY = deltaY;
            }
        }

        public static void executePointVerticalLineTo(PointF point, List<float> coordinates, bool isAbsolute)
        {
            float floatValue = coordinates[0];
            if (isAbsolute)
            {
                point.Y = floatValue;
            }
            else
            {
                point.Y += floatValue;
            }
        }

        public static void executePointHorizontalLineTo(PointF point, List<float> coordinates, bool isAbsolute)
        {
            float floatValue = coordinates[0];
            if (isAbsolute)
            {
                point.X = floatValue;
            }
            else
            {
                point.X += floatValue;
            }
        }

        public static void executePointCurveLineTo(PointF point, List<float> coordinates, bool isAbsolute)
        {
            float floatValue = coordinates[4];
            float floatValue2 = coordinates[5];
            if (isAbsolute)
            {
                point.X = floatValue;
                point.Y = floatValue2;
                return;
            }
            point.X += floatValue;
            point.Y += floatValue2;
        }

        public static void executePointBezyeCurveLineTo(PointF point, List<float> coordinates, bool isAbsolute)
        {
            float floatValue = coordinates[2];
            float floatValue2 = coordinates[3];
            if (isAbsolute)
            {
                point.X = floatValue;
                point.Y = floatValue2;
                return;
            }
            point.X += floatValue;
            point.Y += floatValue2;
        }

    }
}
