using AplicadaBlazorParcial2.DAL;
using AplicadaBlazorParcial2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AplicadaBlazorParcial2.BLL
{
    public class VentasBLL
    {
        private Contexto contexto { get; set; }
        public VentasBLL(Contexto contexto)
        {
            this.contexto = contexto;
        }

        //Metodo Buscar
        public async Task<Ventas> Buscar(int id)
        {
            Ventas ventas;
            try
            {
                ventas = await contexto.Venta.FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
            return ventas;
        }

        //Metodo GetList.
        public async Task<List<Ventas>> GetVentas(Expression<Func<Ventas, bool>> criterio)
        {
            List<Ventas> lista = new List<Ventas>();
            try
            {
                lista = await contexto.Venta.Where(criterio).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return lista;
        }

        public async Task<List<Ventas>> GetVentas()
        {
            List<Ventas> lista = new List<Ventas>();
            try
            {
                lista = await contexto.Venta.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return lista;
        }

    }
}
