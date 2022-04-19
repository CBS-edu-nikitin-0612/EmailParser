using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmailParser
{
    public class EmailParser
    {
        private FileStream stream;

        protected EmailParser() { }
        public EmailParser(FileStream stream)
        {
            this.stream = stream;
        }
        public List<string> GetEmails()
        {
            byte[] buffer;
            string textFromFile = string.Empty;
            var emailsList = new List<string>();
            if (stream != null)
            {
                buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                textFromFile = Encoding.Default.GetString(buffer);
            }
            string[] wordsFromFile = textFromFile.Split(new char[] {'\n', '\r'});

            string pattern = @"^[0-9a-z_-]+@[\S]+\.\S{2,4}$";
            var regex = new Regex(pattern);
            foreach (var item in wordsFromFile)
            {
                if (regex.IsMatch(item))
                {
                    emailsList.Add(item);
                }
            }
            
            return emailsList;
        }
        public async Task<List<string>> GetEmailsAsync()
        {
            return await Task<List<string>>.Factory.StartNew(GetEmails);
        }
    }
}
