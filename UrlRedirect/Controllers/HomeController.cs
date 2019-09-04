using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlRedirect.DataAccess.Implementation;
using UrlRedirect.DataAccess.Interface;
using UrlRedirect.Helpers;
using UrlRedirect.Models;

namespace UrlRedirect.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public IActionResult AssingSubDomainUrl([FromBody]UrlViewModel valObjectUrl) {
            try {
               IUrlData vUrlData = new UrlData();
                Uri vUriResult;
                if (!Utils.ValidHttpURL(valObjectUrl.UrlToRedirect, out vUriResult)) {
                    return Json(new { content = "1", message = "El Formato de Url es incorrecto" });
                }
               UrlViewModel  vUrlResponse = vUrlData.AssingSubDomainUrl(vUriResult?.AbsoluteUri);

                if (vUrlResponse != null) {
                   
                    return Json(new { content = "1", message= vUrlResponse.UrlSubDomain });
                }

            } catch (Exception vEx) {
                return Json(new { content = "1", message = "Error: "+vEx.Message });
            }
            return Json(new { content = "1", message = "No existen dominios disponibles" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
