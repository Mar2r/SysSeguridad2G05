using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

using Microsoft.EntityFrameworkCore;
using SysSeguridad2G05.EN;

namespace SysSeguridad2G05.DAL
{
    public class RolDAL
    {
        //metodo para crear un nuevo rol
        public static async Task<int> CrearAsync(Rol rol)
        {
            int result = 0;
            using (var dbContexto = new DBContexto())
            {
                dbContexto.Rol.Add(rol);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        //metodo para modificar un rol
        public static async Task<int> ModificarAsync(Rol pRol)
        {
            int result = 0;

            using (var dbContexto = new DBContexto())
            {
                var rol = await dbContexto.Rol.FirstOrDefaultAsync(s => s.IdRol == pRol.IdRol);
                rol.Nombre = pRol.Nombre;
                dbContexto.Update(rol);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        //Metodo para eliminar un rol
        public static async Task<int> EliminarAsync(Rol pRol)
        {
            int result = 0;
            using (var dbContexto = new DBContexto())
            {
                var rol = await dbContexto.Rol.FirstOrDefaultAsync(s => s.IdRol == pRol.IdRol);
                dbContexto.Rol.Remove(rol);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<Rol> ObtenerPorId(Rol pRol)
        {
            Rol rol = new Rol();
            using (var dbContexto = new DBContexto())
            {
                //Select Id, Nombre From Rol where Id = 1;
                rol = await dbContexto.Rol.FirstOrDefaultAsync(s => s.IdRol == pRol.IdRol);
            }
            return rol;
        }

        public static async Task<List<Rol>> ObtenerTodoAsync()
        {
            List<Rol> roles = new List<Rol>();
            using (var dbContexto = new DBContexto())
            {
                //Select Id, Nombre From Rol;
                roles = await dbContexto.Rol.ToListAsync();
            }
            return roles;
        }

        internal static IQueryable<Rol> QuerySelect(IQueryable<Rol> pQuery, Rol pRol)
        {
            if (pRol.IdRol > 0)
                pQuery = pQuery.Where(s => s.IdRol == pRol.IdRol);
            if (!string.IsNullOrWhiteSpace(pRol.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pRol.Nombre)); //like
            pQuery = pQuery.OrderByDescending(s => s.IdRol).AsQueryable();
            if (pRol.Top_Aux > 0)
                pQuery = pQuery.Take(pRol.Top_Aux).AsQueryable();
            return pQuery;
        }

        /// <summary>
        /// Margarita Ramos
        /// 18/06/2026
        /// Este metodo permite buscar roles en la base de datos utilizando los campos de la entidad Rol como filtros de búsqueda
        /// por medio de condiciones if, se verifica si cada campo tiene un valor válido para ser incluido en la consulta, y se van agregando las condiciones al IQueryable
        /// </summary>
        /// <param name="pRol">Objeto con los datos a buscar</param>
        /// <returns></returns>
        public static async Task<List<Rol>> BuscarAsync(Rol pRol)
        {
            var roles = new List<Rol>();
            using (var dbContexto = new DBContexto())
            {
                var select = dbContexto.Rol.AsQueryable();
                select = QuerySelect(select, pRol);
                roles = await select.ToListAsync();
            }
            return roles;
        }
    }
   
    
}
