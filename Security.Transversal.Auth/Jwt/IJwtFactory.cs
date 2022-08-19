using Security.Transversal.Auth.Entity;

namespace Security.Transversal.Auth.Jwt
{
    public interface IJwtFactory
    {
        UserToken GetJwt(User appUser);
    }
}
