using System;
using System.Collections.Generic;

namespace TheCreationist.App.Model
{
    public class Library
    {
        public Library()
        {
            Id = Guid.NewGuid();
            Name = Id.ToString().Substring(0, 5);
            Swatches = new List<Swatch>();
            Description = string.Empty;
            Tags = string.Empty;
            Author = string.Empty;
        }

        public Guid Id { get; private set; }

        public string Name { get; set; }

        public List<Swatch> Swatches { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }

        public string Author { get; set; }
    }
}