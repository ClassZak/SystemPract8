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
        static void Main(string[] args)
        {
            try
            {
                const string path = @"G:\\C#\\MyDir\\temp";

                CreateDirectory(path);
                Console.Write("Директория ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(path);
                Console.ResetColor();
                Console.WriteLine(" создана");
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }
    }
}
