using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using PhoneCallCenter.Infrastructure;

namespace PhoneCallCenter.Adorners
{
    public class DragAdorner : Adorner
    {
        #region Fields

        protected UIElement _visual;

        #endregion

        #region Properties

        public Point? StartPoint { get; set; }
        public double TranslateX { get; set; }
        public double TranslateY { get; set; }

        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        #endregion

        #region Constructors

        public DragAdorner(UIElement adornedElement, object data, bool useVisualBrush, double opacity)
            : base(adornedElement)
        {
            if (useVisualBrush)
            {
                //VisualBrush _brush = new VisualBrush(adornedElement);
                //_brush.Opacity = opacity;
                //Rectangle r = new Rectangle();

                //r.Width = adornedElement.DesiredSize.Width;
                //r.Height = adornedElement.DesiredSize.Height;

                //r.Fill = _brush;
                var border = new Border();

                //StartPoint = null;
                //_visual = border;

                StartPoint = null;
                ItemsControl control = new ItemsControl();
                //control.Width = 100;
                //control.Height = 100;
                control.ItemTemplate = DragBehavior.GetDragVisualTemplate(adornedElement);
                control.Items.Add(data);

                border.Child = control;

                _visual = border;
            }
            else
                _visual = adornedElement;
        }

        #endregion

        #region Methods

        protected override Visual GetVisualChild(int index)
        {
            return _visual;
        }

        protected override Size MeasureOverride(Size finalSize)
        {
            _visual.Measure(finalSize);
            return _visual.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _visual.Arrange(new Rect(_visual.DesiredSize));
            return finalSize;
        }

        public void UpdatePosition(Point point)
        {
            AdornerLayer adorner = (AdornerLayer) Parent;
            if (adorner != null)
            {
                if (StartPoint == null)
                {
                    StartPoint = point;
                }
                else
                {
                    Point startPoint = (Point) StartPoint;               
                    TranslateX = startPoint.X + (point.X - startPoint.X);
                    TranslateY = startPoint.Y + (point.Y - startPoint.Y);
                }

                adorner.Update(AdornedElement);
            }
        }

        public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
        {
            GeneralTransformGroup result = new GeneralTransformGroup();

            result.Children.Add(base.GetDesiredTransform(transform));
            result.Children.Add(new TranslateTransform(TranslateX, TranslateY));
            return result;
        }

        #endregion
    }
}