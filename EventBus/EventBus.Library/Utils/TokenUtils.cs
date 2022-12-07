using EventBus.Library.Models;
using System;

namespace EventBus.Library.Utils
{
    public class TokenUtils : ITokenUtils
    {
        public Token GenerateNewToken()
        {
            return new Token() { TokenId = Guid.NewGuid().ToString() };
        }
    }
}
