using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface IIcerikDal : IEntityRepository<Icerik>
    {
        List<IcerikDetailDto> GetIcerikDetails(Expression<Func<IcerikDetailDto, bool>> filter=null);
        IcerikDetailDto GetIcerikDetailsById(Expression<Func<IcerikDetailDto, bool>> filter);
    }
}
