using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using PhoneCallCenter.Adorners;

namespace PhoneCallCenter.Infrastructure
{
    public static class DragBehavior
    {
        #region Fields

        private static readonly DependencyProperty DragDataProperty =
            DependencyProperty.RegisterAttached("DragData", typeof (object), typeof (DragBehavior),
                                                new PropertyMetadata(OnDragChanged));

        private static readonly DependencyProperty IsDraggingProperty =
            DependencyProperty.RegisterAttached("IsDragging", typeof (bool), typeof (DragBehavior));

        private static readonly DependencyProperty IsWiredForDragProperty =
            DependencyProperty.RegisterAttached("IsWiredForDrag", typeof (bool), typeof (DragBehavior));

        private static readonly DependencyProperty StartPointProperty =
            DependencyProperty.RegisterAttached("StartPoint", typeof (Point), typeof (DragBehavior));

        private static readonly DependencyProperty AdjustPointProperty =
    DependencyProperty.RegisterAttached("AdjustPoint", typeof(Point), typeof(DragBehavior));

        private static readonly DependencyProperty DragVisualTemplateProperty =
    DependencyProperty.RegisterAttached("DragVisualTemplate", typeof(DataTemplate), typeof(DragBehavior));

        #endregion

        #region Methods

        public static DataTemplate GetDragVisualTemplate(DependencyObject obj)
        {
            return (DataTemplate)obj.GetValue(DragVisualTemplateProperty);
        }

        public static void SetDragVisualTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(DragVisualTemplateProperty, value);
        }

        public static object GetDragData(DependencyObject obj)
        {
            return obj.GetValue(DragDataProperty);
        }

        public static void SetDragData(DependencyObject obj, object value)
        {
            obj.SetValue(DragDataProperty, value);
        }

        public static Point GetAdjustPoint(DependencyObject obj)
        {
            return (Point) obj.GetValue(AdjustPointProperty);
        }

        public static void SetAdjustPoint(DependencyObject obj, Point value)
        {
            obj.SetValue(AdjustPointProperty, value);
        }

        private static bool GetIsWiredForDrag(DependencyObject obj)
        {
            return (bool) obj.GetValue(IsWiredForDragProperty);
        }

        private static void SetIsWiredForDrag(DependencyObject obj, bool value)
        {
            obj.SetValue(IsWiredForDragProperty, value);
        }

        private static bool GetIsDragging(DependencyObject obj)
        {
            return (bool) obj.GetValue(IsDraggingProperty);
        }

        private static void SetIsDragging(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDraggingProperty, value);
        }

        private static void OnDragChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = obj as UIElement;

            if (element == null) return;

            if (!GetIsWiredForDrag(obj))
            {
                SetIsWiredForDrag(obj, true);

                SetIsDragging(element, false);
                element.PreviewMouseLeftButtonDown += element_MouseLeftButtonDown;
                element.PreviewMouseMove += element_PreviewMouseMove;
                element.GiveFeedback += (sender, feedBackEventArgs) =>
                                            {                                                
                                                feedBackEventArgs.UseDefaultCursors = false;
                                                feedBackEventArgs.Handled = true;
                                            };                
            }
        }

        private static void element_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            DependencyObject obj = sender as DependencyObject;

            if (e.LeftButton == MouseButtonState.Pressed && !GetIsDragging(obj))
            {
                Point startPoint = (Point) obj.GetValue(StartPointProperty);
                Point position = e.GetPosition(null);

                if (Math.Abs(position.X - startPoint.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(position.Y - startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    StartDrag(sender as DependencyObject);
                }
            }
        }

        private static void element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point startPoint = e.GetPosition(null);
            Point adjustPoint = e.GetPosition(sender as IInputElement);

            DependencyObject obj = sender as DependencyObject;
            obj.SetValue(StartPointProperty, startPoint);
            obj.SetValue(StartPointProperty, adjustPoint);
        }        

        private static void StartDrag(DependencyObject source)
        {            
            var dragScope = Application.Current.MainWindow as FrameworkElement;
            //var dragScope = Application.Current.MainWindow.Content as FrameworkElement;
            dragScope.AllowDrop = true;
            SetIsDragging(source, true);

            DataObject dataObject = new DataObject();

            var data = GetDragData(source);
            dataObject.SetData(data);            

            var adorner = new DragAdorner(source as UIElement, data, true, 1);



            var layer = AdornerLayer.GetAdornerLayer(((Window)dragScope).Content as FrameworkElement);
            
            layer.Add(adorner);

            var startPoint = (Point)source.GetValue(StartPointProperty);
            adorner.UpdatePosition(startPoint);

            //var adjustPoint = GetAdjustPoint(source);
            //adorner.UpdatePosition(adjustPoint);

            dragScope.DragOver += (sender, e) =>
            {
                if (adorner != null)
                {                         
                    adorner.UpdatePosition(e.GetPosition(dragScope));
                }
            };

            DragDrop.DoDragDrop(source, dataObject, DragDropEffects.Move);

            layer.Remove(adorner);
            adorner = null;

            SetIsDragging(source, false);
        }

        #endregion
    }
}