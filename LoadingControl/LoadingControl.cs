using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoadingControl
{

    public class LoadingControl : Control
    {

        public static readonly DependencyProperty LoadingStatusProperty = DependencyProperty.Register("LoadingStatus", typeof(bool), typeof(LoadingControl), new PropertyMetadata(false));
        public bool LoadingStatus
        {
            get { return (bool)GetValue(LoadingStatusProperty); }
            set { SetValue(LoadingStatusProperty, value); }
        }
        public static readonly DependencyProperty DiameterProperty = DependencyProperty.Register(
            "Diameter",
            typeof(double),
            typeof(LoadingControl),
            new PropertyMetadata(100.0)
            );
        public double Diameter
        {
            get { return (double)GetValue(DiameterProperty); }
            set { SetValue(DiameterProperty, value); }
        }
        public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register(
            "Thickness",
            typeof(double),
            typeof(LoadingControl),
            new PropertyMetadata(1.0)
            );
        public double Thickness
        {
            get { return (double)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            "Color",
            typeof(Brush),
            typeof(LoadingControl),
            new PropertyMetadata(Brushes.Black)
            );   
        public Brush Color
        {
            get { return (Brush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        static LoadingControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LoadingControl), new FrameworkPropertyMetadata(typeof(LoadingControl)));
        }
    }
}
