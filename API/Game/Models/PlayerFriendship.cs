using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokesharp.Game.Models
{
    public class PlayerFriendship
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

    }
}

