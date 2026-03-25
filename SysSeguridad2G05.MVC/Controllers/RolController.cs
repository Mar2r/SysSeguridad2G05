using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysSeguridad2G05.BL;
using SysSeguridad2G05.EN;

namespace SysSeguridad2G05.MVC.Controllers
{
    public class RolController : Controller
    {
        RolBL rolbl = new RolBL();
        // GET: RolController
        public async Task<ActionResult> Index(Rol pRol=null)
        {
            if (pRol == null)
                (pRol) = new Rol();
            if (pRol.Top_Aux == 0)
                pRol.Top_Aux = 10;
            else
                if (pRol.Top_Aux == -1)
                    pRol.Top_Aux = 0;
            var roles = await rolbl.ObtenertodoAsync();
            ViewBag.Top = pRol.Top_Aux;

            return View(roles);
        }

        // GET: RolController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RolController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RolController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RolController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RolController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RolController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
