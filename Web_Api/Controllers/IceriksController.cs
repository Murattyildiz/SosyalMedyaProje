using Business.Abstract;
using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Result.Abstract.IResult;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IceriksController : ControllerBase
    {
        private readonly IIcerikService _icerikService;

        public IceriksController(IIcerikService icerikService) => _icerikService = icerikService ?? throw new ArgumentNullException(nameof(icerikService));

        [HttpGet("geticerikwithdetails")]
        public ActionResult GetDetails()
        {
            IDataResult<List<IcerikDetailDto>> result = _icerikService.GetIcerikDetails();
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("geticerikwithdetailsbyid")]
        public ActionResult GetDetailsById(int id)
        {
            IDataResult<IcerikDetailDto> result = _icerikService.GetIcerikDetailsById(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("geticerikwithdetailsbyuserid")]
        public ActionResult GetDetailsByUserId(int id)
        {
            IDataResult<List<IcerikDetailDto>> result = _icerikService.GetIcerikDetailsByUserId(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getall")]
        public ActionResult GetAll()
        {
            IDataResult<List<Icerik>> result = _icerikService.GetAll();
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getbyid")]
        public ActionResult GetById(int id)
        {
            IDataResult<Icerik> result = _icerikService.GetEntityById(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("add")]
        public ActionResult Add(Icerik icerik)
        {
            IResult result = _icerikService.Add(icerik);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("update")]
        public ActionResult Update(Icerik icerik)
        {
            IResult result = _icerikService.Update(icerik);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("delete")]
        public ActionResult Delete(int id)
        {
            IResult result = _icerikService.Delete(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}