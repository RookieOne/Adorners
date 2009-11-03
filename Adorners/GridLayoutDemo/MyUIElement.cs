using System.Windows;

namespace GridLayoutDemo
{
    public class MyUIElement : UIElement
    {
        protected override Size MeasureCore(Size availableSize)
        {
            var desiredSize = DesiredSize;
            var s = availableSize;
            //return base.MeasureCore(availableSize);
            return new Size(100, 100);
        }

        protected override void ArrangeCore(Rect finalRect)
        {
            var desiredSize = DesiredSize;
            var r = finalRect;
            base.ArrangeCore(finalRect);
        }
    }
}