using System;
using System.Collections.Generic;
using System.Text;

using SysSeguridad2G05.EN;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;



namespace SysSeguridad2G05.DAL
{
    public class UsuarioDAL
    {
        private static void EncriptMD5(Usuario pUsuario)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(pUsuario.Password));
                var strEncritar = "";
                for (int i = 0; i < result.Length; i++)

                    strEncritar += result[i].ToString("x2").ToLower();
                pUsuario.Password = strEncritar;

            }
        }

        private static async Task<bool> ExisteLogin(Usuario pUsuario, DBContexto pDContexto)
        {
            bool result = false;
            var login = await pDContexto.Usuario.FirstOrDefaultAsync(a => a.Login == pUsuario.Login && a.IdUsuario != pUsuario.IdUsuario);
            if (login != null && login.IdUsuario > 0 && login.Login == pUsuario.Login)
                result = true;
            return result;
        }
    

        #region "CRUD"
        public static async Task<int> GuardarAsync(Usuario pUsuario)
        {
            int result = 0;
            try
            {
                using (var dbContexto = new DBContexto())
                {
                    bool existeLogin = await ExisteLogin(pUsuario, dbContexto);
                    if (existeLogin == false)
                    {
                        EncriptMD5(pUsuario);
                        dbContexto.Add(pUsuario);
                        result = await dbContexto.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("Login ya existe");
                    }
                }
            }
            catch (Exception ex)
            {
                result = 0;
                throw new Exception(ex.Message);
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Usuario pUsuario)
        {
            int result = 0;
            try
            {
                using (var dbContexto = new DBContexto())
                {

                    bool existeLogin = await ExisteLogin(pUsuario, dbContexto);
                    if (existeLogin == false)
                    {
                        var usuario = await dbContexto.Usuario.FirstOrDefaultAsync(d => d.IdUsuario == pUsuario.IdUsuario);
                        usuario.IdRol = pUsuario.IdRol;
                        usuario.Nombre = pUsuario.Nombre;
                        usuario.Apellido = pUsuario.Apellido;
                        usuario.Login = pUsuario.Login;
                        usuario.status = pUsuario.status;
                        dbContexto.Update(usuario);
                        result = await dbContexto.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("Login ya existe");

                    }


                }


            }

            catch (Exception ex)
            {
                result = 0;
                throw new Exception(ex.Message);
            }

            return result;
        }

        public static async Task<int> EliminarAsync(Usuario pUsuario)
        {
            int result = 0;
            try
            {
                using (var dbContext = new DBContexto())
                {
                    var usuario = await dbContext.Usuario.FirstOrDefaultAsync(f => f.IdUsuario == pUsuario.IdUsuario);
                    dbContext.Usuario.Remove(usuario);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                result = 0;
                throw new Exception("Error al eliminar el usuario", ex);

            }
            return result;
        }
        public static async Task<Usuario> ObtenerPorIdAsync(Usuario pUsuario)
        {
            var usuario = new Usuario();
            try
            {
                using (var dbContext = new DBContexto())
                {
                    usuario = await dbContext.Usuario.FirstOrDefaultAsync(s => s.IdUsuario == pUsuario.IdUsuario);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el usuario por Id", ex);
            }
            return usuario;
        }
        public static async Task<List<Usuario>> ObtenerTodosAsync()
        {
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                using (var dbContext = new DBContexto())
                {
                    usuarios = await dbContext.Usuario.ToListAsync();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los usuarios", ex);
            }
            return usuarios;
        }
        internal static IQueryable<Usuario> QuerySelect(IQueryable<Usuario> pQuery, Usuario pUsuario)
        {
            if (pUsuario.IdUsuario > 0)
                pQuery = pQuery.Where(s => s.IdUsuario == pUsuario.IdUsuario);
            if (pUsuario.IdUsuario > 0)
                pQuery = pQuery.Where(s => s.IdUsuario == pUsuario.IdUsuario);
            if (!string.IsNullOrWhiteSpace(pUsuario.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pUsuario.Nombre));
            if (!string.IsNullOrWhiteSpace(pUsuario.Apellido))
                pQuery = pQuery.Where(s => s.Apellido.Contains(pUsuario.Apellido));
            if (pUsuario.status > 0)
                pQuery = pQuery.Where(s => s.status == pUsuario.status);
            if (pUsuario.FechaRegistro.Year > 1000)
            {
                DateTime fechaInicial = new DateTime(pUsuario.FechaRegistro.Year, pUsuario.FechaRegistro.Month, pUsuario.FechaRegistro.Day, 0, 0, 0);
                DateTime fechaFinal = fechaInicial.AddDays(1).AddMilliseconds(-1);
                pQuery = pQuery.Where(s => s.FechaRegistro >= fechaInicial && s.FechaRegistro <= fechaFinal);
            }
            pQuery = pQuery.OrderByDescending(s => s.IdUsuario).AsQueryable();
            if (pUsuario.Top_Aux > 0)
                pQuery = pQuery.Take(pUsuario.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Usuario>> BuscarAsync(Usuario pUsuario)
        {
            var usuarios = new List<Usuario>();
            using (var dbContext = new DBContexto())
            {
                var select = dbContext.Usuario.AsQueryable();
                select = QuerySelect(select, pUsuario);
                usuarios = await select.ToListAsync();
            }
            return usuarios;
        }
        public static async Task<List<Usuario>> BuscarIncluirRolesAsync(Usuario pUsuario)
        {
            var usuarios = new List<Usuario>();
            using (var dbContext = new DBContexto())
            {
                var select = dbContext.Usuario.AsQueryable();
                select = QuerySelect(select, pUsuario).Include(s => s.Rol).AsQueryable();
                usuarios = await select.ToListAsync();
            }
            return usuarios;
        }
        public static async Task<Usuario> LoginAsync(Usuario pUsuario)
        {
            var usuario = new Usuario();
            using (var dbContext = new DBContexto())
            {
                EncriptMD5(pUsuario);
                usuario = await dbContext.Usuario.FirstOrDefaultAsync(s => s.Login == pUsuario.Login && s.Password == pUsuario.Password && s.status == (byte)Status_Usuario.Activo);
            }
            return usuario;
        }
    }

        #endregion
}





     

