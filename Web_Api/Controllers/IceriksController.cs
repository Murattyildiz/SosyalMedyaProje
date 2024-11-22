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
        private readonly IIcerikService _ıcerikService;

        public IceriksController(IIcerikService ıcerikService) => _ıcerikService = ıcerikService ?? throw new ArgumentNullException(nameof(ıcerikService));

        [HttpGet("geticerikwithdetails")]
        public ActionResult GetDetails()
        {
            IDataResult<List<IcerikDetailDto>> result = _ıcerikService.GetIcerikDetails();
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("geticerikwithdetailsbyid")]
        public ActionResult GetDetailsById(int id)
        {
            IDataResult<IcerikDetailDto> result = _ıcerikService.GetIcerikDetailsById(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("geticerikwithdetailsbyuserid")]
        public ActionResult GetDetailsByUserId(int id)
        {
            IDataResult<List<IcerikDetailDto>> result = _ıcerikService.GetIcerikDetailsByUserId(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getall")]
        public ActionResult GetAll()
        {
            IDataResult<List<Icerik>> result = _ıcerikService.GetAll();
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getbyid")]
        public ActionResult GetById(int id)
        {
            IDataResult<Icerik> result = _ıcerikService.GetEntityById(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("add")]
        public ActionResult Add(Icerik icerik)
        {
            IResult result = _ıcerikService.Add(icerik);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("update")]
        public ActionResult Update(Icerik icerik)
        {
            IResult result = _ıcerikService.Update(icerik);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("delete")]
        public ActionResult Delete(int id)
        {
            IResult result = _ıcerikService.Delete(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}