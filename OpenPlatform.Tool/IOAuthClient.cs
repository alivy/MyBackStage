using System;
using GeRenXing.OpenPlatform;
using OpenPlatform.Tool.Commom;

namespace OpenPlatform.Tool
{
   
    public interface IOAuthClient
    {
        AuthOption Option { get; }
        AuthToken Token { get; }
        IUserInterface User { get; }

        String GetAuthorizeUrl(ResponseType responseType);
        AuthToken GetAccessTokenByAuthorizationCode(string code);
        AuthToken GetAccessTokenByPassword(string passport, string password);
        AuthToken GetAccessTokenByRefreshToken(string refreshToken);
        String Get(String url, params RequestOption[] options);
        String Post(String url, params RequestOption[] options);
    }

}
