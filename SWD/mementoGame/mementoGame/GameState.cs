using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mementoGame
{

    //MEMENTO
    public class GameState
    {
        public int Health { get; private set; }
        public int Position { get; private set; }
        public List<string> Inventory { get; private set; }

        public GameState(int health, int position, List<string> inventory)
        {
            Health = health;
            Position = position;
            Inventory = new List<string>(inventory);
        }

        public void Display()
        {
            Console.WriteLine($"Health: {Health}, Position: {Position}, Inventory: {string.Join(", ", Inventory)}");
        }
    }
}
