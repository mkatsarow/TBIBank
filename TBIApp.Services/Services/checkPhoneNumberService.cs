using System.Threading.Tasks;
using TBIApp.Services.Services.Contracts;

namespace TBIApp.Services.Services
{
    public class CheckPhoneNumberService : ICheckPhoneNumberService
    {
        public async Task<bool> IsRealAsync(string phoneNumber)
        {
            await Task.Delay(0);
            int[] phonenumberarray = new int[10];
            if (phoneNumber.Length != 10)
            {
                return false;
            }
            for (int i = 0; i < phoneNumber.Length; i++)
            {
                if (!(char.IsDigit(phoneNumber[i])))
                {
                    return false;
                }
                phonenumberarray[i] = phoneNumber[i] - 48;
            }
            if (phonenumberarray[0] != 0)
            {
                return false;
            }
            if (phonenumberarray[1] != 8 && phonenumberarray[1] != 9)
            {
                return false;
            }
            if (phonenumberarray[2] != 7 && phonenumberarray[2] != 8 && phonenumberarray[2] != 9)
            {
                return false;
            }
            return true;
        }

    }
}
