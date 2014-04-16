using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ProjectVoid.TheCreationist.Model
{
    public class Library
    {
        public Library()
        {
            Id = Guid.NewGuid();
            Name = Id.ToString().Substring(0, 5);
            Swatches = new List<Swatch>();
            Description = string.Empty;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Guid Id { get; private set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public string Name { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<Swatch> Swatches { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public string Description { get; set; }
    }
}
