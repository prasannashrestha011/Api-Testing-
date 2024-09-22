using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSharing.DataStructure
{
    public class ThemeStructure
    {
        public string ThemeColor { get; set; }
        public string FontColor { get; set; }
        public string EditorTheme {  get; set; }    
        public ThemeStructure(string ThemeColor,string FontColor,string EditorTheme)
        {
            this.ThemeColor = ThemeColor;
            this.FontColor = FontColor;
            this.EditorTheme = EditorTheme;
        }
    }
}
