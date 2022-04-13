using System;
using System.ComponentModel.DataAnnotations;//validation
using System.ComponentModel.DataAnnotations.Schema;//not mapped
using System.Collections.Generic;//list


namespace ModelProject.Models
{
    public class ModelUser
    {
        [Key]
        public int ModelUserId {get;set;}

        [Required]
        public string FirstName {get;set;}

        [Required]
        public string LastName {get;set;}

        
        [Required]
        public DateTime DOF {get;set;}

        [Required]
        public string City {get;set;}

        [Phone]
        public string MobilePhone { get; set; }

        [EmailAddress]
        [Required]
        public string Email {get;set;}

        [DataType(DataType.Password)]
        [Required]
        [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]

        public string Password {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        // Will not be mapped to your users table!

        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}


        // navigation 
        public List<App> CreatedApp{get;set;}
        public ClientUser Agency{get;set;}

        // public List<Event> CreatedEvents {get;set;}//OneToMany
        // public List<Attendee> AttendedEvents {get;set;}//ManyToMany User attended Event
    
    }
}