using Flim.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Application.Interfaces
{
    public interface ISlotService
    {
        Task<bool> CreateSlotAsync(SlotDTO slotDTO);
    }
}
