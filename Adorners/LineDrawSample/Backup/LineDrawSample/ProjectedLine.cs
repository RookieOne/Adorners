using System;
using System.Collections.Generic;
using System.Text;

using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;

namespace LineDrawSample
{
    public class ProjectedLine : Shape
    {
        #region Dependency Properties

        public static readonly DependencyProperty X1Property = DependencyProperty.Register("X1", 
                                                                typeof(double), typeof(ProjectedLine),
                                                                new FrameworkPropertyMetadata(
                                                                0d,
                                                                FrameworkPropertyMetadataOptions.AffectsMeasure, pointChange));
        public double X1
        {
            get { return (double)GetValue(X1Property); }
            set { SetValue(X1Property, value); }
        }

        public static readonly DependencyProperty X2Property = DependencyProperty.Register("X2", 
                                                                typeof(double), typeof(ProjectedLine),
                                                                new FrameworkPropertyMetadata(
                                                                0d,
                                                                FrameworkPropertyMetadataOptions.AffectsMeasure, pointChange));
        public double X2
        {
            get { return (double)GetValue(X2Property); }
            set { SetValue(X2Property, value); }
        }

        public static readonly DependencyProperty Y1Property = DependencyProperty.Register("Y1",
                                                                typeof(double), typeof(ProjectedLine),
                                                                new FrameworkPropertyMetadata(
                                                                0d,
                                                                FrameworkPropertyMetadataOptions.AffectsMeasure, pointChange));
        public double Y1
        {
            get { return (double)GetValue(Y1Property); }
            set { SetValue(Y1Property, value); }
        }

        public static readonly DependencyProperty Y2Property = DependencyProperty.Register("Y2",
                                                                typeof(double), typeof(ProjectedLine),
                                                                new FrameworkPropertyMetadata(
                                                                0d,
                                                                FrameworkPropertyMetadataOptions.AffectsMeasure, pointChange));
        public double Y2
        {
            get { return (double)GetValue(Y2Property); }
            set { SetValue(Y2Property, value); }
        }

        #endregion


        static void pointChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            ProjectedLine pj = sender as ProjectedLine;

            if (pj == null)
                return;

            pj.InvalidateVisual();
            pj.OnPointChanged();
        }

        public event EventHandler PointChanged;
        public void OnPointChanged()
        {
            if (PointChanged != null)
            {
                PointChanged(this, null);
            }
        }

        protected override Geometry DefiningGeometry
        {
            get 
            {
                PathGeometry projectedGeometry = new PathGeometry();

                PathFigure pathFigure = new PathFigure();
                pathFigure.StartPoint = new Point(X1, Y1);

                PolyLineSegment polylineSegment = new PolyLineSegment();
                polylineSegment.Points.Add(new Point(X2, Y2));

                pathFigure.Segments.Add(polylineSegment);

                projectedGeometry.Figures.Add(pathFigure);

                return projectedGeometry;
            }
        }
    }
}
