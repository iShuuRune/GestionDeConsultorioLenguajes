using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeConsultorio.Model
{
    public enum Horarios
    {
        [Display(Name = "9:00 a.m. - 9:59 a.m.")]
        Nueve = 9,
        [Display(Name = "10:00 a.m. - 10:59 a.m.")]
        Diez = 10,
        [Display(Name = "11:00 a.m. - 11:59 a.m.")]
        Once = 11,
        [Display(Name = "1:00 p.m. - 1:59 p.m.")]
        Una = 13,
        [Display(Name = "2:00 p.m. - 2:59 p.m.")]
        Dos = 14,
        [Display(Name = "3:00 p.m. - 3:59 p.m.")]
        Tres = 15,
        [Display(Name = "4:00 p.m. - 4:59 p.m.")]
        Cuatro = 16
    }
}

