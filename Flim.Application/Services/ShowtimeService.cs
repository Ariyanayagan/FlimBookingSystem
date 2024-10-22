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
    public class ShowtimeService : IShowtimeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Showtime> _showtimeRepository;

        public ShowtimeService(IGenericRepository<Showtime> showtimeRepository, IUnitOfWork unitOfWork)
        {
            _showtimeRepository = showtimeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Showtime> CreateShowtimeAsync(ShowTimeDTO showtimeDto)
        {
            await _unitOfWork.BeginTransaction();
            var flim = await _unitOfWork.Repository<Film>().GetByIdAsync(showtimeDto.FilmId);

            if(flim is null)
            {
                throw new Exception($"Movie Does not exist id => {showtimeDto.FilmId}");
            }

            var showtime = new Showtime
            {
                FilmId = showtimeDto.FilmId,
                StartTime = showtimeDto.StartTime,
                Film = flim
            };

            await _showtimeRepository.InsertAsync(showtime);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransaction();

            return showtime;
        }

        public async Task<IEnumerable<Showtime>> GetShowtimesByFilmAsync(int filmId)
        {
            return await _showtimeRepository.FindAsync(show => show.FilmId == filmId);
        }
    }
}
