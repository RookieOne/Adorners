using System;
using System.Collections.Generic;
using System.Text;
using WPFMEDIA = System.Windows.Media;
using WPF = System.Windows;

namespace Echelon.Framework.Drawing
{
    public static class Primitives
    {
        //Can be more complex later.
        public static WPFMEDIA.LineGeometry Line(WPF.Point StartPoint, WPF.Point EndPoint)
        {
            WPFMEDIA.LineGeometry newLine = new System.Windows.Media.LineGeometry(StartPoint, EndPoint);
            return newLine;
        }
        public static WPFMEDIA.LineGeometry PerpendicularLine(WPF.Point StartPoint, WPF.Point EndPoint, double Length)
        {
            //Get Current Angle Don't need the actual angle, radians is fine.
            double angle = CurrentAngle(StartPoint, EndPoint);
            double radians = Math.Atan2(StartPoint.Y - EndPoint.Y, StartPoint.X - EndPoint.X);

            //Take 90 deg
            double newAngle = angle + 90;
            double newRadians = radians + Math.PI / 2;

            //need to determine the shift.

            double shiftX = Length * Math.Cos(newRadians);
            //double shiftX2 = Length * Math.Cos(newAngle * Math.PI / 180);

            double shiftY = Length * Math.Sin(newRadians);

            double newStartPointX = StartPoint.X - shiftX;
            double newStartPointY = StartPoint.Y - shiftY;

            double newEndPointX = StartPoint.X + shiftX;
            double newEndPointY = StartPoint.Y + shiftY;

            WPF.Point newStartPoint = new WPF.Point(newStartPointX, newStartPointY);
            WPF.Point newEndPoint = new WPF.Point(newEndPointX, newEndPointY);

            //get the second point

            //draw line from center of the point.
            WPFMEDIA.LineGeometry newLine = new System.Windows.Media.LineGeometry(newStartPoint, newEndPoint);
            return newLine;
        }
        //draws a line at a relative angle compared to the origin line.
        public static WPFMEDIA.LineGeometry AngularLine(WPF.Point StartPoint, WPF.Point EndPoint, double Length, double RelativeAngle)
        {
            //Get Current Angle Don't need the actual angle, radians is fine.
            double angle = CurrentAngle(StartPoint, EndPoint);
            double radians = Math.Atan2(StartPoint.Y - EndPoint.Y, StartPoint.X - EndPoint.X);

            //Take 90 deg
            double newAngle = angle + RelativeAngle;
            //double newRadians = radians + Math.PI / 2;

            double shiftX = Length * Math.Cos(newAngle * Math.PI / 180);

            double shiftY = Length * Math.Sin(newAngle * Math.PI / 180);

            double newEndPointX = StartPoint.X - shiftX;
            double newEndPointY = StartPoint.Y - shiftY;

            WPF.Point newEndPoint = new WPF.Point(newEndPointX, newEndPointY);

            //get the second point

            //draw line from center of the point.
            WPFMEDIA.LineGeometry newLine = new System.Windows.Media.LineGeometry(StartPoint, newEndPoint);
            return newLine;
        }
        //Draws a polygon with different thiknesses at each end. Traces over a line.
        public static WPFMEDIA.PathGeometry PolygonLine(WPF.Point StartPoint, WPF.Point EndPoint, double StartWidth, double EndWidth)
        {

            //Get Current Angle Don't need the actual angle, radians is fine.
            double angle = CurrentAngle(StartPoint, EndPoint);
            double radians = Math.Atan2(StartPoint.Y - EndPoint.Y, StartPoint.X - EndPoint.X);

            //Take 90 deg
            double newAngle = angle + 90;
            double newRadians = radians + Math.PI / 2;

            //need to determine the shift.

            double shiftStartX = StartWidth * Math.Cos(newRadians);
            double shiftStartY = StartWidth * Math.Sin(newRadians);

            double shiftEndX = EndWidth * Math.Cos(newRadians);
            double shiftEndY = EndWidth * Math.Sin(newRadians);

            double newStartPointX1 = StartPoint.X - shiftStartX;
            double newStartPointY1 = StartPoint.Y - shiftStartY;
            double newStartPointX2 = StartPoint.X + shiftStartX;
            double newStartPointY2 = StartPoint.Y + shiftStartY;

            double newEndPointX1 = EndPoint.X - shiftEndX;
            double newEndPointY1 = EndPoint.Y - shiftEndY;
            double newEndPointX2 = EndPoint.X + shiftEndX;
            double newEndPointY2 = EndPoint.Y + shiftEndY;

            WPF.Point newStartPoint1 = new WPF.Point(newStartPointX1, newStartPointY1);
            WPF.Point newStartPoint2 = new WPF.Point(newStartPointX2, newStartPointY2);
            WPF.Point newEndPoint1 = new WPF.Point(newEndPointX1, newEndPointY1);
            WPF.Point newEndPoint2 = new WPF.Point(newEndPointX2, newEndPointY2);


            //Now we have all the points. Need to build a polygon shape.
            WPFMEDIA.PathGeometry rootPath = new System.Windows.Media.PathGeometry();
            WPFMEDIA.PolyLineSegment poly = new System.Windows.Media.PolyLineSegment();
            //poly.Points.Add(newStartPoint1);
            poly.Points.Add(newStartPoint2);
            poly.Points.Add(newEndPoint2);
            poly.Points.Add(newEndPoint1);

            WPFMEDIA.PathFigure newFigure = new WPFMEDIA.PathFigure();
            newFigure.StartPoint = newStartPoint1;
            newFigure.Segments.Add(poly);
            rootPath.Figures.Add(newFigure);

            return rootPath;
        }
        public static double CurrentAngle(WPF.Point StartPoint, WPF.Point EndPoint)
        {
            double radians = Math.Atan2(StartPoint.Y - EndPoint.Y, StartPoint.X - EndPoint.X);
            double angle = radians * (180 / Math.PI);
            return angle;
        }
        public static double DistanceBetweenPoints(WPF.Point StartPoint, WPF.Point EndPoint)
        {
            double length = Math.Sqrt(Math.Pow(StartPoint.X - EndPoint.X, 2) + Math.Pow(StartPoint.Y - EndPoint.Y, 2));
            return length;
        }
    }
}
