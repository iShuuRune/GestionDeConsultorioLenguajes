using GestionDeConsultorio.Model;
using Microsoft.EntityFrameworkCore;


namespace GestionDeConsultorio.DA
{
    public class DBContexto : DbContext
    {

        public DBContexto(DbContextOptions<DBContexto> opciones) : base(opciones)
        {
            
        }

        public DbSet<Cita> Citas { get; set; }
    }
}
