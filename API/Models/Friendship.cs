using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokesharp.Models
{
    public class Friendship
    {

        [Key]
        public int ID {
            get;
            set;
        }

        [ForeignKey("Player")]
        public int PlayerID
        {
            get;
            set;
        }

        [ForeignKey("Friend")]
        public int FriendID 
        {
            get;
            set;
        }

        public virtual Player Friend 
        {
            get;
            set;
        }

        public bool Enabled
        {
            get;
            set;
        }

        public DateTime CreatedAt 
        {
            get;
            set;
        }

    }
}

