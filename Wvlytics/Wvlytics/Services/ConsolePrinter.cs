using System;
using Wvlytics.Model;

namespace Wvlytics.Services
{
    public class ConsolePrinterService : IPrinterService
    {
        private readonly object _lock = new object();

        public void PrintScores(MatchHistory match, MatchHistorySnapshot snapshot)
        {
            lock (_lock)
            {
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write("{0}:", snapshot.Timestamp.ToString("u"));

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("{0}: {1} +{2} ", match.RedWorldName, snapshot.Scores.RedScore,
                    snapshot.Scores.RedPotentialPoints);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("{0}: {1} +{2} ", match.GreenWorldName, snapshot.Scores.GreenScore,
                    snapshot.Scores.GreenPotentialPoints);

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("{0}: {1} +{2} ", match.BlueWorldName, snapshot.Scores.BlueScore,
                    snapshot.Scores.BluePotentialPoints);


                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
        } 
    }
}