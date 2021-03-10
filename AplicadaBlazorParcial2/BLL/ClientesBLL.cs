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
