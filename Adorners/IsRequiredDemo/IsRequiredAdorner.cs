using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace IsRequiredDemo
{
    public class IsRequiredAdorner : Adorner
    {
        public IsRequiredAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            VisualChildren = new VisualCollection(this);

            CreatePath();
        }

        Path _path;

        VisualCollection VisualChildren { get; set; }

        protected override int VisualChildrenCount
        {
            get { return VisualChildren.Count; }
        }

        void CreatePath()
        {
            _path = new Path();
            _path.Fill = Brushes.Red;
            _path.Data = Geometry.Parse("M0,0 L7,0 L7,7 Z");
            _path.HorizontalAlignment = HorizontalAlignment.Right;

            VisualChildren.Add(_path);
        }

        protected override Visual GetVisualChild(int index)
        {
            return VisualChildren[index];
        }

        protected override Size MeasureOverride(Size constraint)
        {
            _path.Measure(constraint);

            return base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _path.Arrange(new Rect(finalSize));

            return base.ArrangeOverride(finalSize);
        }
    }
}