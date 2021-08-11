using System;

namespace Encrypt_Decrypt
{
    class Program
    {
        static void Main()
        {
            Console.Write("Please Enter Password: ");
            string password = Console.ReadLine();
            CheckPasswordError(password);

            string inputFilePath = @"D:\enc.txt";
            string outputFilePath = @"D:\decr.txt";
            FileEncryptor encryptor = new(inputFilePath, outputFilePath);
            encryptor.Encrypt(password);

            Console.Write("Please Enter Password: ");
            password = Console.ReadLine();
            CheckPasswordError(password);

            inputFilePath = @"D:\decr.txt";
            outputFilePath = @"D:\decrToenc.txt";

            encryptor.Decrypt(password, inputFilePath, outputFilePath);
        }

        public static void CheckPasswordError(string password)
        {
            if (ProcessExpection.HasError())
            {
                Console.WriteLine(ProcessExpection.ThrowMessage(password));
            }
        }
    }
}
