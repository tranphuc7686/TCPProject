using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CrawlDataTool.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TCPProject.Repository;

namespace TCPProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly IDataRepository dataRepository;
        private readonly ICrawlDataRepository crawlDataRepository;
        public CommonController(IDataRepository _dataRepository, ICrawlDataRepository _crawlDataRepository)
        {
            dataRepository = _dataRepository;
            crawlDataRepository = _crawlDataRepository;
        }

        // GET api/Common
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Data1", "Data2" };
        }

        // GET api/Common/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "Data";
        }
        // GET api/Common/Datas/5
        // get id data by ctg id
        [HttpPost("Datas/{id}/{index}")]
        public async Task<IActionResult> GetDataByIdCategory(int id, int index)
        {
            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                var post = await dataRepository.GetDatas(id, index);

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

        // GET api/Common/Category/5
        // get id data by ctg id
        [HttpPost("Categories/{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                var post = await dataRepository.GetCategoriesById(id);

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



        // GET api/Common/5
        [HttpGet("RefeshData")]
        public void RefeshData()
        {

        }
        // POST api/Common
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Data data)
        {
            if (await crawlDataRepository.AddData(data) == 0)
            {
                return BadRequest();
            }
            return Ok();
            
        }

        // PUT api/Common/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string Data)
        {
        }

        // DELETE api/Common/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        [HttpPost, Route("datas/upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
           
            return Ok(await crawlDataRepository.AddImageAsync(file));

        }
    }
}
