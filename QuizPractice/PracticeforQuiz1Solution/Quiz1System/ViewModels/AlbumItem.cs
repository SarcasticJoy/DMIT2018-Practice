using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz1System.ViewModels
{
    public class AlbumItem
    {
        public int AlbumId { get; set; } //PK
        public string Title { get; set; }
        public int ArtistId { get; set; } //FK [ForeignKey] only name if name is different. May create problems if yu add 
        public int ReleaseYear { get; set; }
        public string ReleaseLabel { get; set; }
    }
}
