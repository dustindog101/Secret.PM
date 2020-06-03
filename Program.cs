using System;
using System.Security.Cryptography;
using System.Text;
namespace SecretPM
{
    class Program
    {//sinFzm/BiW9cvyY1FAVDpJNEjss=Dvnk:uSZ18BLnywJjT6zb:cPt8+90S1ElpNA/zaXzmEw==
        static void Main(string[] args)
        {
            Internals a = new Internals();
            int b = 0;
            while (true)
            {

                if (b == 3)
                {
                    Console.Clear();
                    b = 0;
                }

                b++;
                try
                {

                    Console.WriteLine($"{a.start()} \n\n");

                }
                catch (Exception ex)
                {

                    Console.WriteLine($"\n\n Please enter a valid secure message : {ex.Message} \n");
                }
            }


        }
    }
}
