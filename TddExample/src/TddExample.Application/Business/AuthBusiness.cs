using AutoMapper;
using Microsoft.Extensions.Options;
using TddExample.Application.Configuration;
using TddExample.Application.Interface;
using TddExample.Application.Model;
using TddExample.Data.Interface;
using static TddExample.CrossCutting.Extensions.TokenExtensions;

namespace TddExample.Application.Business
{
    public class AuthBusiness : IAuthBusiness
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly AppSettingsConfiguration _appSettingsConfiguration;
        public AuthBusiness(IMapper mapper, IUserRepository userRepository, IOptions<AppSettingsConfiguration> appSettingsConfiguration)
        {
            _mapper = mapper;
            _appSettingsConfiguration = appSettingsConfiguration.Value;
            _userRepository = userRepository;
        }
        public async Task<AuthModel?> LoginAsync(LoginModel model)
        {
            var user = await _userRepository.AuthenticateAsync(model.Email, model.Password);

            if (user == null) return null;

            var token = CreateToken(user.Id, user.Email, user.Name, _appSettingsConfiguration.SecretKey);

            var result = _mapper.Map<AuthModel>(user);
            result.Token = token;

            return result;
        }

        public async Task<AuthModel?> RegisterAsync(RegisterModel model)
        {
            var user = await _userRepository.AddAsync(model.Name, model.Email, model.Password);

            if (user == null)
                return null;

            var token = CreateToken(user.Id, user.Email, user.Name, _appSettingsConfiguration.SecretKey);

            var result = _mapper.Map<AuthModel>(user);
            result.Token = token;

            return result;
        }
    }
}
