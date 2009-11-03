using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;

using System.Windows.Controls;

namespace AdornerExperiments
{
    class OriginElement : FrameworkElement
    {
        public static DependencyProperty OriginProperty;

        public Point Origin
        {
            get { return (Point)GetValue(OriginProperty); }
            set { SetValue(OriginProperty, value); }
        }

        public OriginElement()
        {
            FrameworkPropertyMetadata meta = new FrameworkPropertyMetadata();
            meta.AffectsRender = true;

            OriginProperty = DependencyProperty.Register("Origin", typeof(Point), typeof(OriginElement), meta);

        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            Rect rect = new Rect(Origin.X - 5, Origin.Y - 5, 10, 10);
            drawingContext.DrawRectangle(Brushes.Green, null, rect);
        }
    }
}
