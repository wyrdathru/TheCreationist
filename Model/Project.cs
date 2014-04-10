using ProjectVoid.TheCreationist.Properties;
using System;
using System.Windows.Documents;
using System.Windows.Media;

namespace ProjectVoid.TheCreationist.Model
{
    public class Project
    {
        public Project()
        {
            Id = Guid.NewGuid();
            Name = Id.ToString().Substring(0, 5);
            Foreground = Settings.Default.Foreground;
            Background = Settings.Default.Background;
            Backdrop = Settings.Default.Backdrop;
            Document = new FlowDocument();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public Color Foreground { get; set; }

        public Color Background { get; set; }

        public Color Backdrop { get; set; }

        public FlowDocument Document { get; set; }
    }
}
