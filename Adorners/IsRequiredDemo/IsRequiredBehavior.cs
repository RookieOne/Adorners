using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace IsRequiredDemo
{
    public static class IsRequiredBehavior
    {
        static IsRequiredBehavior()
        {
            IsRequiredProperty = DependencyProperty.RegisterAttached(
                "IsRequired", typeof (bool), typeof (IsRequiredBehavior),
                new PropertyMetadata(IsRequiredChanged));
        }

        public static readonly DependencyProperty IsRequiredProperty;

        public static void SetIsRequired(DependencyObject obj, bool value)
        {
            obj.SetValue(IsRequiredProperty, value);
        }

        public static bool GetIsRequired(DependencyObject obj)
        {
            return (bool) obj.GetValue(IsRequiredProperty);
        }

        static void IsRequiredChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            bool newValue = (bool) e.NewValue;

            Control control = obj as Control;

            if (!newValue || control == null)
                return;

            bool adornerloaded = false;
            control.Loaded += delegate
                                  {
                                      if (!adornerloaded)
                                      {
                                          AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(control);

                                          if (adornerLayer != null)
                                          {
                                              adornerLayer.Add(new IsRequiredAdorner(control));
                                              adornerloaded = true;
                                          }
                                      }
                                  };
        }
    }
}