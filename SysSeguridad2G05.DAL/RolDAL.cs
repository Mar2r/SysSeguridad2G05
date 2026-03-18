using System;
using System.Collections.Generic;
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
        public static async Task<int> EliminarAsync( Rol pRol)
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
    }
}
