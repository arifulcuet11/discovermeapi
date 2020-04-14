using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Utility;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalEnumController : ControllerBase
    {
        [HttpGet("catagory")]
        public IActionResult GetAllCatagory()
        {
            try
            { 
                Dictionary<string, int> Catagorys = Enum.GetValues(typeof(Catagory))
                                        .Cast<Catagory>()
                                        .ToDictionary(k => k.ToString(), v => (int)v);
                return Ok(Catagorys);
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}
