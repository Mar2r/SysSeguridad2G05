using SysSeguridad2G05.DAL;
using SysSeguridad2G05.EN;
using System;
using System.Collections.Generic;
using System.Text;

namespace SysSeguridad2G05.BL
{
    public class RolBL
    {
        public async Task<int> CrearAsync(Rol pRol)
        {
            return await RolDAL.CrearAsync(pRol);
        }

        public async Task<int> ModificarAsync(Rol pRol)
        {
            return await RolDAL.ModificarAsync(pRol);
        }

        public async Task<int> EliminarAsync(Rol pRol)
        {
            return await RolDAL.EliminarAsync(pRol);
        }

        public async Task<Rol> BuscarPorId(Rol pRol)
        {
            return await RolDAL.ObtenerPorId(pRol);
        }

        public async Task<List<Rol>> ObtenertodoAsync()
        {
            return await RolDAL.ObtenerTodoAsync();

        }

        public async Task<List<Rol>> BuscarAsync(Rol pRol)
        {
            return await RolDAL.BuscarAsync(pRol);
        }
    }
}
