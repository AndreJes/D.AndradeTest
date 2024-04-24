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

            Console.WriteLine($"Lendo arquivo de entrada...");
            var rawLectures = FileHandler.ReadFileLines(@".\input.txt");

            Console.WriteLine($"Obtendo dados do arquivo...");
            var lectures = lecturesManager.ParseLectures(rawLectures);

            Console.WriteLine($"Total de Palestras encontradas: {lectures.Count}");

            Console.WriteLine($"Gerando trilhas: {lectures.Count}");
            var tracks = trackManager.GenerateTracks(lectures);

            var totalTracks = tracks.Count();

            Console.WriteLine($"Total de trilhas: {totalTracks}");

            for (int i = 0; i < totalTracks; i++)
            {

                Console.WriteLine($"=====================================================================================");
                Console.WriteLine($"[Trilha {i+1}]\n{tracks.ElementAt(i)}\n");
            }
            Console.WriteLine($"=====================================================================================");

            Console.WriteLine($"Pressione qualquer tecla para finalizar...");
            Console.ReadLine();
        }
    }
}
