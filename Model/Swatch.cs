using System.Windows.Media;

namespace TheCreationist.App.Model
{
    public class Swatch
    {
        public Swatch()
            : this(Colors.Transparent)
        {
            //
        }

        public Swatch(Color color)
        {
            Color = color;
        }

        public Color Color { get; set; }
    }
}
