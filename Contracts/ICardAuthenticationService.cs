using System;
using System.Collections.Generic;
using System.Text;

namespace w13_Quiz.Contracts
{
    public interface ICardAuthenticationService
    {
        void Authenticate(string cardNumber, string password);

    }
}
