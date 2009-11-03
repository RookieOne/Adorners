using System;
using System.Collections.Generic;
using System.Text;

using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace LineDrawSample
{
    public class MeasurementAdorner : Adorner
    {
        VisualCollection _visualChildren;
        protected override int VisualChildrenCount { get { return _visualChildren.Count; } }
        protected override Visual GetVisualChild(int index) { return _visualChildren[index]; }

        ProjectedLine _element;
        TextBlock _lengthTextBlock;

        double _desiredWidth;
        double _desiredHeight;

        public MeasurementAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            _visualChildren = new VisualCollection(this);

            _element = (ProjectedLine)adornedElement;

            _element.PointChanged += new EventHandler(_element_PointChanged);
            _lengthTextBlock = new TextBlock();
            _visualChildren.Add(_lengthTextBlock);
        }

        void _element_PointChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Point Changed");
            this.InvalidateVisual();
            this.InvalidateMeasure();
        }

        double GetLineLength()
        {
            return Math.Sqrt(Math.Pow(_element.X2 - _element.X1, 2) + Math.Pow(_element.Y2 - _element.Y1, 2));
        }

        Point GetMidPoint()
        {
            return new Point((_element.X2 + _element.X1) / 2, (_element.Y2 + _element.Y1) / 2);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            UpdateValues();

            _lengthTextBlock.ApplyTemplate();
            _lengthTextBlock.Measure(constraint);

            _desiredWidth = _lengthTextBlock.DesiredSize.Width;
            _desiredHeight = _lengthTextBlock.DesiredSize.Height;
            
            return constraint;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Point midPoint = GetMidPoint();

            _lengthTextBlock.Arrange(new Rect(midPoint.X,
                                              midPoint.Y,
                                              _desiredWidth,
                                              _desiredHeight));

            return base.ArrangeOverride(finalSize);
        }

        int c = 0;

        protected override void OnRender(DrawingContext drawingContext)
        {
            c++;
            Console.WriteLine("RENDER : " + c);
            drawingContext.DrawEllipse(Brushes.Blue, new Pen(Brushes.Black, 1), new Point(_element.X2, _element.Y2), 3, 3);
            base.OnRender(drawingContext);
        }

        private void UpdateValues()
        {
            double len = GetLineLength();
            _lengthTextBlock.Text = String.Format("{0}", Math.Round(len).ToString());
        }
    }
}
