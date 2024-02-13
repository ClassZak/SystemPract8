using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SystemPract8
{
    internal class Program
    {
        static bool CreateDirectory(string path)
        {
            if (Directory.Exists(path))
                return true;
            else
            {
                if(path.Length == 2)
                    throw new ArgumentException("Нет нужного раздела диска");


                DirectoryInfo dir = new DirectoryInfo(path);


                int slashPos=0;
                do
                    slashPos = path.IndexOf('\\', slashPos+2);
                while
                    (path.IndexOf('\\', slashPos+2) != -1);

                CreateDirectory(path.Substring(0,slashPos));
                dir.Create();
            }
            return Directory.Exists(path);
        }
        public static void CopyDirectory(DirectoryInfo source, DirectoryInfo storage)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
                CopyDirectory(dir, storage.CreateSubdirectory(dir.Name));
            foreach (FileInfo file in source.GetFiles())
                file.CopyTo(storage.FullName+@"\\"+file.Name,true);
        }
        static void Main(string[] args)
        {
            try
            {
                const string path = @"G:\\C#\\MyDir\\temp";

                if (Directory.Exists(path))
                {
                    Console.Write("Директория ");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(path);
                    Console.ResetColor();
                    Console.WriteLine(" была создана");
                }
                else
                {
                    CreateDirectory(path);
                    Console.Write("Директория ");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(path);
                    Console.ResetColor();
                    Console.WriteLine(" создана");
                }
                

                DirectoryInfo dir1 = new DirectoryInfo(path);
                DirectoryInfo dir2= new DirectoryInfo(@"G:\\C#\\111");


                CopyDirectory(dir2, dir1);
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            Console.ReadKey(false);
        }
    }
}
