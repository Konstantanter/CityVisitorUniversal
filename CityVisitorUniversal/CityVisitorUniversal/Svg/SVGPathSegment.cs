using Java.Lang;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityVisitorUniversal.Svg
{
    public class SVGPathSegment
    {
        private char mCommand;
        private List<float> mCoordinates;

        public SVGPathSegment(char command, List<float> coordinates)
        {
            this.mCommand = command;
            this.mCoordinates = coordinates;
        }

        public char getCommand()
        {
            return this.mCommand;
        }

        public void setCommand(char command)
        {
            this.mCommand = command;
        }

        public List<float> getCoordinates()
        {
            return this.mCoordinates;
        }

        public void setCoordinates(List<float> coordinates)
        {
            this.mCoordinates = coordinates;
        }

        public string toString()
        {
            return string.Format("{command: %s, coordinates: %s}", this.mCommand, mCoordinates.ToString());
        }
    }
}
