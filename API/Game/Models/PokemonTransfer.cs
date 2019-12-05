using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokesharp.Game.Models
{
    public class PokemonTransfer
    {

        public int PlayerID
        {
            get;
            set;
        }

        public int FriendID 
        {
            get;
            set;
        }

        public int PokedexPokemonID
        {
            get;
            set;
        }

    }
}

