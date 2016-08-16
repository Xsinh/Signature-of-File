using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Signature
{
    class Program
    {
        
        static void Main(string[] args)
        {            
                Console.ForegroundColor = ConsoleColor.Cyan;
                string path;
                string file;
                string part;
                Interface(out path, out file, out part);

            try
            {   
                
                var thread = new Thread(() =>
                  {
                      Console.Clear();
                      try
                      {
                          Stream(path, file, part);
                      }

                      catch(Exception ex)
                      {
                          Console.WriteLine(ex.Message);
                          Console.WriteLine("\nStackTrace ---\n{0}", ex.StackTrace);
                          Console.Beep();
                      }
                     
                });
            thread.Start();
            thread.Join();
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("\nStackTrace ---\n{0}", ex.StackTrace);
                Console.Beep();
            }

            Console.Beep();
            Sha256 con = new Sha256();
            con.Convert(path);
            Console.ReadKey();
        }

        private static void Interface(out string path, out string file, out string part)
        {
            Console.WriteLine("Enter a path of the file, name with extension and quantity of parts into which \nyou want to divide it\n");
            Console.WriteLine("Path:");
            path = @"" + (string)Console.ReadLine();

            try
            {
                string[] directory = Directory.GetFiles(path);
                Console.WriteLine("\nFiles in this folder:\n");

                for (int k = 0; k < directory.Length; k++)
                {
                    Console.WriteLine(directory[k]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("\nStackTrace ---\n{0}", ex.StackTrace);
                Console.Beep();
            }

            Console.WriteLine("\nEnter name of file:");
            file = @"" + (string)Console.ReadLine();
            Console.WriteLine("Part:");
            part = (string)Console.ReadLine();
        }

        private static void Stream(string path, string file, string part)
        {
            using (FileStream fs = new FileStream(path + file, FileMode.Open))
            {
                Int64 Flength = fs.Length / Convert.ToInt64(part);
                Int64 Fcount = fs.Length / Flength;

                for (int i = 0; i < Fcount; i++)

                    using (FileStream stream = new FileStream(string.Format(path+ "file_{0,3}.dat", i + 1), FileMode.OpenOrCreate))
                    {
                        long ByteBox = Flength;
                        while (fs.CanRead && ByteBox > 0)
                        {
                            Sha256 b = new Sha256();
                            ByteBox--;
                            stream.WriteByte((byte)fs.ReadByte());

                        }

                        for (int j = 0; j <= 100; j++)
                        {
                            Console.SetCursorPosition(0, i);
                            Console.Write( "file_{0,3}.dat", i+1);
                            Console.WriteLine("         Execute {0}%", j);
                            Thread.Sleep(i);
                        }

                    }
            }
        }
    }
}
