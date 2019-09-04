using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlRedirect.Models;

namespace UrlRedirect.DataAccess.Interface {
    interface IUrlData {
        UrlViewModel AssingSubDomainUrl(string valUrlToRedirect);

        UrlViewModel GetUrlToRedirect(string valSubDomainUrl);
    }
}
