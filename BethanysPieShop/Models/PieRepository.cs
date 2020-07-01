using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly AppDbContext dbContext;

        public PieRepository(AppDbContext dbContext) 
            => this.dbContext = dbContext;

        public IEnumerable<Pie> AllPies 
            => dbContext.Pies.Include(c=>c.Category).ToList();

        public IEnumerable<Pie> PiesOfWeek 
            => dbContext.Pies.Include(c => c.Category).Where(p=>p.IsPieOfTheWeek);

        public Pie GetPieById(int pieID)
            => dbContext.Pies.FirstOrDefault(p => p.PieId == pieID);
    }
}
