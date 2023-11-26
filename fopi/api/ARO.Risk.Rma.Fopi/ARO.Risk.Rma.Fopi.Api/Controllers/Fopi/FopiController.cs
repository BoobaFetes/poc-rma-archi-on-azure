using ARO.Risk.Rma.Fopi.Api.Controllers.Fopi.Dto;
using ARO.Risk.Rma.Fopi.Application.Fopi;
using ARO.Risk.Rma.Fopi.Domain.Fopi;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ARO.Risk.Rma.Fopi.Api.Controllers.Fopi
{
    [Route("api/fopi")]
    [ApiController]
    public class FopiController : ControllerBase
    {
        private readonly IFopiUsecases fopiUsecases;

        public FopiController(IFopiUsecases fopiUsecases)
        {
            this.fopiUsecases = fopiUsecases;
        }

        // GET: api/<FopiController>
        [HttpGet]
        public ActionResult<IEnumerable<FopiBase>> Get()
        {
            return Ok(this.fopiUsecases.All());
        }

        // GET api/<FopiController>/5
        [HttpGet("{id}")]
        public ActionResult<FopiDetailDto> Get(int id)
        {
            var result = this.fopiUsecases.Read(id);
            if (result == null) return NotFound(id);
            return Ok(result);
        }

        // GET api/<FopiController>/5/base
        [HttpGet("{id}/base")]
        public ActionResult<FopiBase> GetBase(int id)
        {
            var result = this.fopiUsecases.ReadBase(id);
            if (result == null) return NotFound(id);
            return Ok(result);
        }

        // POST api/<FopiController>
        [HttpPost]
        public ActionResult<FopiDetailDto> Post([FromBody] FopiDetailDto value)
        {
            var result = this.fopiUsecases.Create(value);
            if (result == null) return NotFound(value);
            return Ok(result);
        }

        // PUT api/<FopiController>/5
        [HttpPut("{id}")]
        public ActionResult<FopiDetailDto> Put(int id, [FromBody] FopiDetailDto value)
        {
            var result = this.fopiUsecases.Update(id, value);
            if (result == null) return NotFound(value);
            return Ok(result);
        }

        // DELETE api/<FopiController>/5
        [HttpDelete("{id}")]
        public ActionResult<FopiDetailDto> Delete(int id)
        {
            var result = this.fopiUsecases.Delete(id);
            if (result == null) return NotFound(id);
            return Ok(result);

        }
    }
}
