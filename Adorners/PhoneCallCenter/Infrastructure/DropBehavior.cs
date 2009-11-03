using System;
using System.Windows;

namespace PhoneCallCenter.Infrastructure
{
    public static class DropBehavior
    {
        #region Fields

        private static readonly DependencyProperty DropActionProperty =
            DependencyProperty.RegisterAttached("DropAction", typeof (string), typeof (DropBehavior),
                                                new PropertyMetadata(OnDropChanged));

        private static readonly DependencyProperty DropDataTypeProperty =
            DependencyProperty.RegisterAttached("DropDataType", typeof (Type), typeof (DropBehavior));

        private static readonly DependencyProperty DropInstanceProperty =
            DependencyProperty.RegisterAttached("DropInstance", typeof (object), typeof (DropBehavior),
                                                new PropertyMetadata(OnDropChanged));

        private static readonly DependencyProperty IsWiredForDropProperty =
            DependencyProperty.RegisterAttached("IsWiredForDrop", typeof (bool), typeof (DropBehavior));

        #endregion

        #region Methods

        public static string GetDropAction(DependencyObject obj)
        {
            return (string) obj.GetValue(DropActionProperty);
        }

        public static void SetDropAction(DependencyObject obj, string value)
        {
            obj.SetValue(DropActionProperty, value);
        }

        public static object GetDropInstance(DependencyObject obj)
        {
            return obj.GetValue(DropInstanceProperty);
        }

        public static void SetDropInstance(DependencyObject obj, object value)
        {
            obj.SetValue(DropInstanceProperty, value);
        }

        public static Type GetDropDataType(DependencyObject obj)
        {
            return (Type) obj.GetValue(DropDataTypeProperty);
        }

        public static void SetDropDataType(DependencyObject obj, Type value)
        {
            obj.SetValue(DropDataTypeProperty, value);
        }

        private static bool GetIsWiredForDrop(DependencyObject obj)
        {
            return (bool) obj.GetValue(IsWiredForDropProperty);
        }

        private static void SetIsWiredForDrop(DependencyObject obj, bool value)
        {
            obj.SetValue(IsWiredForDropProperty, value);
        }

        private static void OnDropChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = obj as UIElement;

            if (element == null) return;

            if (!GetIsWiredForDrop(obj))
            {
                SetIsWiredForDrop(obj, true);

                element.AllowDrop = true;
                element.Drop += element_Drop;
            }
        }

        private static void element_Drop(object sender, DragEventArgs e)
        {
            DependencyObject depObj = sender as DependencyObject;
            string methodName = GetDropAction(depObj);
            object instance = GetDropInstance(depObj);
            Type dataType = GetDropDataType(depObj);

            if (instance == null) return;

            var type = instance.GetType();

            var method = type.GetMethod(methodName);
            object[] parameters = new object[1];

            DataObject dataObject = e.Data as DataObject;
            parameters[0] = dataObject.GetData(dataType);
            method.Invoke(instance, parameters);
        }

        #endregion
    }
}