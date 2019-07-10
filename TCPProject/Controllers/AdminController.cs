using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TCPProject.Repository;
using TCPProject.ViewModel;

namespace TCPProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ICrawlDataRepository _crawlDataRepository;
        public AdminController(ICrawlDataRepository crawlDataRepository)
        {
            _crawlDataRepository = crawlDataRepository;
        }
        // GET api/Common
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Data1", "Data2" };
        }
        // GET api/admin/refeshdata/
        // get id Categories by list url profile
        [HttpPost("refeshdata")]
        public async Task<IActionResult> RefeshData([FromBody] CrawlDataViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            try
            {
                await _crawlDataRepository.CrawlDataByUrlProfile(model.model,model.ctgId);               

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}