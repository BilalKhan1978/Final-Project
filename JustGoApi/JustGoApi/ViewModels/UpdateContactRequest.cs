using System.ComponentModel.DataAnnotations;

namespace JustGoApi.ViewModels
{
    public class UpdateContactRequest
    {
        [Required]
        public string FullName { get; set; }
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
