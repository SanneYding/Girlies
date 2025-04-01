using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mementoGame { 


    //CARETAKER
    internal class saveManager
    {
        private List<GameState> saves = new();

        public void Save(GameState state) => saves.Add(new GameState(state.Health, state.Position, state.Inventory));
        public GameState Load(int index) => saves[index];
    }
}

