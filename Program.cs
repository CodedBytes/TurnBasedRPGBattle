using System;
using System.Threading;
using TurnBased_RPG_Battle_System.Classes;

namespace TurnBased_RPG_Battle_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Starting the game.
            Game game = new Game();
            Console.WriteLine("System -> Staring the game...\n\n");
            Thread.Sleep(4000);
            Console.Clear();// Clean the window.
            game.Start();
        }
    }
}