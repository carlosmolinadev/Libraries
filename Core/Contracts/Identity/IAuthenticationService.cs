using Core.Models.Authentication;

namespace Core.Contracts.Identity
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
        Task<bool> AddRolesAsync(AddRolesRequest request);
    }
}
