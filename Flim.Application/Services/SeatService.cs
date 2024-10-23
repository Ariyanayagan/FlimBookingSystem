using Flim.Application.DTOs;
using Flim.Application.Interfaces;
using Flim.Domain.DTO;
using Flim.Domain.Entities;
using Flim.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Application.Services
{
    public class SeatService : ISeatService
    {
        public readonly IUnitOfWork _unitOfWork;


        public SeatService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task AddSeat(SeatDto seatDto)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                await _unitOfWork.SeatRepository.AddSeat(seatDto);


                await _unitOfWork.SaveAsync();

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception ex) { 

                await _unitOfWork.RollbackTransaction();
                await _unitOfWork.DisposeTransactionAsync();
                throw ex;
            
            }



        }
    }
}
