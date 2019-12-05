using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokesharp.Models
{
    public class Player
    {

        [Key]
        public int ID {
            get;
            set;
        }

        public string Username {
            get;
            set;
        }

        public string Password {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        [ForeignKey("Pokedex")]
        public int PokedexID
        {
            get;
            set;
        }

        [ForeignKey("MainPokedexPokemon")]
        public int? MainPokedexPokemonID 
        {
            get;
            set;
        }

        [ForeignKey("PlayerID")]
        public virtual List<Friendship> Friendships {
            get;
            set;
        }

        public virtual PokedexPokemon MainPokedexPokemon 
        {
            get;
            set;
        }
        
        public virtual Pokedex Pokedex
        {
            get;
            set;
        }

        public bool CatchedAnyPokemon() {
            return MainPokedexPokemonID > 0 && MainPokedexPokemon != null;
        }

        public bool Enabled {
            get;
            set;
        }

        public DateTime CreatedAt {
            get;
            set;
        }

        public DateTime UpdatedAt {
            get;
            set;
        }

    }
}

