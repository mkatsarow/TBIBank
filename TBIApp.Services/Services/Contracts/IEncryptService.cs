using System;
using System.Collections.Generic;
using System.Text;

namespace TBIApp.Services.Services.Contracts
{
    public interface IEncryptService
    {
        string EncryptString(string text);

        string DecryptString(string text);
    }
}
