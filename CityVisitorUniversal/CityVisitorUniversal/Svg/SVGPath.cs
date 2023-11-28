using Android.Graphics;
using Svg.Pathing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityVisitorUniversal.Svg
{
    public class SVGPath
    {
        private int mFillColor;
        private string mId;
        private int mStrokeColor;
        private float mStrokeWidth;
        private List<SVGPathSegment> mSVGPathSegments = new List<SVGPathSegment>();
        private List<Path> mPaths = new List<Path>();

        public SVGPath()
        {
        }

        public SVGPath(String id)
        {
            this.mId = id;
        }

        public SVGPath(String id, int fillColor, int strokeColor, float strokeWidth)
        {
            this.mId = id;
            this.mFillColor = fillColor;
            this.mStrokeColor = strokeColor;
            this.mStrokeWidth = strokeWidth;
        }

        public string getId()
        {
            return this.mId;
        }

        public void setId(String id)
        {
            this.mId = id;
        }

        public int getFillColor()
        {
            return this.mFillColor;
        }

        public void setFillColor(int fillColor)
        {
            this.mFillColor = fillColor;
        }

        public int getStrokeColor()
        {
            return this.mStrokeColor;
        }

        public void setStrokeColor(int strokeColor)
        {
            this.mStrokeColor = strokeColor;
        }

        public float getStrokeWidth()
        {
            return this.mStrokeWidth;
        }

        public void setStrokeWidth(float strokeWidth)
        {
            this.mStrokeWidth = strokeWidth;
        }

        public List<Path> getPaths()
        {
            return this.mPaths;
        }

        public void setPaths(List<Path> paths)
        {
            this.mPaths = paths;
        }

        public List<SVGPathSegment> getSVGPathSegments()
        {
            return this.mSVGPathSegments;
        }

        public void setSVGPathSegments(List<SVGPathSegment> SVGPathSegments)
        {
            this.mSVGPathSegments = SVGPathSegments;
        }

        public void addSVGPathSegment(SVGPathSegment pathSegment)
        {
            this.mSVGPathSegments.Add(pathSegment);
        }

    }
}
