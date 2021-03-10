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
    public class ClientesBLL
    {
        private Contexto contexto { get; set; }
        public ClientesBLL(Contexto contexto)
        {
            this.contexto = contexto;
        }

        //Metodo Existe
        public async Task<bool> Existe(int id)
        {
            bool encontrado = false;
            try
            {
                encontrado = await contexto.Cliente.AnyAsync(c => c.ClienteId == id);
            }
            catch (Exception)
            {
                throw;
            }
            return encontrado;
        }

        //Metodo insertar
        public async Task<bool> Insertar(Clientes clientes)
        {
            bool paso = false;
            try
            {
                await contexto.Cliente.AddAsync(clientes);
                paso = await contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            return paso;

        }
        //Metodo
        private async Task<bool> Modificar(Clientes clientes)
        {
            bool paso = false;
            try
            {
                contexto.Entry(clientes).State = EntityState.Modified;
                paso = await contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        //Metodo Guardar
        public async Task<bool> Guardar(Clientes clientes)
        {
            if (!await Existe(clientes.ClienteId))
                return await Insertar(clientes);
            else
                return await Modificar(clientes);
        }

        //Metodo Buscar.
        public async Task<Clientes> Buscar(int id)
        {
            Clientes clientes;
            try
            {
                clientes = await contexto.Cliente.FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
            return clientes;
        }

        //Metodo Eliminar
        public async Task<bool> Eliminar(int id)
        {
            bool paso = false;
            try
            {
                var clientes = await contexto.Cliente.FindAsync(id);
                if (clientes != null)
                {
                    contexto.Cliente.Remove(clientes);
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
        public async Task<List<Clientes>> GetClientes(Expression<Func<Clientes, bool>> criterio)
        {
            List<Clientes> lista = new List<Clientes>();
            try
            {
                lista = await contexto.Cliente.Where(criterio).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return lista;
        }
        //
        public async Task<List<Clientes>> GetClientes()
        {
            List<Clientes> lista = new List<Clientes>();
            try
            {
                lista = await contexto.Cliente.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return lista;
        }

    }
}
