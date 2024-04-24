using Business.Helpers;
using Business.Management;
using Models.Classes;
using Models.Enums;

namespace D.AndradeTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lecturesManager = LectureManager.Instance;
            var trackManager = TrackManager.Instance;

            var rawLectures = FileHandler.ReadFileLines(@".\input.txt");

            var lectures = lecturesManager.ParseLectures(rawLectures);

            Console.WriteLine($"Total de Palestras encontradas: {lectures.Count}");

            var tracks = trackManager.GenerateTracks(lectures);

            var totalTracks = tracks.Count();

            Console.WriteLine($"Trilhas: {totalTracks}");

            for (int i = 0; i < totalTracks; i++)
            {
                Console.WriteLine($"[Trilha {i+1}]\n{tracks.ElementAt(i)}\n");
            }
        }
    }
}
