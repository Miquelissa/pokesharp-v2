using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pokesharp.Models;

namespace Pokesharp.Game.Models 
{
    public class Encounter 
    {

        public int ID 
        {
            get;
            set;
        }

        public int PlayerID
        {
            get;
            set;
        }

        public int LocalID 
        {
            get;
            set;
        }

        public PokemonEncountered PokemonEncountered 
        {
            get;
            set;
        }

        public bool PokemonAlreadyCaught 
        {
            get;
            set;
        }

        public bool CanBattle 
        {
            get;
            set;
        }

    }

}

