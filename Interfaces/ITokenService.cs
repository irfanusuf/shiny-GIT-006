using System;

namespace P1WebMVC.Interfaces;

public interface ITokenService
{


    public string CreateToken(Guid userId, string email, string username , int expiryTime);


    public Guid VerifyTokenAndGetId(string token);

}
