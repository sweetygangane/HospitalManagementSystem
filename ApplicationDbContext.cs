/*
using H.Model;
using Hospital.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<ApplicationUser> ApplicationUsers{ get; set; }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Contact>Contacts{ get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<HospitalInfo> HospitalInfo { get; set; }
    public DbSet<Insurance> Insurances { get; set; }
    public DbSet<Lab> Lab { get; set; }
    public DbSet<Medicine> Medicines { get; set; }
    public DbSet<MedicineReport> MedicineReports { get; set; }
    public DbSet<Payroll> Payrolls { get; set; }
    public DbSet<PrescribedMedicine> PrescribedMedicines { get; set; }
    public DbSet<PatientReport> PatientReports { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Supplier> Supplier { get; set; }
    public DbSet<TestPrice> TestPrices { get; set; }

}*/
using H.Model;
using Hospital.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    public DbSet<HospitalInfo> HospitalInfos { get; set; }
    public DbSet<Insurance> Insurances { get; set; }
    public DbSet<Lab> Lab { get; set; }
    public DbSet<Medicine> Medicines { get; set; }
    public DbSet<MedicineReport> MedicineReports { get; set; }
    public DbSet<Payroll> Payrolls { get; set; }
    public DbSet<PrescribedMedicine> PrescribedMedicines { get; set; }
    public DbSet<PatientReport> PatientReports { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Supplier> Supplier { get; set; }
    public DbSet<TestPrice> TestPrices { get; set; }
}
