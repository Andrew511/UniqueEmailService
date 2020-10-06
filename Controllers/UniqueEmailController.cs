using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using UniqueEmailService.Models;

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
