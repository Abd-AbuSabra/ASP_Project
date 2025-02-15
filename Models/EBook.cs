using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_Project.Models
{
    public class EBook
    {
        [Key]
        public int EBookId { get; set; }
        [MinLength(3, ErrorMessage = "Name should be at least 3 charecters !")]
        [MaxLength(30, ErrorMessage = "Name should be no more than 50 charecters !")]
        [Column(TypeName ="NChar(30)")]
        [Required]
        public string Title { get; set; } = string.Empty;
        [DataType(DataType.Currency)]
        [Range(0, 500)]

        public Double BookPrice { get; set; }

        public IEnumerable<ELibrary>? eLibraries { get; set; }

    }
}
