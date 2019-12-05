using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokesharp.Game.Models
{
    public class PokemonSearch 
    {

        public int LocalID
        {
            get;
            set;
        }

        public int PlayerID
        {
            get;
            set;
        }

    }
}

