using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SecretPM
{
    class Internals
    {
        public string start()
        {
            encryption enc = new encryption();
            generatekey gen = new generatekey();
            string outp = "Please enter a valid selection!";
            string key = gen.generatePrivate();
            string iv = gen.generateIV();
            Console.WriteLine("Would you like to secure(1), read text(2) or exit(3)?");
            string a = Console.ReadLine().ToString();
            if (a == "1")//Encrypt
            {
                Console.Write("Please enter the text you would like to secure: ");
                byte[] ToEncrypt = Encoding.ASCII.GetBytes(Console.ReadLine().ToString());
                outp = key + ":" + iv + ":" + Convert.ToBase64String(encryption.encryptdata(ToEncrypt, key, iv));
            }
            if (a == "2")
            {

                Console.Write("Please enter the text you would like to read: ");
                string[] readd = Console.ReadLine().ToString().Split(":");
                key = readd[0];
                iv = readd[1];
                outp = Encoding.ASCII.GetString(encryption.decryptdata(Convert.FromBase64String(readd[2]),key,iv));

            }
            if (a == "3")
            {
                Environment.Exit(0);
                
            }
            
            
            return ($"{outp}");
        }


    }
    class generatekey
    {
        public string generatePrivate()
        {
            Random rnd = new Random();
            string ret = "";
            int a = 0;
            SHA1 sHA1 = SHA1.Create();
            while (a <rnd.Next(2,1000))
            {
                byte[] input = Encoding.ASCII.GetBytes(rnd.Next(0,999999999).ToString());
                ret +=Convert.ToBase64String(sHA1.ComputeHash(input));
                a++;
            }
            return ret.Substring(0,32);

        }
        public string generateIV()
        {
            Random rnd = new Random();
            string ret = "";
            int a = 0;
            SHA1 sHA1 = SHA1.Create();
            while (a < rnd.Next(1,1000))
            {
                byte[] input = Encoding.ASCII.GetBytes(rnd.Next(0, 999999999).ToString());
                ret += Convert.ToBase64String(sHA1.ComputeHash(input));
                a++;
            }
            return ret.Substring(0,16);

        }
    }
    class encryption
    {

        public static byte[] encryptdata(byte[] bytearraytoencrypt, string key, string iv)//make it byte just in case we need to encrypt a file :shrug:
        {
            try
            {

                using (var dataencrypt = new AesCryptoServiceProvider())
                { //Block size : Gets or sets the block size, in bits, of the cryptographic operation.  
                    dataencrypt.BlockSize = 128;
                    //KeySize: Gets or sets the size, in bits, of the secret key  
                    dataencrypt.KeySize = 128;
                    //Key: Gets or sets the symmetric key that is used for encryption and decryption.  
                    dataencrypt.Key = System.Text.Encoding.UTF8.GetBytes(key);
                    //IV : Gets or sets the initialization vector (IV) for the symmetric algorithm  
                    dataencrypt.IV = System.Text.Encoding.UTF8.GetBytes(iv);
                    //Padding: Gets or sets the padding mode used in the symmetric algorithm  
                    dataencrypt.Padding = PaddingMode.PKCS7;
                    //Mode: Gets or sets the mode for operation of the symmetric algorithm  
                    dataencrypt.Mode = CipherMode.CBC;
                    //Creates a symmetric AES encryptor object using the current key and initialization vector (IV).  
                    ICryptoTransform crypto1 = dataencrypt.CreateEncryptor(dataencrypt.Key, dataencrypt.IV);
                    //TransformFinalBlock is a special function for transforming the last block or a partial block in the stream.   
                    //It returns a new array that contains the remaining transformed bytes. A new array is returned, because the amount of   
                    //information returned at the end might be larger than a single block when padding is added.  
                    byte[] encrypteddata = crypto1.TransformFinalBlock(bytearraytoencrypt, 0, bytearraytoencrypt.Length);
                    crypto1.Dispose();
                    //return the encrypted data  
                    return encrypteddata;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static byte[] decryptdata(byte[] bytearraytodecrypt, string key, string iv)
        {//do i even have to explain??

            using (var keydecrypt = new AesCryptoServiceProvider())
            {
                keydecrypt.BlockSize = 128;
                keydecrypt.KeySize = 128;
                keydecrypt.Key = System.Text.Encoding.UTF8.GetBytes(key);
                keydecrypt.IV = System.Text.Encoding.UTF8.GetBytes(iv);
                keydecrypt.Padding = PaddingMode.PKCS7;
                keydecrypt.Mode = CipherMode.CBC;
                ICryptoTransform crypto1 = keydecrypt.CreateDecryptor(keydecrypt.Key, keydecrypt.IV);

                byte[] returnbytearray = crypto1.TransformFinalBlock(bytearraytodecrypt, 0, bytearraytodecrypt.Length);
                crypto1.Dispose();
                return returnbytearray;
            }
        }

    }
}
