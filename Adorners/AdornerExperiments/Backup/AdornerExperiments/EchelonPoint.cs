using System;
using System.Collections.Generic;
using System.Text;

namespace AdornerExperiments
{
    public class EchelonPoint
    {
        private double x;

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        private double y;

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        public EchelonPoint(double X, double Y)
        {
            x = X;
            y = Y;
        }

        public override string ToString()
        {
            return x + ", " + y;
        }

    }
}
