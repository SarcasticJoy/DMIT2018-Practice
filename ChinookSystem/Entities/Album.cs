namespace ChinookSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class Album
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Album() // This is for later in the course
        {
            Tracks = new HashSet<Track>();
        }

        public int AlbumId { get; set; }

        [Required(ErrorMessage = "Title required")]
        [StringLength(160, ErrorMessage = "Album title is limited to 160 characters")]
        public string Title { get; set; }

        public int ArtistId { get; set; }

        public int ReleaseYear { get; set; }

        [StringLength(50, ErrorMessage = "ReleaseLabel is limited to 50 characters")]
        public string ReleaseLabel { get; set; }

        //Navigational properties
        //Navigational properties create a virtual relational presence that
        //you can use in your application.
        // Super useful in LINQ Pad, to visualize the data
        public virtual Artist Artist { get; set; } //parent

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Track> Tracks { get; set; } //child
    }
}
