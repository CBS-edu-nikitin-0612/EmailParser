using System;
using System.Collections.Generic;
using System.IO;

namespace EmailParser
{
    static class EmailWriterToFile
    {
        public static void SearchEmailsFromEmlFilesInDirectory(DirectoryInfo directory)
        {
            StreamWriter mailsTxtWriter = new StreamWriter("mails.txt", false);
            StreamWriter notFoundTxtWriter = new StreamWriter("notFound.txt", false);

            FileInfo[] emlFiles = directory.GetFiles("*.eml");
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
                        emails = new EmailParser(stream).GetEmails();
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
    }
}
