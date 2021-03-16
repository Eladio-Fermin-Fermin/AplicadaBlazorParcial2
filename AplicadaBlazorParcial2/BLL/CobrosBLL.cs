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
    public class CobrosBLL
    {
        private Contexto contexto { get; set; }
        public CobrosBLL(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task<bool> Guardar(Cobros cobros)
        {
            return await Insertar(cobros);
        }

        private async Task<bool> Insertar(Cobros cobros)
        {
            bool Insertado = false;
            //Contexto contexto = new Contexto();

            try
            {
                await contexto.Cobros.AddAsync(cobros);
                Insertado = await contexto.SaveChangesAsync() > 0;
                foreach (var item in cobros.Cobrosdetalles)
                {

                    item.Venta = contexto.Ventas.Find(item.VentaId);
                    item.Venta.Balance -= item.Cobrado;
                    contexto.Entry(item.Venta).State = EntityState.Modified;
                }
                contexto.Cobros.Add(cobros);
                Insertado = (contexto.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Insertado;
        }

        public async Task<Cobros> Buscar(int id)
        {
            Cobros cobros;

            try
            {
                cobros = await contexto.Cobros.Where(m => m.CobroId == id)
                    .Include(d => d.Cobrosdetalles)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return cobros;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool ok = false;
            try
            {

                var registro = await Buscar(id);

                if (registro != null)
                {
                    contexto.Cobros.Remove(registro);
                    ok = await contexto.SaveChangesAsync() > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        /*public async Task<List<Cobros>> GetCobros()
        {
            List<Cobros> lista = new List<Cobros>();

            try
            {
                lista = await contexto.Cobros.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }*/

        public async Task<List<Cobros>> GetList(Expression<Func<Cobros, bool>> criterio)
        {
            List<Cobros> lista = new List<Cobros>();

            try
            {
                lista = await contexto.Cobros.Where(criterio).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }
    }

}
