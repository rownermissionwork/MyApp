using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Application.Dtos.Services
{
    public class AddServiceRequest
    {
        public string CategoryPublicId { get; set; }
        public string ServiceName { get; set; }
        public string CreatedBy { get; set; }
    }
}
