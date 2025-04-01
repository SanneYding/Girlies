using mementoGame;
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        saveManager manager = new saveManager();
        GameState player = new GameState(100, 1, new List<string> { "Sword", "Shield" });

        manager.Save(player); // Save state

        // Modify player state
        player = new GameState(50, 2, new List<string> { "Sword", "Shield", "Potion" });
        Console.WriteLine("Current State:");
        player.Display();

        // Load previous state
        player = manager.Load(0);
        Console.WriteLine("\nAfter Loading Save:");
        player.Display();
    }
}
