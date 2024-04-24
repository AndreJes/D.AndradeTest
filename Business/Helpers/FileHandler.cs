using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helpers
{
    public static class FileHandler
    {
        public static IEnumerable<string> ReadFileLines(string path)
        {
            List<string> fileContents = [];
            try
            {
                using StreamReader sr = new StreamReader(path);

                string? line = sr.ReadLine();
                while (line != null)
                {
                    fileContents.Add(line);
                    line = sr.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception while trying to read file: {path}: {e.Message}");
            }

            return fileContents;
        }
    }
}
