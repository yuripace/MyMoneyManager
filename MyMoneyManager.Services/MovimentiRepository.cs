using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMoneyManager.Models;
namespace Iphaser.Services
{
    public class MovimentiRepository
    {
        private MyMoneyManagerEntities context;
        public MovimentiRepository(MyMoneyManagerEntities _context)
        {
            _context = context;
        }
        public void Aggiungi(Movimenti value)
        {
            context.Movimenti.Add(value);
        }
    }
}
