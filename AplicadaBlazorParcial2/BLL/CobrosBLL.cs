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

        //Metodo Existe.
        public async Task<bool> Existe(int id)
        {
            bool encontrado = false;
            try
            {
                encontrado = await contexto.Cobro.AnyAsync(c => c.CobroId == id);
            }
            catch (Exception)
            {
                throw;
            }
            return encontrado;
        }

        //Metodo Insertar.
        public async Task<bool> Insertar(Cobros cobros)
        {
            bool paso = false;
            try
            {
                await contexto.Cobro.AddAsync(cobros);
                paso = await contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        //Metodo Modificar.
        private async Task<bool> Modificar(Cobros cobros)
        {
            bool paso = false;
            try
            {
                contexto.Entry(cobros).State = EntityState.Modified;
                paso = await contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }

            return paso;
        }

        //Metodo Guardar.
        public async Task<bool> Guardar(Cobros cobros)
        {
            if (!await Existe(cobros.CobroId))
                return await Insertar(cobros);
            else
                return await Modificar(cobros);
        }

        //Buscar
        /*public async Task<Cobros> Buscar(int id)
        {
            Cobros cobros;
            try
            {
                cobros = await contexto.Cobro
                   .Where(o => o.CobroId == id)
                   .Include(d => d.CobrosDetalles)
                   .AsNoTracking()
                   .SingleOrDefaultAsync();

            }
            catch (Exception)
            {
                throw;
            }

            return cobros;
        }*/

        //Eliminar
        public async Task<bool> Eliminar(int id)
        {
            bool paso = false;
            try
            {
                var cobros = await contexto.Cobro.FindAsync(id);
                if (cobros != null)
                {
                    contexto.Cobro.Remove(cobros);
                    paso = await contexto.SaveChangesAsync() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return paso;
        }

        //GetList
        public async Task<List<Cobros>> GetCobros()
        {
            List<Cobros> lista = new List<Cobros>();
            try
            {
                lista = await contexto.Cobro.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return lista;
        }

        public async Task<List<Cobros>> GetCobros(Expression<Func<Cobros, bool>> criterio)
        {
            List<Cobros> lista = new List<Cobros>();

            try
            {
                lista = await contexto.Cobro.Where(criterio).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return lista;
        }

    }

}
