using System.ComponentModel.DataAnnotations;

namespace BabyNames.Areas.SEC_User.Models
{
    public class SEC_UserModel
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "User Name is required. ")]
        public string UserName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Password is required. ")]


        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
        public string? FirstName { get; set; }


        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? PhotoPath { get; set; }

        public IFormFile File { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

    }
}
