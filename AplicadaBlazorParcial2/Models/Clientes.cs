using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AplicadaBlazorParcial2.Models
{
    public class Clientes
    {
        [Key]
        public int ClienteId { get; set; }
        public string Nombres { get; set; }
    }
}
