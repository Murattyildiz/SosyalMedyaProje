using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingconcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class IcerikManager : IIcerikService
    {
        private readonly IIcerikDal _ıcerikDal;
        private readonly ICommentService _commentService;

        public IcerikManager(IIcerikDal icerikDal, ICommentService commentService)
        {
            _ıcerikDal = icerikDal;
            _commentService = commentService;
        }

        [LogAspect(typeof(FileLogger))]
        [ValidationAspect(typeof(IcerikValidator))]
        [SecuredOperation("admin,user")]
        [CacheRemoveAspect("IIcerikService.Get")]
        public IResult Add(Icerik entity)
        {
            _ıcerikDal.Add(entity);
            return new SuccessResult(Messages.Icerik_Add);
        }

        [LogAspect(typeof(FileLogger))]
        [SecuredOperation("admin,user")]
        [CacheRemoveAspect("IIcerikService.Get")]
        public IResult Delete(int id)
        {
            var deleteIcerik = _ıcerikDal.Get(x => x.Id == id);
            if (deleteIcerik != null)
            {
                var deledetComment = _commentService.GetbyIcerikId(deleteIcerik.Id);
                if (deledetComment != null)
                {
                    foreach (var item in deledetComment.Data)
                    {
                        _commentService.Delete(item.Id);
                    }
                }
                _ıcerikDal.Delete(deleteIcerik);
                return new SuccessResult(Messages.Icerik_Deleted);
            }
            return new ErrorResult(Messages.IcerikNotFound);

        }

        [CacheAspect(2)]
        public IDataResult<List<Icerik>> GetAll()
        {
            return new SuccessDataResult<List<Icerik>>(_ıcerikDal.GetAll(), Messages.Iceriks_Listed);
        }

        [CacheAspect(2)]
        public IDataResult<List<IcerikDetailDto>> GetIcerikDetails()
        {
            return new SuccessDataResult<List<IcerikDetailDto>>(_ıcerikDal.GetIcerikDetails(), Messages.IcerikWithDetailListed);
        }

        [CacheAspect(2)]
        public IDataResult<IcerikDetailDto> GetIcerikDetailsById(int id)
        {
            return new SuccessDataResult<IcerikDetailDto>(_ıcerikDal.GetIcerikDetailsById(x => x.Id == id), Messages.IcerikWithDetailListed);
        }

        [CacheAspect(2)]
        public IDataResult<List<IcerikDetailDto>> GetIcerikDetailsByUserId(int id)
        {
            return new SuccessDataResult<List<IcerikDetailDto>>(_ıcerikDal.GetIcerikDetails(x => x.UserId == id), Messages.IcerikWithDetailListed);
        }


        [CacheAspect(2)]
        public IDataResult<Icerik> GetEntityById(int id)
        {
            return new SuccessDataResult<Icerik>(_ıcerikDal.Get(x => x.Id == id), Messages.Icerik_Listed);
        }

        [LogAspect(typeof(FileLogger))]
        [SecuredOperation("admin,user")]
        [CacheRemoveAspect("IIcerikService.Get")]
        public IResult Update(Icerik entity)
        {
            _ıcerikDal.Update(entity);
            return new SuccessResult(Messages.Icerik_Edit);
        }
    }
}