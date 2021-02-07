using System;

namespace thirdProblem
{
    class cntSteps
    {
        public int NumberOfSteps(int num)
        {
            int cnt = 0;
            while (num != 0)
            {
                if (num % 2 != 0)
                {
                    num = -1;
                }
                else
                {
                    num /= 2;
                }
                cnt++;
            }
            return cnt;
        }
    }
}
