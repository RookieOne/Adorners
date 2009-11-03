using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using System.ComponentModel;

using System.Windows.Controls;

using Echelon.Framework.Drawing;

namespace AdornerExperiments
{
    public class WallElement : Shape
    {
        public static DependencyProperty OriginProperty;
        public Point Origin
        {
            get { return (Point)GetValue(OriginProperty); }
            set { SetValue(OriginProperty, value); }
        }

        public static DependencyProperty WallDetailsProperty;
        public WallDetails WallDetails
        {
            get { return (WallDetails)GetValue(WallDetailsProperty); }
            set { SetValue(WallDetailsProperty, value); }
        }

        public static DependencyProperty WallElementDetailsProperty;
        public WallElementDetails WallElementDetails
        {
            get { return (WallElementDetails)GetValue(WallElementDetailsProperty); }
            set { SetValue(WallElementDetailsProperty, value); }
        }


        public WallElement(Point origin, WallDetails wallInfo)
        {
            SetupDependencyProperties();

            SetBindings(origin, wallInfo);
        }

        #region Dependency Property Initialization and Event Handlers

        private void SetupDependencyProperties()
        {
            FrameworkPropertyMetadata meta;

            meta = new FrameworkPropertyMetadata();
            meta.PropertyChangedCallback += OnOriginChanged;            
            OriginProperty = DependencyProperty.Register("Origin", typeof(Point), typeof(WallElement), meta);

            meta = new FrameworkPropertyMetadata();                    
            meta.PropertyChangedCallback += OnWallDetailsChanged;
            WallDetailsProperty = DependencyProperty.Register("WallDetails", typeof(WallDetails), typeof(WallElement), meta);

            meta = new FrameworkPropertyMetadata();
            meta.AffectsMeasure = true;    
            meta.PropertyChangedCallback += OnWallElementDetailsChanged;
            WallElementDetailsProperty = DependencyProperty.Register("WallElementDetails", typeof(WallElementDetails), typeof(WallElement), meta);
        }

        private void OnWallDetailsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            WallElementDetails newWallElementDetails = new WallElementDetails();
            newWallElementDetails.StartPoint = GetAsPoint(WallDetails.StartEchelonPoint);
            newWallElementDetails.EndPoint = GetAsPoint(WallDetails.EndEchelonPoint);
            newWallElementDetails.IsFinal = true;

            WallElementDetails = newWallElementDetails;
                        
            if (WallDetails.IsFinal)
            {
                WallDetails.IsFinal = false;
            }
        }

        private void OnWallElementDetailsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            SetFillBrush();

            if (!WallElementDetails.IsFinal)
            {
                // Would call the change method, etc that would communicate to Client Services and start that workflow
                // here we will mock the process
                WallDetails newWallDetails = new WallDetails();
                newWallDetails.StartEchelonPoint = GetAsEchelonPoint(WallElementDetails.StartPoint);
                newWallDetails.EndEchelonPoint = GetAsEchelonPoint(WallElementDetails.EndPoint);
                newWallDetails.IsFinal = true;
                WallDetails = newWallDetails;
            }
            else
            {
                WallElementDetails.IsFinal = false;
            }
        }

        private void OnOriginChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

        private void SetBindings(Point origin, WallDetails wallInfo)
        {
            try
            {
                Origin = origin;
                WallDetails = wallInfo;

                Binding bind;

                //bind = new Binding();
                //bind.Source = this;
                //bind.Path = new PropertyPath(WallElement.WallDetailsProperty);
                //bind.ConverterParameter = Origin;
                //bind.Converter = new EchelonPointToPointConverter();
                //bind.Mode = BindingMode.OneWay;

                //this.SetBinding(WallElementDetailsProperty, bind);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        protected override Geometry DefiningGeometry
        {
            get 
            {
                GeometryGroup rootGeometryGroup = new GeometryGroup();

                PathGeometry SolidWall = Primitives.PolygonLine(WallElementDetails.StartPoint, WallElementDetails.EndPoint, 10, 10);
            
                rootGeometryGroup.Children.Add(SolidWall);

                return rootGeometryGroup;            
            }
        }

        
        public override Geometry RenderedGeometry
        {
            get
            {                
                return base.RenderedGeometry;
            }
        }

        public void SetFillBrush()
        {
            DrawingGroup rootDrawingGroup = new DrawingGroup();

            GeometryDrawing aDrawing = new GeometryDrawing();
            aDrawing.Brush = Brushes.Gray.CloneCurrentValue();
            aDrawing.Pen = new Pen(Brushes.Gray, 1);
            aDrawing.Brush.Opacity = .5;

            aDrawing.Geometry = Primitives.PolygonLine(WallElementDetails.StartPoint, WallElementDetails.EndPoint, 10, 10);

            rootDrawingGroup.Children.Add(aDrawing);

            //create a transition line
            GeometryDrawing aCenterLine = new GeometryDrawing();
            aCenterLine.Brush = Brushs.WallBoundaryStroke;
            aCenterLine.Pen = Pens.WallBoundaryStroke;

            aCenterLine.Geometry = Primitives.Line(WallElementDetails.StartPoint, WallElementDetails.EndPoint);
            
            rootDrawingGroup.Children.Add(aCenterLine);

            DrawingBrush brush = new DrawingBrush(rootDrawingGroup);
            
            this.Fill = brush;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
        }

        private Point GetAsPoint(EchelonPoint echelonPoint)
        {
            return new Point(echelonPoint.X + Origin.X, echelonPoint.Y + Origin.Y);
        }

        private EchelonPoint GetAsEchelonPoint(Point point)
        {
            return new EchelonPoint(point.X - Origin.X, point.Y - Origin.Y);
        }
    }
}
