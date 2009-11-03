using System.Windows;
using System.Windows.Documents;

namespace LoadingDemo
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
                              var adornerLayer = AdornerLayer.GetAdornerLayer(grid);
                              var adorner = new LoadingAdorner(grid);

                              adornerLayer.Add(adorner);
                          };
        }
    }
}