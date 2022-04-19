using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace EmailParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.Write("Input fullname directory for search email in eml files:\n>");
            //D:\hardWork\courses\C#\studies\ТЗ\парсер\INBOX.AMAZON REPORT
            DirectoryInfo directory = null;
            bool flag = false;
            while (!flag)
            {
                string fullNameDirectory = Console.ReadLine();
                directory = new DirectoryInfo(fullNameDirectory);
                if (directory.Exists)
                {
                    flag = true;
                }
                else
                {
                    Console.WriteLine("wrong format or path, try again:\n>");
                }
            }

            Task task = SearchEmailsFromEmlFilesInDirectory.EmailWriterToFileAsync(directory);
            
            while (!task.IsCompleted)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.Write("in progress");
                Thread.Sleep(500);
                if (task.IsCompleted) break;
                Console.Write(" . ");
                Thread.Sleep(500);
                if (task.IsCompleted) break;
                Console.Write(" . ");
                Thread.Sleep(500);
                if (task.IsCompleted) break;
                Console.Write(" . ");
            }
            Console.ReadKey();
        }

        
    }
}
