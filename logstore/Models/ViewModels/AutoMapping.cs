using AutoMapper;
using logstore.Auth;

namespace logstore.Models.ViewModels
{
    class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserViewModel>().BeforeMap((a, b) => a.Password = null);

            //no mapeamento automaticamente converte a string de senha para hash
            CreateMap<UserViewModel, User>().BeforeMap((a, b) => a.Password = AuthHelpers.getHashOfString(a.Password));

            CreateMap<UserAuthViewModel, User>().BeforeMap((a, b) => a.Password = AuthHelpers.getHashOfString(a.Password));
        }


    }
}
