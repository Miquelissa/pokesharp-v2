using Pokesharp.Base;
using Pokesharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Pokesharp.Data {
    public class Regions {

        private readonly Context db;

        public Regions(Context context) {
            db = context;
        }

        public List<Region> List() {
            return db.Region
                .Include("Locals")
                .Include("Locals.Pokemons")
                .Include("Locals.Pokemons.Pokemon")
                .Where(region => region.Enabled)
                .ToList();
        }
    }
}
