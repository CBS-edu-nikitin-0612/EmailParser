using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EmailParser
{
    static class SearchEmailsFromEmlFilesInDirectory
    {
        public static void EmailWriterToFile(object directory)
        {
            DirectoryInfo dir = directory as DirectoryInfo;
            StreamWriter mailsTxtWriter = new StreamWriter("mails.txt", false);
            StreamWriter notFoundTxtWriter = new StreamWriter("notFound.txt", false);

            FileInfo[] emlFiles = dir.GetFiles("*.eml");
            if (emlFiles.Length == 0)
            {
                Console.WriteLine("eml files not found.");
            }
            else
            {
                foreach (var emailFile in emlFiles)
                {
                    List<string> emails;
                    using (FileStream stream = emailFile.Open(FileMode.Open, FileAccess.Read))
                    {
                        emails = new EmailParser(stream).GetEmailsAsync().Result;
                    }
                    if (emails.Count > 0)
                    {
                        foreach (var email in emails)
                        {
                            mailsTxtWriter.WriteLine(email);
                        }
                    }
                    else
                    {
                        notFoundTxtWriter.WriteLine(emailFile.Name);
                    }
                }
            }

            mailsTxtWriter.Close();
            notFoundTxtWriter.Close();

            Console.WriteLine("Succesfull. Look mails.txt and notfound.txt");
        }
        public static async Task EmailWriterToFileAsync(DirectoryInfo directory)
        {
            await Task.Factory.StartNew(EmailWriterToFile, directory);
        }
    }
}
