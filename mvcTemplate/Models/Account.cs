using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace mvc.Models;

public class Account : IdentityUser
{
    [StringLength(20, MinimumLength = 5)]
    public string Firstname { get; set; }

    [StringLength(20, MinimumLength = 5)]
    public string Lastname { get; set; }

    public int Age { get; set;}

    public bool IsTeacher { get; set; }

    public bool IsStudent { get; set; }
}