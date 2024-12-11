using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace mvc.Models;

public class Teacher : IdentityUser
{
    //public int Id { get; set; }
    
    [StringLength(20, MinimumLength = 5)]
    public string Firstname { get; set; }

    [StringLength(20, MinimumLength = 5)]
    public string Lastname { get; set; }
    
    [Required]
    public int Age { get; set; }

    public string Matter { get; set; }

    public string Langage { get; set; }

    public enum LangageEnum
    {
        RubyOnRails,
        CSharp,
        CPplusplus,
        PHP
    }
}
