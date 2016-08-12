using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Signature
{
    class Sha256
    {   
        public void Convert(string dn)
        {
          
             string dirName = dn;    
             try
                {
                    DirectoryInfo dir = new DirectoryInfo(dirName);
                    FileInfo[] files = dir.GetFiles("*.dat");
                    SHA256 mySHA256 = SHA256Managed.Create();

                    byte[] hashValue;
                    foreach (FileInfo fInfo in files)
                    {
                        FileStream fileStream = fInfo.Open(FileMode.Open);
                        fileStream.Position = 0;
                        hashValue = mySHA256.ComputeHash(fileStream);
                        Console.Write("\n(hash) " + fInfo.Name + ":\n");
                        PrintByteArray(hashValue);
                        fileStream.Close();
                    }
                    return;
                }
            catch (Exception ex)
            {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("\nStackTrace ---\n{0}", ex.StackTrace);
                        Console.Beep();
            }
        }
        private static void PrintByteArray(byte[] array)
        {
            int i;
            for (i = 0; i < array.Length; i++)
            {
                Console.Write(String.Format("{0:X2}", + array[i]));
                if ((i % 4) == 3) Console.Write("\n");
            }
            Console.WriteLine();
        }
    }
}

