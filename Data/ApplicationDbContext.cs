using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using MusicAppApi.Models;

namespace MusicAppApi.Data
{ 
    
    public class ApplicationDbContext: IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Permission>()
                .HasKey(rp => new { rp.Id });

            // Claves primarias compuestas para las tablas de unión
            builder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            builder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            // Relaciones entre las entidades
            builder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermission)
                .HasForeignKey(rp => rp.RoleId);

            builder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermission)
                .HasForeignKey(rp => rp.PermissionId);

            builder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            builder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRole)
                .HasForeignKey(ur => ur.RoleId);

            // Seed de datos iniciales (opcional)
            SeedData(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            // Crear rol SuperAdmin
            var superAdminRole = new Role { Id = "1", Name = "SuperAdmin", NormalizedName = "SUPERADMIN", Type = "SuperAdmin" };
            builder.Entity<Role>().HasData(superAdminRole);

            // Crear permisos iniciales (ejemplos)
            var crearUsuarioPermiso = new Permission { Id = "1", Name = "CreateUser", Description = "Allow creating users." };
            var editarUsuarioPermiso = new Permission { Id = "2", Name = "EditUser", Description = "Allow editing users." };
            var crearRolePermission = new Permission { Id = "3", Name = "CreateRole", Description = "Allow creating roles." };
            // ... otros permisos ...
            builder.Entity<Permission>().HasData(crearUsuarioPermiso, editarUsuarioPermiso, crearRolePermission);

            // Asignar todos los permisos al rol SuperAdmin
            builder.Entity<RolePermission>().HasData(
                new RolePermission { RoleId = superAdminRole.Id, PermissionId = crearUsuarioPermiso.Id },
                new RolePermission { RoleId = superAdminRole.Id, PermissionId = editarUsuarioPermiso.Id },
                new RolePermission { RoleId = superAdminRole.Id, PermissionId = crearRolePermission.Id }
                // ... asignar todos los permisos creados ...
            );

            // Crear un usuario SuperAdmin inicial
            var superAdminUser = new User { 
                Id = "1", 
                UserName = "nitci_superadmin", 
                NormalizedUserName = "NITCI_SUPERADMIN", 
                Email = "superadmin@example.com", 
                NormalizedEmail = "SUPERADMIN@EXAMPLE.COM", 
                EmailConfirmed = true,
                firstName = "Nitcelis",
                lastName = "Bravo",
                //phoneNumber = "04120870460",
                picture = "./",
                timeZone = "UTC-4",
                activeStatus = true,
                licenseNumber = "34879875",
                redLine = 23.9f
            };

            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            superAdminUser.PasswordHash = passwordHasher.HashPassword(superAdminUser, "123456789"); // ¡Cambia esto!
            builder.Entity<User>().HasData(superAdminUser);

            // Asignar el rol SuperAdmin al usuario SuperAdmin
            builder.Entity<UserRole>().HasData(
                new UserRole { UserId = superAdminUser.Id, RoleId = superAdminRole.Id }
            );
        }

    }
}
