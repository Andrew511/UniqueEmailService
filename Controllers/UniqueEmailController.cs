using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UniqueEmailService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniqueEmailController : ControllerBase
    {
        [HttpPost]
        public int GetUniqueAddresses(IEnumerable<Email> emails)
        {
            return emails.Select(email => email.UniqueEmail).Distinct().Count();
        }
    }
}
