using Pokesharp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokesharp.Game.Models
{
    public class PlayerUpdate 
    {

        public int ID 
        {
            get;
            set;
        }

        public string Name 
        {
            get;
            set;
        }

    }

}

