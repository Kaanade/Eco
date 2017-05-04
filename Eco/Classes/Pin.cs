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
    public class Pin : UIElement
    {
        public Point position,origine;
        public Vector deplacement;
        public int width = 50, height = 50, offsetY = 160;
        public string name;
        public double scale;
        public Rectangle rectangle;

        public Pin()
        {

        }

        public Pin(Point _position)
        {
            position = _position;
            position.Y -= offsetY;
            origine = _position;

            deplacement.X = 0;
            deplacement.Y = 0;
            scale = 1;

            rectangle = new Rectangle();
            rectangle.Width = width;
            rectangle.Height = height;
            Canvas.SetLeft(rectangle, position.X - (rectangle.Width / 2));
            Canvas.SetTop(rectangle, position.Y - (rectangle.Height / 2));

            rectangle.Fill = new SolidColorBrush() { Color = Colors.Red, Opacity = 0.75f };
            rectangle.StrokeThickness = 2;
            
        }

        public Pin(string _name)
        {
            name = _name;
        }

        public Point Position
        {
            get { return position; }
            set { position = value; }
        }

    }
}
