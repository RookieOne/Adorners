using System;
using System.Collections.Generic;
using System.Text;

using System.Windows;

namespace AdornerExperiments
{
    public class WallElementDetails
    {
        private bool _isFinal;

        public bool IsFinal
        {
            get { return _isFinal; }
            set { _isFinal = value; }
        }
	
        private Point _startPoint;

        public Point StartPoint
        {
            get { return _startPoint; }
            set { _startPoint = value; }
        }

        private Point _endPoint;

        public Point EndPoint
        {
            get { return _endPoint; }
            set { _endPoint = value; }
        }

        public WallElementDetails GetCopy()
        {
            WallElementDetails copy = new WallElementDetails();
            
            copy.StartPoint = _startPoint;
            copy.EndPoint = _endPoint;

            return copy;
        }
    }
}
