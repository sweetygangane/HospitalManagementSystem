Create Solution Structure in Visual Studio
Create Solution → HospitalManagement
Then add these 5 Class Library Projects + 1 MVC Project
📁 Projects (Assemblies)
1.	Hospital.Models 
2.	Hospital.Repository 
3.	Hospital.Utility 
4.	Hospital.ViewModels 
5.	Hospital.Data (for DbContext — recommended) 
6.	Hospital.Web (ASP.NET Core MVC) 
________________________________________
2. Add Project References
In Hospital.Web add references:
•	Hospital.Models 
•	Hospital.Repository 
•	Hospital.Utility 
•	Hospital.ViewModels 
•	Hospital.Data 
In Hospital.Repository:
•	Hospital.Models 
•	Hospital.Data 
In Hospital.Utility:
•	Hospital.Data 
•	Hospital.Models 
________________________________________
3. Install Required Packages
Install in Hospital.Web:
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.AspNetCore.Identity.EntityFrameworkCore
________________________________________
 4. Create ApplicationUser (IMPORTANT)
 Hospital.Models → ApplicationUser.cs
using Microsoft.AspNetCore.Identity;

namespace Hospital.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
________________________________________
5. Create DbContext
Hospital.Data → ApplicationDbContext.cs
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Hospital.Models;

namespace Hospital.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
________________________________________
 6. Create Models
📁 Hospital.Models
Patient.cs
namespace Hospital.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Disease { get; set; }
    }
}
Doctor.cs
namespace Hospital.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
    }
}
Appointment.cs
namespace Hospital.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
    }
}
________________________________________
7. Repository Layer
 Hospital.Repository
Interface
using System.Collections.Generic;

namespace Hospital.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T entity);
        void Remove(T entity);
    }
}
________________________________________
Implementation
using Hospital.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
            _db.SaveChanges();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
            _db.SaveChanges();
        }
    }
}
________________________________________
8. Utility Layer (Roles + DbInitializer)
Hospital.Utility
Roles.cs
namespace Hospital.Utility
{
    public static class WebsiteRoles
    {
        public const string Admin = "Admin";
        public const string Doctor = "Doctor";
        public const string Patient = "Patient";
    }
}
________________________________________
DbInitializer.cs
using Microsoft.AspNetCore.Identity;
using Hospital.Data;
using Hospital.Models;

namespace Hospital.Utility
{
    public class DbInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if (!_roleManager.RoleExistsAsync(WebsiteRoles.Admin).Result)
            {
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.Admin)).Wait();
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.Doctor)).Wait();
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.Patient)).Wait();

                var admin = new ApplicationUser
                {
                    UserName = "admin@hospital.com",
                    Email = "admin@hospital.com",
                    Name = "Admin"
                };

                _userManager.CreateAsync(admin, "Admin@123").Wait();
                _userManager.AddToRoleAsync(admin, WebsiteRoles.Admin).Wait();
            }
        }
    }
}
________________________________________
9. Setup in MVC Project
📁 Hospital.Web → Program.cs
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<DbInitializer>();
________________________________________
10. Connection String
📁 appsettings.json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=HospitalDB;Trusted_Connection=True;"
}
________________________________________
 11. Run Migration
1. Add-Migration InitialCreate
2. Update-Database

Step 1: Project Creation
Created ASP.NET Core MVC project
Used .NET 8 (LTS) for stability
Enabled Razor Pages for Identity
📌 Step 2: Solution Structure

Created 3-layer architecture:

H.Web → UI Layer (MVC)
H.Repo → Repository Layer (Data Access)
H.Utility → Common Utilities
📌 Step 3: Install Required Packages

Installed NuGet packages (version 8.0.6):

Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.AspNetCore.Identity.EntityFrameworkCore
Microsoft.VisualStudio.Web.CodeGeneration.Design
📌 Step 4: Configure Database

Updated appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=HospitalDB;Trusted_Connection=True;"
}
📌 Step 5: Setup DbContext
Created ApplicationDbContext
Inherited from IdentityDbContext
Registered in Program.cs:
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
📌 Step 6: Configure Identity

Added Identity services:

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
📌 Step 7: Implement Repository Pattern
Created IGenericRepository<T>
Implemented GenericRepository<T>
Added CRUD operations (GetAll, GetById, Add, Update, Delete)
📌 Step 8: Implement Unit of Work
Created IUnitOfWork
Implemented UnitOfWork
Managed repositories and database transactions
📌 Step 9: Dependency Injection

Registered services in Program.cs:

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
📌 Step 10: Database Migration

Executed commands:

Add-Migration InitialCreate
Update-Database
📌 Step 11: Data Seeding
Created IDbInitializer and DbInitializer
Seeded default roles and admin user
Called in Program.cs
📌 Step 12: Authentication UI
Added Login/Register pages using Identity
Implemented _LoginPartial.cshtml
Displayed user name and logout option
📌 Step 13: Routing Setup

Configured default route:

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Patient}/{controller=Home}/{action=Index}/{id?}"
);
