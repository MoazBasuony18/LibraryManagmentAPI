using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentAPI.Common.DTOs
{
    public class UserDTO:LoginUserDTO
    {
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public ICollection<string> Roles { get; set; }
    }

    public class LoginUserDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }


    public class UserDataDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public List<CheckoutDTO> Checkouts { get; set; }
    }
}
