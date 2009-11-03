using System;
using System.Collections.Generic;
using System.Text;
using WPFMEDIA = System.Windows.Media;


namespace Echelon.Framework.Drawing
{
    class Pens
    {
        public static WPFMEDIA.Pen WallBoundaryStroke
        {
            get
            {
                WPFMEDIA.Pen pen = new System.Windows.Media.Pen(Echelon.Framework.Drawing.Brushs.WallBoundaryStroke, 3);
                pen.DashStyle = WPFMEDIA.DashStyles.Solid;
                return pen;
            }
        }

        public static WPFMEDIA.Pen TransitionStroke
        {
            get
            {
                WPFMEDIA.Pen pen = new System.Windows.Media.Pen(Echelon.Framework.Drawing.Brushs.TransitionStroke, 3);
                pen.DashStyle = WPFMEDIA.DashStyles.Dash;
                return pen;
            }
        }
        public static WPFMEDIA.Pen TransitionPoint
        {
            get
            {
                WPFMEDIA.Pen pen = new System.Windows.Media.Pen(Echelon.Framework.Drawing.Brushs.TransitionStroke, 3);
                pen.DashStyle = WPFMEDIA.DashStyles.Solid;
                return pen;
            }
        }
        public static WPFMEDIA.Pen DoorTransitionStroke
        {
            get
            {
                WPFMEDIA.Pen pen = new System.Windows.Media.Pen(Echelon.Framework.Drawing.Brushs.DoorTransitionStroke, 3);
                pen.DashStyle = WPFMEDIA.DashStyles.Dash;
                return pen;
            }
        }
        public static WPFMEDIA.Pen DoorTransitionPoint
        {
            get
            {
                WPFMEDIA.Pen pen = new System.Windows.Media.Pen(Echelon.Framework.Drawing.Brushs.DoorTransitionStroke, 3);
                pen.DashStyle = WPFMEDIA.DashStyles.Solid;
                return pen;
            }
        }
    }
}
