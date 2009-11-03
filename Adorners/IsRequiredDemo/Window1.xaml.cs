using System.Windows;
using System.Windows.Documents;

namespace IsRequiredDemo
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            Loaded += (sender, e) =>
                          {
                              var adornerLayer = AdornerLayer.GetAdornerLayer(textBox1);
                              var adorner = new IsRequiredAdorner(textBox1);

                              adornerLayer.Add(adorner);
                          };
        }        
    }
}