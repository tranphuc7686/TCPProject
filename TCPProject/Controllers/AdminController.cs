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

        private readonly IDataRepository _dataRepository;
        public AdminController(ICrawlDataRepository crawlDataRepository, IDataRepository dataRepository)
        {
            _crawlDataRepository = crawlDataRepository;
            _dataRepository = dataRepository;
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
            catch (Exception ex)
            {
                Console.Write(ex.ToString() + " BugBugBugBug");
                return BadRequest();
            }
        }

        // GET api/admin/update/5
        // update status element pending
        [HttpPost("update/{id}")]
        public async Task<IActionResult> PublicElement(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                var post = _dataRepository.PublicElement(id);

                if (post == null)
                {
                    return NotFound();
                }

                return Ok(post);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        // GET api/Common/Datas/5
        // get id data by ctg id
        [HttpPost("datas/{id}/{index}")]
        public async Task<IActionResult> GetDataPendingByIdCategory(int id, int index)
        {
            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                var post = await _dataRepository.GetDatasPending(id, index);

                if (post == null)
                {
                    return NotFound();
                }

                return Ok(post);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET api/admin/update/5
        // update status element pending
        [HttpPost("applications")]
        public async Task<IActionResult> GetAllApplications()
        {
            

            try
            {
                var post = await _dataRepository.GetApplications();

                if (post == null)
                {
                    return NotFound();
                }

                return Ok(post);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


    }
}