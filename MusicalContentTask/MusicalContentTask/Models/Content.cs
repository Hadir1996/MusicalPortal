using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MusicalContentTask.Models
{
    public class Content
    {
        public int id { get; set; }
        [Display(Name="Song Name")]
        [Required]
        public string songname { get; set; }

        [Display(Name = "Singer Name")]
        [Required]
        public string singername { get; set; }

        [Display(Name = "Album Name")]
        [Required]
        public string albumname { get; set; }

         [Required]
         [Display(Name="Upload song")]
        public string contentfile { get; set; }

         [Required]
        [Display(Name="Song Image")]
        public string contentimg { get; set; }

        [Required]
        [Display(Name=" ID copy")]
        public string idimg { get; set; }

        public int Registereduserid { get; set; }
        public Registereduser Registereduser { get; set; }
        
    }
}