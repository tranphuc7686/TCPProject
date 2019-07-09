using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TCPProject.Repository;

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
        public async Task<IActionResult> RefeshData([FromBody] List<string> model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            try
            {
                await _crawlDataRepository.CrawlDataByUrlProfile(model);               

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}