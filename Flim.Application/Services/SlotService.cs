using Flim.Application.DTOs;
using Flim.Application.Interfaces;
using Flim.Domain.Entities;
using Flim.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Application.Services
{
    public class SlotService : ISlotService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IGenericRepository<Slot> _slotRepository;

        public SlotService(IUnitOfWork unitOfWork, IGenericRepository<Slot> genericRepository)
        {
            _unitOfWork = unitOfWork;
            _slotRepository = genericRepository;
        }

        public async Task<bool> CreateSlotAsync(SlotDTO slotDTO)
        {
            var film = await _unitOfWork.Repository<Film>().FindSingleAsync(f=>f.FilmId == slotDTO.FilmId);

            if (film == null)
            {
                throw new Exception("Film not found.");
            }

            var existingSlots = await _slotRepository.FindAsync( s => s.FilmId == slotDTO.FilmId && 
                                                                 s.SlotDate == slotDTO.SlotDate );

            if (existingSlots.Count() >= 3)
            {
                throw new Exception("A film can have a maximum of 3 slots per day.");
            }

            var duplicateSlot = existingSlots.FirstOrDefault(s => s.ShowCategory == slotDTO.ShowCategory);

            if (duplicateSlot != null)
            {
                throw new Exception($"A slot for this film with show category {slotDTO.ShowCategory} already exists on this date.");
            }

            var newSlot = new Slot {

                ShowCategory = slotDTO.ShowCategory,
                FilmId  = slotDTO.FilmId,
                SlotDate = slotDTO.SlotDate
                

            };

            try
            {
                await _unitOfWork.BeginTransaction();

                await _slotRepository.InsertAsync(newSlot);

                await _unitOfWork.SaveAsync();

                await _unitOfWork.CommitTransaction();

                return true;

            }
            catch (Exception ex) { 
            
                await _unitOfWork.RollbackTransaction();
                throw ex;
            }
        }
    }
}
