using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDVRP_ORIG
{
    class Chromosome
    {
        public List<List<int>> ChromosomeList  { get; set; }
        
        public Chromosome(int depots)
        {
            ChromosomeList = new List<List<int>>(depots);
        }


        
    }
}
