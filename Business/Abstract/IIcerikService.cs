using Core.Service;
using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using Entities.DTOs;
namespace Business.Abstract
{
    public interface IIcerikService : IServiceRepository<Icerik>
    {
        IDataResult<List<IcerikDetailDto>> GetIcerikDetails();
        IDataResult<List<IcerikDetailDto>> GetIcerikDetailsByUserId(int id);
        IDataResult<IcerikDetailDto> GetIcerikDetailsById(int id);
    }
}
