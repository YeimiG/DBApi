using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemPenitentiary.Models;

namespace SistemPenitentiary.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    
    public class LoginController : ControllerBase
    {
        private readonly UeUsocotContext  _context;     
    }


}
