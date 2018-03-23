using System;
using System.Collections.Generic;
using System.Text;

namespace Minutes
{
    public class Entry
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public override string ToString()
        {
            return Title + " " + CreatedDateTime.ToShortDateString();
        }
    }
}
