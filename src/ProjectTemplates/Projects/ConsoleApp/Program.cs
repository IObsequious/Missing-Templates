using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace $safeprojectname$
{
    public static class Program
    {
        public static void Main(string[] args)
        {

            PressAnyKeyToContinue();
        }

        private static void PressAnyKeyToContinue()
        {
            Write("Press Any Key To Continue...");
            ReadKey(true);
        }
    }
}
