using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace mvc.Models;

public class Teacher : Account
{
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
