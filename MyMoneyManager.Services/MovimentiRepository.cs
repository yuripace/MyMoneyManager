using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iphaser.Agenti.Model;
namespace Iphaser.Services
{
    public class MovimentiRepository
    {
        private IphaserEntities context;
        public MovimentiRepository(IphaserEntities _context)
        {
            _context = context;
        }
        public void Aggiungi(Movimenti value)
        {
            context.Movimenti.Add(value);
        }
    }
}
