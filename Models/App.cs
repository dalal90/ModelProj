using System;
using System.ComponentModel.DataAnnotations;//validation
using System.ComponentModel.DataAnnotations.Schema;//not mapped
using System.Collections.Generic;//list

using Microsoft.AspNetCore.Http;

namespace ModelProject.Models
{
    public class App
    {
        [Key]
        public int AppId {get;set;}



        [Required]
        public string AgeCat{get;set;}

        [Required]
        public int Age{get;set;}

        [Required]
        public int Height{get;set;}

        [Required]
        public int Weight{get;set;}

        [Required]
        public string Instagram{get;set;}

        [Required]
        public string LinkedIn{get;set;}

        public int ModelUserId { get; set; }
        //navigation 
        public ModelUser Creator {get;set;}//1:M

    }
}