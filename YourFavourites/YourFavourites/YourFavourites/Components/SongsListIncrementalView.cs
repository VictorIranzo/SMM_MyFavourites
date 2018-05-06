using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YourFavourites.Data;

namespace YourFavourites.Components
{
    public class SongsListIncrementalView : IncrementalViewModel<Song>
    {
        public SongsListIncrementalView()
            : base()
        {
            this.CurrentPosition = 1;
        }

        protected async override Task AddItems()
        {
            IEnumerable<Song> songsCollection = null;
            songsCollection = await new SongsManager().GetSongs(this.CurrentPosition, this.PageSize);

            foreach (Song song in songsCollection)
            {
                this.MyItems.Add(song);
            }
        }

        protected async override Task<bool> CheckMoreItems()
        {
            return 500 > this.CurrentPosition;
        }
    }
}

