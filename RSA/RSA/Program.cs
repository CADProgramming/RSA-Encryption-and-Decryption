using System;
using System.Numerics;

namespace RSA
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;

            do
            {
                do
                {
                    Console.Write(" RSA Program ------\n\n[ MENU OPTIONS ]\n\n[1] Encrypt\n[2] Decrypt\n\n[3] Exit\n\nInput Value: ");
                    input = Console.ReadLine();
                    Console.Clear();
                    if (input.Length != 1)
                    {
                        Console.WriteLine("[ INVALID INPUT ]\n");
                    }
                } while (input.Length != 1);

                switch (input)
                {
                    case "1":
                        Encrypt();
                        break;
                    case "2":
                        Decrypt();
                        break;
                }

            } while (input != "3");
        }

        static void Encrypt()
        {
            bool valid = false;
            int e = 0;

            Console.Write("Public Key in form (n, e)\n\nEnter 'n' value: ");
            BigInteger n = Convert.ToUInt64(Console.ReadLine());

            Console.Write("Enter 'e' value: ");
            e = Convert.ToInt32(Console.ReadLine());

            valid = false;
            char[] plainText = new char[0];

            Console.Write("Input plain text: ");
            plainText = Console.ReadLine().Replace(" ", "").ToLower().ToCharArray();
            valid = true;

            string numbers = "";

            for (int i = 0; i < plainText.Length; i++)
            {
                if (Convert.ToInt32(plainText[i]) - 97 < 10)
                {
                    numbers += "0" + Convert.ToString(Convert.ToInt32(plainText[i]) - 97);
                }
                else
                {
                    numbers += Convert.ToString(Convert.ToInt32(plainText[i]) - 97);
                }
            }

            int blockSize = 0;

            do
            {
                try
                {
                    Console.Write("Input digit block size: ");
                    blockSize = Convert.ToInt32(Console.ReadLine());
                    valid = true;

                    if ((blockSize <= 0) || (blockSize > numbers.Length))
                    {
                        valid = false;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid digit block size entry");
                }
            } while (valid == false);

            while ((numbers.Length % blockSize) != 0)
            {
                numbers += "0";
            }

            Console.Write("RSA Encrypted text: ");

            for (int i = 0; i < numbers.Length; i += blockSize)
            {
                int number = 0;
                number = Convert.ToInt32(numbers.Substring(i, blockSize));
                BigInteger cipherNumber = (BigInteger.Pow(number, e)) % n;
                if (cipherNumber.ToString().Length < (n-1).ToString().Length)
                {
                    for (int x = 0; x < ((n - 1).ToString().Length - cipherNumber.ToString().Length); x++)
                    {
                        Console.Write("0");
                    }
                }

                Console.Write($"{cipherNumber} ");
            }

            Console.WriteLine("\n");
        }

        static void Decrypt()
        {
            BigInteger n = 0;
            int e = 0;
            int blockSize = 0;
            int blockSizeAfter = 0;
            int m = 0;
            int invE = 0;
            string cipherText;

            Console.Write("Public Key in form (n, e)\n\nP = C^k mod n\nk = invE mod m\n\nEnter 'n' value: ");
            n = Convert.ToUInt64(Console.ReadLine());
            Console.Write("Enter 'e' value: ");
            e = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter 'm' value: ");
            m = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter cipher text: ");
            cipherText = Console.ReadLine().Trim().Replace(" ", "");
            Console.Write("Enter block size: ");
            blockSize = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter decrypted block size (Usually 1 less than when encrypted): ");
            blockSizeAfter = Convert.ToInt32(Console.ReadLine());

            for (int i = 1; i <= m - 1; i++)
            {
                if (((e * i) % m) == 1)
                {
                    invE = i;
                    break;
                }
            }

            int k = invE % m;
            string plainText = "";
            int count = 0;

            for (int i = 0; i < cipherText.Length; i += blockSize)
            {
                int number = 0;
                number = Convert.ToInt32(cipherText.Substring(i, blockSize));
                BigInteger plainNumber = (BigInteger.Pow(number, k)) % n;
                if (plainNumber.ToString().Length < blockSizeAfter)
                {
                    for (int z = 0; z < (blockSizeAfter - plainNumber.ToString().Length); z++)
                    {
                        plainText += "0";
                    }
                    plainText += plainNumber;
                }
                else
                {
                    plainText += plainNumber;
                }
                count++;
            }

            Console.Write($"Plain Text: ");

            for (int i = 0; i < plainText.Length; i+=2)
            {
                if (i+2 > plainText.Length-1)
                {
                    Console.Write($"{((char)(Convert.ToInt32(plainText.Substring(i, 1)) + 97)).ToString().ToUpper()}");
                }
                else
                {
                    Console.Write($"{((char)(Convert.ToInt32(plainText.Substring(i, 2)) + 97)).ToString().ToUpper()}");
                }
            }

            Console.WriteLine("\n");

        }
    }
}
