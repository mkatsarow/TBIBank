using System.Threading.Tasks;
using TBIApp.Services.Services.Contracts;

namespace TBIApp.Services.Services
{
    public class CheckEgnService : ICheckEgnService
    {
        public async Task<bool> IsRealAsync(string egn)
        {
            await Task.Delay(0);
            Task.Run(() => { });
            int[] egnArray = new int[10];

            if(egn.Length!=10)
            {
                return false;
            }
            for(int i=0;i<egn.Length;i++)
            {
                if(!(char.IsDigit(egn[i])))
                {
                    return false;
                }
                egnArray[i] = egn[i]-48;
            }
            if(!isValidMonthAndDate(egnArray))
            {
                return false;
            }
            var sumEgn = EGNCount(egnArray);


            if (sumEgn%11==10)
            {
                if(egnArray[9]==0)
                {
                    return true;
                }
                return false;
            }
            if(sumEgn%11==egnArray[9])
            {
                return true;
            }
            return false;
        }

        public bool isValidMonthAndDate(int[] egnArray)
        {
            if(egnArray[2]*10+egnArray[3]>12)
            {
                return false;
            }
            if(egnArray[4]*10+egnArray[5]>31)
            {
                return false;
            }
            if(egnArray[2]*10+egnArray[3]==2)
            {
                if(egnArray[4]*10+egnArray[5]>28)
                {
                    return false;
                }
            }
            return true;
        }

        public int EGNCount(int[] egnArray)
        {
            return 2 * egnArray[0] + 4 * egnArray[1] + 8 * egnArray[2] + 5 * egnArray[3] +
                10 * egnArray[4] + 9 * egnArray[5] + 7 * egnArray[6]
                + 3 * egnArray[7] + 6 * egnArray[8];
        }
    }
}
