using Models.Classes;
using System.Drawing;

namespace Business.Management
{
    public class TrackManager
    {
        private static TrackManager? _instance = null;

        private TrackManager() { }

        public static TrackManager Instance
        {
            get
            {
                _instance ??= new();

                return _instance;
            }
        }

        public IEnumerable<Track> GenerateTracks(IEnumerable<Lecture> lectures)
        {
            List<Track> tracks = new List<Track>();

            var lecturesList = lectures.ToList();

            // Sorts list in descending order by lecture duration
            lecturesList.Sort((a,b) => { return b.Duration.CompareTo(a.Duration); });

            int currentTrackIndex = -1; // Starts as negative one since we'll increment at the start of the loop and use as a zero based index

            while (lecturesList.Count > 0)
            {
                currentTrackIndex++;
                
                tracks.Add(new());

                var currentTrack = tracks.ElementAt(currentTrackIndex);

                foreach (var session in currentTrack.Sessions)
                {
                    foreach (var lecture in lecturesList.ToList())
                    {
                        // No more lectures with a duration that can be put on the session
                        if (session.RemainingDuration < lecturesList.Min((a) => a.Duration))
                        {
                            break;
                        }

                        if (session.GetLastLectureEndTime().TotalMinutes + lecture.Duration > session.SessionEvent.MaxStartTime.TotalMinutes)
                        {
                            break;
                        }

                        // Finds a lecture that can fit into the remaining time
                        var lectureToAdd = lecturesList.Find((lec) => lecture.Duration < session.RemainingDuration);

                        if (lectureToAdd is not null)
                        {
                            session.AddLecture(lecture);
                            lecturesList.Remove(lecture);
                        }
                    }

                    session.SessionEvent.StartTime = session.GetLastLectureEndTime() <= session.SessionEvent.MinStartTime ? session.SessionEvent.MinStartTime : session.GetLastLectureEndTime();
                }
            }

            return tracks;
        }
    }
}
