using Microsoft.AspNetCore.Identity;
namespace MusicAppApi.Models;

using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

public class User : IdentityUser
{
    //public int Id { get; set; }
    //public string UserName { get; set; }
    //public string NormalizedUserName { get; set; }
    //public string Email { get; set; }
    //public string NormalizedEmail { get; set; }
    //public string PasswordHash { get; set; }
    //public bool EmailConfirmed { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    //public string phoneNumber { get; set; }
    public string picture { get; set; }
    public string timeZone { get; set; }
    public Boolean activeStatus { get; set; }
    public string licenseNumber { get; set; }
    public float redLine { get; set; }
    public List<UserRole> UserRoles { get; set; }
}