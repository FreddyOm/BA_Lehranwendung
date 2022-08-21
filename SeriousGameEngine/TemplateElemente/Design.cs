using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SeriousGameEngine.TemplateElemente
{
    class Design
    {
        public static FontFamily AppFont = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Sinkin Sans 500 Medium");

        public static string C9E2F2 = "#C9E2F2";
        public static string _3D77B1 = "#3D77B1";
        public static string FFFFFFFF = "#FFFFFFFF";
        public static string FF91BAD5 = "#FF91BAD5";
        public static string FF5C6C74 = "#FF5C6C74";

        public static String HexConverter(Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

    }
}
