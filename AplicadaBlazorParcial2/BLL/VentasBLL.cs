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

        //Metodo Existe.
        public async Task<bool> Existe(int id)
        {
            bool encontrado = false;
            try
            {
                encontrado = await contexto.Venta.AnyAsync(v => v.VentaId == id);
            }
            catch (Exception)
            {
                throw;
            }
            return encontrado;
        }

        //Metodo Insertar.
        public async Task<bool> Insertar(Ventas ventas)
        {
            bool paso = false;
            try
            {
                await contexto.Venta.AddAsync(ventas);
                paso = await contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        //Metodo Modificar
        private async Task<bool> Modificar(Ventas ventas)
        {
            bool paso = false;
            try
            {
                contexto.Entry(ventas).State = EntityState.Modified;
                paso = await contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        //Metodo Guardar
        public async Task<bool> Guardar(Ventas ventas)
        {
            if (!await Existe(ventas.VentaId))
                return await Insertar(ventas);
            else
                return await Modificar(ventas);
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

        //Metodo Eliminar
        public async Task<bool> Eliminar(int id)
        {
            bool paso = false;
            try
            {
                var ventas = await contexto.Venta.FindAsync(id);
                if(ventas != null)
                {
                    contexto.Venta.Remove(ventas);
                    paso = await contexto.SaveChangesAsync() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
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
