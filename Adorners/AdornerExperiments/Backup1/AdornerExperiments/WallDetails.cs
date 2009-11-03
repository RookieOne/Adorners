using System;
using System.Collections.Generic;
using System.Text;

namespace AdornerExperiments
{
    public class WallDetails
    {
        private bool _isFinal;

        public bool IsFinal
        {
            get { return _isFinal; }
            set { _isFinal = value; }
        }
	
        private EchelonPoint _startEchelonPoint;

        public EchelonPoint StartEchelonPoint
        {
            get { return _startEchelonPoint; }
            set { _startEchelonPoint = value; }
        }

        private EchelonPoint _endEchelonPoint;

        public EchelonPoint EndEchelonPoint
        {
            get { return _endEchelonPoint; }
            set { _endEchelonPoint = value; }
        }
    }
}
