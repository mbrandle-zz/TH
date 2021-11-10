using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TH.Data;
using TH.Models;
using TH.Utilities;

namespace TH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly AppDBContext _context;

        public SurveyController(AppDBContext context)
        {
            _context = context;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetSurveyAsync(int id)
        {
            Activity activity = await _context.Activities.FirstOrDefaultAsync(a => a.id == id);
            if(activity == null)
            {
                return BadRequest("The Survey Is Not Valid");
            }

            return base.Content(PaginaEncuesta.GetPagina());
        }

    }
}
