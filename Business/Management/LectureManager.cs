using Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Management
{
    public class LectureManager
    {
        private static LectureManager? _instance = null;

        private readonly uint _STANDARD_QUICK_DURATION = 5;

        private LectureManager() { }

        public static LectureManager Instance 
        { 
            get 
            { 
                _instance ??= new();

                return _instance; 
            } 
        }

        private bool ValidateLectureString(string str)
        {
            return true;
        }

        private uint ParseDuration(string rawTime)
        {

            if (rawTime == "relâmpago" || rawTime == "relampago")
            {
                return _STANDARD_QUICK_DURATION;
            }

            rawTime = rawTime.TrimEnd(['m', 'i', 'n']).Trim();

            try
            {
                return uint.Parse(rawTime);
            }
            catch (Exception)
            {
                throw;
            }


        }

        public IList<Lecture> ParseLectures(IEnumerable<string> rawLectures)
        {
            var lectureList = new List<Lecture>();
            foreach(string rawLecture in rawLectures)
            {
                if (rawLecture == "")
                {
                    continue;
                }

                if (!ValidateLectureString(rawLecture))
                {
                    Console.WriteLine($"Invalid lecture input: ''{rawLecture}''");
                    throw new Exception($"Invalid lecture input: ''{rawLecture}''");
                }

                try
                {
                    var rawTime = rawLecture.Split(' ').Last();

                    var description = rawLecture.Replace(rawTime, "").Trim();

                    uint durationMinutes = ParseDuration(rawTime);

                    lectureList.Add(new(durationMinutes, description));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed to create ''Lecture'' instance from string: {rawLecture}, error: {e.Message}");
                    throw;
                }
            }

            return lectureList;
        }
    }
}
