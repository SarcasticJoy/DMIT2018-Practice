//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//#region Additional Namespaces
//using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;
//#endregion

//namespace ChinookSystem.Entities
//{
//    [Table("Albums")] // The SLQ has the table as a plural... so we need to point to it. I think 
//    internal class Album
//    {
//        [Key]
//        public int AlbumId { get; set; } //PK
//        [Required(ErrorMessage = "Title required")]
//        [StringLength(160, ErrorMessage = "Album title is limited to 160 characters")]
//        public string Title { get; set; }
//        public int ArtistId { get; set; } //FK [ForeignKey] only name if name is different. May create problems if yu add 
//        public int ReleaseYear { get; set; }
//        [StringLength(50, ErrorMessage = "ReleaseLabel is limited to 50 characters")]
//        public string ReleaseLabel { get; set; }
//    }
//}
