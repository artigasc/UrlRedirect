using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlRedirect.Models {
    public class UrlViewModel {
        public Guid Id { get; set; }
        public string UrlToRedirect { get; set; }
        public string UrlSubDomain { get; set; }
    }
}
