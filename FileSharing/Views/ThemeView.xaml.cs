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

namespace FileSharing.Views
{

    
    public partial class ThemeView : UserControl
    {
        public static readonly DependencyProperty FontColorProperty = DependencyProperty.Register(
        "FontColor",
        typeof(Brush),
        typeof(ThemeView)

        );
        public Brush FontColor
        {
            get { return (Brush)GetValue(FontColorProperty); }
            set { SetValue(FontColorProperty, value); }
        }
        public static readonly DependencyProperty FontFamilyProperty = DependencyProperty.Register(
            "FontFamily",
            typeof(string),
            typeof(ThemeView)
            );
        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }
        public ThemeView()
        {
            InitializeComponent();
        }
    }
}
