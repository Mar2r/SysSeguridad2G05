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
    }

    #region CRUD

    #endregion
}




