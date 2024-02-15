using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlTypes;

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
        public static void ReadDirectoryAttributes(DirectoryInfo storage)
        {
            Console.Write("Файлы директории ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(storage.FullName);
            Console.ResetColor();
            Console.WriteLine(":");
            foreach(FileInfo file in storage.GetFiles("*.*"))
            {
                Console.Write("Файл ");
                Console.ForegroundColor= ConsoleColor.DarkGray;
                Console.Write(file);
                Console.ResetColor();
                Console.WriteLine(":");

                
                Console.WriteLine("Имя:");
                Console.WriteLine(file.Name);

                Console.WriteLine("Расширение:");
                Console.WriteLine(file.Extension);

                Console.WriteLine("Имя родительского каталога:");
                Console.WriteLine(file.DirectoryName);

                Console.WriteLine("Время создания:");
                Console.WriteLine(file.CreationTime);

                Console.WriteLine("Время последнего доступа к файлу:");
                Console.WriteLine(file.LastAccessTime);

                Console.WriteLine("Время последнего изменения:");
                Console.WriteLine(file.LastWriteTime);

                Console.WriteLine("Аттрибуты:");
                foreach(FileAttributes fattr in Enum.GetValues(typeof(FileAttributes)))
                {
                    if ((file.Attributes & fattr) == fattr)
                        Console.WriteLine(fattr);
                }
            }
            foreach(DirectoryInfo directoryInfo in storage.GetDirectories())
            {
                
                Console.Write("Директория ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(directoryInfo.FullName);
                Console.ResetColor();
                Console.WriteLine(":");

                Console.WriteLine("Имя:");
                Console.WriteLine(directoryInfo.Name);
                Console.WriteLine("Время создания:");
                Console.WriteLine(directoryInfo.CreationTime);
                Console.WriteLine("Время последнего доступа к файлу:");
                Console.WriteLine(directoryInfo.LastAccessTime);
                Console.WriteLine("Время последнего изменения:");
                Console.WriteLine(directoryInfo.LastWriteTime);


                Console.WriteLine("Аттрибуты:");
                foreach (FileAttributes fattr in Enum.GetValues(typeof(FileAttributes)))
                {
                    if ((directoryInfo.Attributes & fattr) == fattr)
                        Console.WriteLine(fattr);
                }
                Console.WriteLine();
                Console.WriteLine("Файлы и поддиректории:");

                ReadDirectoryAttributes(directoryInfo);
                Console.WriteLine();
            }
        }
        public static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }

        static void HideFiles(DirectoryInfo storage)
        {
            foreach(FileInfo f in storage.GetFiles())
                File.SetAttributes(f.FullName, FileAttributes.Hidden);
            foreach(DirectoryInfo d in storage.GetDirectories())
                HideFiles(d);
        }
        static void DeleteDirectory(DirectoryInfo storage)
        {
            foreach(DirectoryInfo d in storage.GetDirectories())
                DeleteDirectory(d);
            foreach(FileInfo f in storage.GetFiles())
                f.Delete();
            storage.Delete();
        }
        static void DeleteSubdirectories(DirectoryInfo storage, bool deleteFiles=true)
        {
            foreach (DirectoryInfo d in storage.GetDirectories())
                DeleteDirectory(d);
            if(deleteFiles)
            foreach (FileInfo f in storage.GetFiles())
                f.Delete();
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
                    Console.WriteLine(" была ранее создана");
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

                Console.WriteLine("Копируем директорию");
                CopyDirectory(dir2, dir1);
                Console.WriteLine();

                ReadDirectoryAttributes(dir1);
                Console.ForegroundColor= ConsoleColor.DarkRed;
                Console.WriteLine("Делаем все файлы скрытыми");
                Console.WriteLine("Для продолжения нажмите клавишу . . .");
                Console.ReadKey(false);
                Console.ResetColor();


                Console.Clear();
                HideFiles(dir1);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Директория после изменений:");
                Console.ResetColor();
                ReadDirectoryAttributes(dir1);
                

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("Удаляем поддиректории ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(path);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Для продолжения нажмите клавишу . . .");
                Console.ReadKey(false);
                Console.ResetColor();

                Console.Clear();
                DeleteSubdirectories(dir1);
                Console.Write("Директории ");
                Console.ForegroundColor=ConsoleColor.DarkGray;
                Console.Write(path);
                Console.ResetColor();
                Console.WriteLine(" удалены");

            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            Console.WriteLine("Для продолжения нажмите клавишу . . .");
            Console.ReadKey(false);
        }
    }
}
