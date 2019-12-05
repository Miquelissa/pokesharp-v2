using Pokesharp.Base;
using Pokesharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Pokesharp.Data {
    public class Locals {

        private readonly Context db;

        public Locals(Context context) {
            db = context;
        }

        public Local FindOneByID(int id) {
            return db.Local
                .Include("Pokemons")
                .Include("Pokemons.Pokemon")
                .Where(local => local.Enabled)
                .FirstOrDefault(_local => _local.ID == id);
        }
    }
}
