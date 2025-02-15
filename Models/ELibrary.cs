using System.ComponentModel.DataAnnotations;

namespace ASP_Project.Models
{
    public class ELibrary
    {
        [Key]
        public int ELibraryId { get; set; }
        public int UserId { get; set; }
        public int EBookId { get; set; }
        [StringLength(255)]
        [DataType(DataType.MultilineText)]
        public string BookDescription { get; set; } = string.Empty;


        [DataType(DataType.Date)]
        public DateTime PublishingDate { get; set; } = DateTime.Now;

        public eGenera Genera { get; set; }

        public Boolean IsAvailable { get; set; }
        public User? user { get; set; }
        public EBook? eBook { get; set; }
    }
}
