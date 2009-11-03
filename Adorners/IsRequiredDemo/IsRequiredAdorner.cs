using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace IsRequiredDemo
{
    public class IsRequiredAdorner : Adorner
    {
        public IsRequiredAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            VisualChildren = new VisualCollection(this);

            Control = new Control();
            VisualChildren.Add(Control);

            Style style = FindResource("IsRequiredAdornerStyle") as Style;

            if (style != null)
                Control.Style = style;
        }

        VisualCollection VisualChildren { get; set; }

        protected override int VisualChildrenCount
        {
            get { return VisualChildren.Count; }
        }

        Control Control { get; set; }

        protected override Visual GetVisualChild(int index)
        {
            return VisualChildren[index];
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Control.Measure(constraint);

            return base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Control.Arrange(new Rect(finalSize));

            return base.ArrangeOverride(finalSize);
        }
    }
}