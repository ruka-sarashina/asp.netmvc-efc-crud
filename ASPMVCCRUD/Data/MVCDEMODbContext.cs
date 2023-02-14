using ASPMVCCRUD.Models.Domain;
using Microsoft.EntityFrameworkCore;

// jangan lupa untuk memasukkan ke dalam Programs.cs
// karena untuk mengetahui applikasi membuat DbContext
// dan properti apa yang akan di buat

namespace ASPMVCCRUD.Data
{
    public class MVCDEMODbContext : DbContext //MVCDEMODbContext inheritance dari DbContext
    {
        // Opsi Constructor ini akn mengembalikkan kembali ke kelas awal
        public MVCDEMODbContext(DbContextOptions options) : base(options)
        {
        }

        // Sebagai entitas model untuk data karyawan dan melakukan crud 
        // setelah membuat model yang domain, lalu akan di terima oleh properti
        // DbSet
        public DbSet<Employee> Employees { get; set; }

    }
}
