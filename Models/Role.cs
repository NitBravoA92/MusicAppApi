using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
namespace MusicAppApi.Models
{
    public class Role: IdentityRole
    {
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public string NormalizedName { get; set; }
        public List<RolePermission> RolePermission { get; set; }
        public List<UserRole> UserRole { get; set; }
        
        public string Type { get; set; }
    }
}
