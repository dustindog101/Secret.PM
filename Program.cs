using System;
using System.Security.Cryptography;
using System.Text;
namespace SecretPM
{
    class Program
    {
        static void Main(string[] args)
        {
            Internals a = new Internals();
            int b = 0;
            while (true)
            {
                
                if (b ==3)
                {
                    Console.Clear();
                    b = 0;
                }

                b++;
                try
                {

                Console.WriteLine($"{a.start()} \n");

                }
                catch (Exception ex)
                {

                    Console.WriteLine($"\n\n Please enter a valid secure message : {ex.Message} \n");
                }
            }
          

        }
    }
}
