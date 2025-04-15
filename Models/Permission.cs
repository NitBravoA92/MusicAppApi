using System.Collections.Generic;
namespace MusicAppApi.Models
{
    public class Permission
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<RolePermission> RolePermission { get; set; }
    }
}
