using System;
// for List line 37
using System.Collections.Generic;
// validation
using System.ComponentModel.DataAnnotations;
// to use [NotMapped]
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelProject.Models {
    public class ClientUser {

        [Key]
        public int UserId { get; set; }

        [Required (ErrorMessage = "Enter your first name")]
        [MinLength (2, ErrorMessage = "First name must be at least 2 characters")]
        public string FirstName { get; set; }

        [Required (ErrorMessage = "Enter your last name")]
        [MinLength (2, ErrorMessage = "Last name must be at least 2 characters")]
        public string LastName { get; set; }

        [Required (ErrorMessage = "Enter your email")]
        [EmailAddress (ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }

        [DataType (DataType.Password)]
        [Required (ErrorMessage = "Enter a password")]
        [MinLength (8, ErrorMessage = "Password must be 8 characters or longer!")]
        public string Password { get; set; }

        [NotMapped]
        [Compare ("Password", ErrorMessage = "Passwords do not match")]
        [DataType (DataType.Password)]
        public string Confirm { get; set; }

        [Required (ErrorMessage = "Enter a company name")]
        public string CompanyName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        //navigation
        public List<ModelUser> JoinModels{ get; set; }

    }
}

