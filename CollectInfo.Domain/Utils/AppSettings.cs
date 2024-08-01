using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectInfo.Domain.Utils
{
    public class AppSettings
    {
        public string OwnerRepository { get; set; }
        public string Repository { get; set; }
        public string TokenAccessRepository { get; set; }
        public string BaseUrl { get; set; }
        public string CriteriaExtensionSearchFileType { get; set; }
    }
}
