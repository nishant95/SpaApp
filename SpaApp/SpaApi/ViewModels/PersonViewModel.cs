using System;
using System.ComponentModel.DataAnnotations;

namespace SpaApi.ViewModels
{
    public class PersonViewModel
    {
        #region Properties

        public long Id { get; set; }

        [Required(AllowEmptyStrings = false , ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessage = "Email is Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Date of Birth is Required")]
        public DateTime Dob { get; set; }

        #endregion
    }
}
