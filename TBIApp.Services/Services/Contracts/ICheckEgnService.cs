using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TBIApp.Services.Services.Contracts
{
    public interface ICheckEgnService
    {
        Task<bool> IsRealAsync(string egn);
        bool isValidMonthAndDate(int[] egnArray);
        int EGNCount(int[] egnArray);
    }
}
