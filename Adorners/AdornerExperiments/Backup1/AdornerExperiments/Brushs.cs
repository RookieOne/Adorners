using System;
using System.Collections.Generic;
using System.Text;
using WPFMEDIA = System.Windows.Media;

namespace Echelon.Framework.Drawing
{
    public static class Brushs
    {

        public static WPFMEDIA.Brush WallBoundaryStroke
        {
            get
            {
                return WPFMEDIA.Brushes.Red.CloneCurrentValue();
            }
        }
        public static System.Windows.Media.Brush WallBoundaryFill
        {
            get
            {
                return WPFMEDIA.Brushes.Red.CloneCurrentValue();
            }
        }
        public static System.Windows.Media.Brush ReferenceGridStroke
        {
            get
            {
                return WPFMEDIA.Brushes.LightSteelBlue.CloneCurrentValue();
            }
        }
        public static System.Windows.Media.Brush TransitionStroke
        {
            get
            {
                return WPFMEDIA.Brushes.Green.CloneCurrentValue();
            }
        }
        public static System.Windows.Media.Brush TransitionFill
        {
            get
            {
                return WPFMEDIA.Brushes.Red.CloneCurrentValue();
            }
        }
        public static System.Windows.Media.Brush DoorTransitionStroke
        {
            get
            {
                return WPFMEDIA.Brushes.Blue.CloneCurrentValue();
            }
        }
        public static System.Windows.Media.Brush DoorTransitionFill
        {
            get
            {
                return WPFMEDIA.Brushes.Blue.CloneCurrentValue();
            }
        }
    }
}
