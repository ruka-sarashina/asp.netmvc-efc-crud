using Microsoft.EntityFrameworkCore;

namespace ASPMVCCRUD.Data
{
    public class MVCDEMODbContext : DbContext //MVCDEMODbContext inheritance dari DbContext
    {
       // Opsi Constructor ini akn mengembalikkan kembali ke kelas awal
        public MVCDEMODbContext(DbContextOptions options) : base(options)
        {
        }

        // Sebagai entitas model untuk data karyawan dan melakukan crud 
        public DbSet<> MyProperty { get; set; }
        
    }
}
