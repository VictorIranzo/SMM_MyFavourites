using System;
using System.Collections.Generic;
using System.Text;

namespace YourFavourites.Data
{
    public enum ElementType
    {
        Movie,
        Song,
        Book
    }

    public interface IElement
    {
        string Id { get; set; }

        ElementType TypeElement { get; set; }

        string MainTitle { get; set; }

        string SecondTitle { get; set; }

        string ImageUrl { get; set; }

        string Description { get; set; }

        string FirstFeature { get; set; }

        string SecondFeature { get; set; }
    }
}
