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
//    [Table("Artists")] // The SLQ has the table as a plural... so we need to point to it. I think
//    internal class Artist // Internal to keep secure. Unlike last DMIT class.
//    {
//        // [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Tells the system not to guess the key but to generate it, and with an identity # generated. 
//        [Key]
//        public int ArtistId { get; set; }

//        // assessor is the get , mutator is the set

//        [Required(ErrorMessage = "Artist Name is required.")]
//        [StringLength(120, MinimumLength = 1, ErrorMessage = "Artist name is limited to 120 characters")] // max, then min but you need to specifiy the min
//        public string Name { get; set; }

//    }   // If the names are the same as the database, they will auto line up. If the names differ, you will have to type them in the correct order yourself.
//}
