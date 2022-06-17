using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Rede social onde os usuários terão a possibilidade de se conectarem com outras pessoas que possuem um gosto por quadrinhos, nesta rede social os membros poderão fazer amizades, trocar conteúdo em formato de fotos, vídeos ou texto";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Entre em contato.";

            return View();
        }
    }
}