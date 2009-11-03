using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace AdornerExperiments
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : System.Windows.Window
    {
        public static DependencyProperty OriginProperty;
        public Point Origin
        {
            get { return (Point) GetValue(OriginProperty); }
            set { SetValue(OriginProperty, value); }
        }

        public static DependencyProperty WallElementProperty;
        public WallElement WallElement
        {
            get { return (WallElement)GetValue(WallElementProperty); }
            set { SetValue(WallElementProperty, value); }
        }
        
        private Wall wall;

        public Window1()
        {
            InitializeComponent();

            SetDependencyProperties();

            SetAndMarkOrigin();

            WireupCanvasEvents();

          
            wall = new Wall();

            WallDetails wallInfo = new WallDetails();
            wallInfo.StartEchelonPoint = new EchelonPoint(-25, -25);
            wallInfo.EndEchelonPoint = new EchelonPoint(75, 75);
            wall.WallInfo = wallInfo;

            WallElement = new WallElement(Origin, wall.WallInfo);


            designerCanvas.Children.Add(WallElement);

            SetBindings();

            this.MouseMove += new MouseEventHandler(Window1_MouseMove);
        }

        private void SetDependencyProperties()
        {
            FrameworkPropertyMetadata meta;
            
            meta = new FrameworkPropertyMetadata();            
            meta.PropertyChangedCallback += OnOriginChanged;

            OriginProperty = DependencyProperty.Register("Origin", typeof(Point), typeof(Window1), meta);

            meta = new FrameworkPropertyMetadata();
            meta.PropertyChangedCallback += OnWallElementChanged;
            WallElementProperty = DependencyProperty.Register("WallElement", typeof(WallElement), typeof(Window1), meta);
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
                       WallDetails wallInfo = new WallDetails();
            wallInfo.StartEchelonPoint = new EchelonPoint(-125, -225);
            wallInfo.EndEchelonPoint = new EchelonPoint(75, 75);
            wall.WallInfo = wallInfo;

        }

        private void OnOriginChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {

        }

        private void OnWallElementChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            txtBlockStartPoint.Text = WallElement.WallDetails.StartEchelonPoint.ToString();
            txtBlockEndPoint.Text = WallElement.WallDetails.EndEchelonPoint.ToString();

            txtBlockElementStartPoint.Text = WallElement.WallElementDetails.StartPoint.ToString();
            txtBlockElementEndPoint.Text = WallElement.WallElementDetails.EndPoint.ToString();
        }


        private void SetAndMarkOrigin()
        {
            try
            {
                Origin = new Point(designerCanvas.Width / 2, designerCanvas.Height / 2);

                OriginElement originElement = new OriginElement();
                originElement.Origin = Origin;

                designerCanvas.Children.Add(originElement);

                Binding bind = new Binding();
                bind.Source = this;
                bind.Path= new PropertyPath(Window1.OriginProperty);

                txtBlockOriginCoords.SetBinding(TextBlock.TextProperty, bind);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region Canvas Events

        private void WireupCanvasEvents()
        {
            designerCanvas.MouseMove += new MouseEventHandler(designerCanvas_MouseMove);
            designerCanvas.MouseLeftButtonDown += new MouseButtonEventHandler(designerCanvas_MouseLeftButtonDown);
            designerCanvas.MouseWheel += new MouseWheelEventHandler(designerCanvas_MouseWheel);
            designerCanvas.MouseRightButtonDown += new MouseButtonEventHandler(designerCanvas_MouseRightButtonDown);
        }

        void designerCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point newCenter = e.GetPosition(designerCanvas);

            ScaleTransform scaleTransform = new ScaleTransform(scaleX, scaleY, newCenter.X, newCenter.Y);
            designerCanvas.RenderTransform = scaleTransform;
        }

        void designerCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double centerX = e.GetPosition(designerCanvas).X;
            double centerY = e.GetPosition(designerCanvas).Y;

            if (e.Delta > 0)
            {
                scaleX += 1;
                scaleY += 1;
            }
            else
            {
                if (scaleX > 1)
                    scaleX += -1;
                if (scaleY > 1)
                    scaleY += -1;
            }

            ScaleTransform scaleTransform = new ScaleTransform(scaleX, scaleY);
            designerCanvas.RenderTransform = scaleTransform;
        }

        void designerCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            txtBlockCanvasMouseCoords.Text = (e.MouseDevice.GetPosition(designerCanvas)).X + ", " + (e.MouseDevice.GetPosition(designerCanvas)).Y;
        }


        void designerCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(designerCanvas);

            WallAdorner wa = new WallAdorner(WallElement);

            wa.MouseMove += new MouseEventHandler(wa_MouseMove);
            adornerLayer.Add(wa);
        }
        #endregion


        private void SetBindings()
        {
            try
            {


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void Window1_MouseMove(object sender, MouseEventArgs e)
        {
            txtBlockScreenMouseCoords.Text = (e.MouseDevice.GetPosition(this)).X + ", " + (e.MouseDevice.GetPosition(this)).Y;
        }

        

        private double scaleX = 1;
        private double scaleY = 1;



        void wa_MouseMove(object sender, MouseEventArgs e)
        {
            txtBlockWallAdornerMouseCoords.Text = (e.MouseDevice.GetPosition(designerCanvas)).X + ", " + (e.MouseDevice.GetPosition(designerCanvas)).Y;
        }

    }
}