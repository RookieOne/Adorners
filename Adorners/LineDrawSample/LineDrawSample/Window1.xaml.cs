using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LineDrawSample
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : System.Windows.Window
    {
        ProjectedLine previewLine;
        bool isDrawing = false;
        double snapAngle = 45;              //Angle increments to which the drawn line will be restricted

        public Window1()
        {
            InitializeComponent();
        }

        private void CanvasRefresh(object sender, EventArgs e)
        {
            //Set area where problem is occurring
            BrokenZone.Width = canvasMain.ActualWidth / 2;
            BrokenZone.Height = canvasMain.ActualHeight / 2;
        }

        public void Canvas_OnLeftMouseDown(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                //Reset drawing
                isDrawing = false;
                canvasMain.Children.Remove(previewLine);
            }
            else
            {
                isDrawing = true;
                Point thePoint = e.GetPosition(sender as IInputElement);

                //Create projected line
                previewLine = new ProjectedLine();
                previewLine.Fill = Brushes.Black;
                previewLine.StrokeThickness = 2;
                previewLine.Stroke = Brushes.Black;

                //Set initial position for the line
                previewLine.X1 = canvasMain.ActualWidth / 2;
                previewLine.Y1 = canvasMain.ActualHeight / 2;
                previewLine.X2 = thePoint.X;
                previewLine.Y2 = thePoint.Y;

                //Add to canvas
                canvasMain.Children.Add(previewLine);

                AddMeasurementAdorner(previewLine);
            }
        }

        public void Canvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                Point thePoint = e.GetPosition(sender as IInputElement);
                Point adjustedPoint = GetAdjustedPoint(thePoint);               // Get adjusted position based on snap angle
                //Set new line end point
                previewLine.X2 = adjustedPoint.X;
                previewLine.Y2 = adjustedPoint.Y;
                //previewLine.InvalidateVisual();
            }
        }

        private Point GetAdjustedPoint(Point NewPoint)
        {
            return AdjustToClosestAngle(new Point(previewLine.X1, previewLine.Y1), NewPoint, snapAngle);
        }

        private void AddMeasurementAdorner(ProjectedLine line)
        {
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(line);
            MeasurementAdorner pla = new MeasurementAdorner(line);
            adornerLayer.Add(pla);
        }

        #region Angle functions

        private Point AdjustToClosestAngle(Point StartPoint, Point EndPoint, double Angle)
        {
            //Whatever the angle is, force it to be as close as possible to that angle.
            //Keep the length the same, then figure out what angle it should be and
            //force it to that position.
            //Don't adjust the start point.
            //Returns a new endpoint.

            double newX = 0, newY = 0;
            double x = EndPoint.X - StartPoint.X;
            double y = EndPoint.Y - StartPoint.Y;

            double CurrentAngle = SegmentAngle(StartPoint, EndPoint);

            double length = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));

            double result = Math.IEEERemainder(CurrentAngle, Angle);
            CurrentAngle -= result;

            double angRad = Math.PI * CurrentAngle / 180.0;
            double sinTheta = Math.Sin(angRad);
            double cosTheta = Math.Cos(angRad);

            newX = length * cosTheta + StartPoint.X;
            newY = length * sinTheta + StartPoint.Y;
            return new Point(newX, newY);
        }

        private double SegmentAngle(Point StartPoint, Point EndPoint)
        {
            double pxRes = EndPoint.X - StartPoint.X;
            double pyRes = EndPoint.Y - StartPoint.Y;
            double angle = 0.0;
            angle = Math.Atan2(pyRes, pxRes);

            return angle * (180 / Math.PI);
        }

        #endregion
    }
}