using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Documents;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;

namespace AdornerExperiments
{
    public class WallAdorner : Adorner
    {
        VisualCollection _visualChildren;
        protected override int VisualChildrenCount { get { return _visualChildren.Count; } }
        protected override Visual GetVisualChild(int index) { return _visualChildren[index]; }

        Thumb _startThumb;
        Thumb _endThumb;

        WallElement element;

        Line previewLine;


        public WallAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            FrameworkPropertyMetadata meta = new FrameworkPropertyMetadata();
            meta.AffectsMeasure = true;

            element = (WallElement)adornedElement;

            _visualChildren = new VisualCollection(this);

            _startThumb = new Thumb();
            _startThumb.Height = 10;
            _startThumb.Width = 10;
            _startThumb.Background = Brushes.Yellow;

            _startThumb.DragDelta += new DragDeltaEventHandler(_startThumb_DragDelta);
            _startThumb.DragCompleted += new DragCompletedEventHandler(_startThumb_DragCompleted);
            _startThumb.DragStarted += new DragStartedEventHandler(_startThumb_DragStarted);

            _visualChildren.Add(_startThumb);

            _endThumb = new Thumb();
            _endThumb.Height = 10;
            _endThumb.Width = 10;
            _endThumb.Background = Brushes.Green;

            _endThumb.DragCompleted += new DragCompletedEventHandler(_endThumb_DragCompleted);

            _visualChildren.Add(_endThumb);

            previewLine = new Line();
            previewLine.Fill = Brushes.Purple;
            previewLine.Stroke = Brushes.Blue;
            previewLine.StrokeThickness = 3;
            previewLine.Visibility = Visibility.Hidden;
            previewLine.X1 = element.WallElementDetails.StartPoint.X;
            previewLine.Y1 = element.WallElementDetails.StartPoint.Y;
            previewLine.X2 = element.WallElementDetails.EndPoint.X;
            previewLine.Y2 = element.WallElementDetails.EndPoint.Y;

            _visualChildren.Add(previewLine);
        }
        

        void _startThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            previewLine.Visibility = Visibility.Visible;

            previewLine.X2 = element.WallElementDetails.EndPoint.X;
            previewLine.Y2 = element.WallElementDetails.EndPoint.Y;
        }

        void _startThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            previewLine.X1 = element.WallElementDetails.StartPoint.X + e.HorizontalChange;
            previewLine.Y1 = element.WallElementDetails.StartPoint.Y + e.VerticalChange;                
        }

        void _startThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            previewLine.Visibility = Visibility.Hidden;

            Point newStartPoint = new Point(element.WallElementDetails.StartPoint.X + e.HorizontalChange,
                                            element.WallElementDetails.StartPoint.Y + e.VerticalChange);

            WallElementDetails newWallElementDetails = element.WallElementDetails.GetCopy();
            newWallElementDetails.StartPoint = newStartPoint;

            element.WallElementDetails = newWallElementDetails;
        }

        void _endThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            Point newEndPoint = new Point(element.WallElementDetails.EndPoint.X + e.HorizontalChange,
                                element.WallElementDetails.EndPoint.Y + e.VerticalChange);
            WallElementDetails newWallElementDetails = element.WallElementDetails.GetCopy();
            newWallElementDetails.EndPoint = newEndPoint;

            element.WallElementDetails = newWallElementDetails;
        }



        protected override Size MeasureOverride(Size constraint)
        {
            return constraint;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _startThumb.Arrange(new Rect(
                                        new Point(element.WallElementDetails.StartPoint.X - 5, 
                                                  element.WallElementDetails.StartPoint.Y - 5),
                                        new Point(element.WallElementDetails.StartPoint.X + 5, 
                                                  element.WallElementDetails.StartPoint.Y + 5)
                                        )
                               );

            _endThumb.Arrange(new Rect(
                                        new Point(element.WallElementDetails.EndPoint.X - 5,
                                                  element.WallElementDetails.EndPoint.Y - 5),
                                        new Point(element.WallElementDetails.EndPoint.X + 5,
                                                  element.WallElementDetails.EndPoint.Y + 5)
                                        )
                               );

            FrameworkElement parent = (FrameworkElement) element.Parent;

            previewLine.Arrange(new Rect(0,0,parent.Width, parent.Height));

            return finalSize;
        }

        public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
        {
            return base.GetDesiredTransform(transform);
        }



    }
}
