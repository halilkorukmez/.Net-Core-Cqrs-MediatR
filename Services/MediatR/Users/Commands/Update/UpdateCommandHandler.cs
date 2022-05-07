using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.ComplexTypes;
using Core.Utilities.Result.Concrete;
using DataAccess.UnitOfWork;
using Entities.Users;
using MediatR;

namespace Services.MediatR.Users.Commands.Update;

public class UpdateCommandHandler : IRequestHandler<UpdateCommand, IResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public UpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<IResult> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userdata = await _unitOfWork.Users.GetAsync(p => p.Id == request.Id);
            if (userdata != null)
            {
                userdata.UserName = request.UserName; 
                userdata.Password = request.Password; 
                userdata.IsActive = request.IsActive; 
                userdata.UpdateDate = request.UpdateDate = DateTime.Now;
                await _unitOfWork.Users.UpdateAsync(userdata).ContinueWith(t => _unitOfWork.SaveAsync()); 
                return new Result(ResultStatus.Success, $"{userdata.UserName} Kulanıcı Güncellendi."); 
            }
            return new Result(ResultStatus.Error, "Kullanıcı Güncellenemedi");
        }
        catch (Exception e)
        {
            throw new ArgumentException(e.Message);
        }
    }
}