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

namespace Eco
{
    public class Procedure : UIElement
    {
        public Button btnLink;
        public string pathPdf, name;

        public Procedure(string _name,string _pathPdf)
        {
            name = _name;
            pathPdf = _pathPdf;
        }




    }
}
