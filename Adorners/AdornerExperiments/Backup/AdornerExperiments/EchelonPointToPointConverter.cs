using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Globalization;
using System.Windows.Data;

namespace AdornerExperiments
{

    [ValueConversion(typeof(EchelonPoint), typeof(Point))]
    public class EchelonPointToPointConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            WallDetails wallDetails = (WallDetails)value;
            Point origin = (Point)parameter;

            WallElementDetails newWallElementDetails = new WallElementDetails();

            newWallElementDetails.StartPoint = new Point(wallDetails.StartEchelonPoint.X + origin.X, 
                                                         wallDetails.StartEchelonPoint.Y + origin.Y);
            newWallElementDetails.EndPoint = new Point(wallDetails.EndEchelonPoint.X + origin.X, 
                                                         wallDetails.EndEchelonPoint.Y + origin.Y);
            return newWallElementDetails;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            WallElementDetails wallElementDetails = (WallElementDetails)value;
            Point origin = (Point)parameter;

            WallDetails newWallDetails = new WallDetails();

            newWallDetails.StartEchelonPoint = new EchelonPoint(wallElementDetails.StartPoint.X - origin.X,
                                                         wallElementDetails.StartPoint.Y - origin.Y);
            newWallDetails.EndEchelonPoint = new EchelonPoint(wallElementDetails.EndPoint.X - origin.X,
                                                         wallElementDetails.EndPoint.Y - origin.Y);
            return newWallDetails;         
        }

        #endregion
    }
}
