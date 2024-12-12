using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace mvc.Models;

public class Student : Account
{
    public bool FirstConnection { get; set; } = true;


}
