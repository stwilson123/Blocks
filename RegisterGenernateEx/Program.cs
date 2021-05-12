using Blocks.Framework.License;
using System;

namespace RegisterGenernate
{
    class Program
    {
        static void Main(string[] args)
        {
            var machineInfo = new WindowMachineInfo();
            try
            {
                machineInfo.GenernateRegister();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException.Message);
                Console.ReadKey();
            }
        }
    }
}
