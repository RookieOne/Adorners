using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LoadingDemo
{
    public class LoadingAdorner : Adorner
    {
        public LoadingAdorner(UIElement adornedElement) : base(adornedElement)
        {
            VisualChildren = new VisualCollection(adornedElement);

            CreateVisual();
        }

        Rectangle _Overlay;
        VisualCollection VisualChildren { get; set; }

        protected override int VisualChildrenCount
        {
            get { return VisualChildren.Count; }
        }

        protected override Visual GetVisualChild(int index)
        {
            return VisualChildren[index];
        }

        void CreateVisual()
        {
            _Overlay = new Rectangle();
            _Overlay.Fill = new SolidColorBrush(Color.FromArgb(141, 255, 255, 255));

            VisualChildren.Add(_Overlay);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            _Overlay.Measure(constraint);
            return base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _Overlay.Height = DesiredSize.Height;
            _Overlay.Width = DesiredSize.Width;

            _Overlay.Arrange(new Rect(finalSize));

            return base.ArrangeOverride(finalSize);
        }
    }
}