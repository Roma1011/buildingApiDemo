using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using webapi.Data;
using webapi.Models;
using webapi.Models.Dto;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebApiController:ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public ActionResult<IEnumerable<WebapiDTO>> GetApi()
        {
            return Ok(WebapiStore.WebList);
        }
        [HttpGet("{id:int}",Name ="GetAllApi")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public  ActionResult<WebapiDTO> GetApi(int id)
        {
            if (id == 0)
                return BadRequest();

            var TestResult=(WebapiStore.WebList.FirstOrDefault(item => item.Id.Equals(id)));

            if (TestResult is null)
                return NotFound();

            else return Ok(TestResult);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<WebapiDTO> PostApi([FromBody] WebapiDTO _webapi)
        {
            if(WebapiStore.WebList.FirstOrDefault(item=>item.Name.ToLower()== _webapi.Name.ToLower())!=null)
            {
                ModelState.AddModelError("Costumer error", "Already Exists!");
                return BadRequest(ModelState);
            }
            if (_webapi is null)
                return BadRequest(_webapi);

            else if (_webapi.Id > 0)
                return  StatusCode(StatusCodes.Status500InternalServerError);

            _webapi.Id = WebapiStore.WebList.OrderByDescending(item => item.Id).First().Id+1;
            WebapiStore.WebList.Add(_webapi);

            return CreatedAtRoute("GetAllApi", new { id = _webapi.Id }, _webapi);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "ApiDelete")]
        public IActionResult ApiDelete(int id)
        {
            var webapi = WebapiStore.WebList.FirstOrDefault(item => item.Id.Equals(id));
            if (id.Equals(0))
                return BadRequest();
            else if (webapi is null)
                return NotFound();
            else
                WebapiStore.WebList.Remove(webapi);

            return NoContent();
        }
        [HttpPut("{id:int}",Name ="UpdateApi")]
        public IActionResult Updateapi(int id, [FromBody]WebapiDTO _apiDto) 
        {
            var webapi = WebapiStore.WebList.FirstOrDefault(item => item.Id.Equals(id));
            if (id.Equals(0))
                return BadRequest();
            else if (webapi is null)
                return NotFound();
            else
                webapi.Name = _apiDto.Name;

            return NoContent();
        }

        [HttpPatch("{id:int}",Name ="UpdatePartialApi")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialapi(int id, JsonPatchDocument<WebapiDTO>_patch)
        {
            var webapi = WebapiStore.WebList.FirstOrDefault(item => item.Id.Equals(id));
            if (id.Equals(0) || _patch is null || webapi is null)
                return BadRequest();

            _patch.ApplyTo(webapi,ModelState);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            else
                return NoContent();
        }
    }
}
