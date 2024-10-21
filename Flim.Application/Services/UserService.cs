using BCrypt.Net;
using Flim.Application.Interfaces;
using Flim.Domain.Entities;
using Flim.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flim.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
      
        public UserService(IGenericRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        // Authenticate user using username and password
        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var foundUser = await _userRepository.FindSingleAsync(u => u.Email == username);
            Debug.WriteLine($"Password Hash : {BCrypt.Net.BCrypt.EnhancedHashPassword(password, HashType.SHA256)}");
            if (foundUser != null && BCrypt.Net.BCrypt.Verify(password, foundUser.Password))
            {
                return foundUser; 
            }
            return null;
        }

        // Register a new user
        public async Task<bool> RegisterAsync(User user)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                await _userRepository.InsertAsync(user);
                await _unitOfWork.SaveAsync();
                _unitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception ex) {

                _unitOfWork.RollbackTransaction();
                throw ex;
            }
            
        }
    }
}
