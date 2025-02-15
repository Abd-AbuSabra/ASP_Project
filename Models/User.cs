using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_Project.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [MinLength(3, ErrorMessage = "Name should be at least 3 charecters !")]
        [MaxLength(50, ErrorMessage = "Name should be no more than 50 charecters !")]
        [Required]
        [Column(TypeName = "NChar(30)")]
        public string UserName { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [EmailAddress]
        public string UserEmail { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string PasswordConfirmation { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateTime UserBD { get; set; } = DateTime.Now;
        public IEnumerable<ELibrary>? eLibraries { get; set; }
        

    }
}
