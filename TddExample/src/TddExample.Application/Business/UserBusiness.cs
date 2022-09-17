using AutoMapper;
using TddExample.Application.Interface;
using TddExample.Application.Model;
using TddExample.Data.Interface;

namespace TddExample.Application.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserBusiness(
            IMapper mapper,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserModel> GetProfileAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(new Guid(userId));
            return _mapper.Map<UserModel>(user);
        }
    }
}
