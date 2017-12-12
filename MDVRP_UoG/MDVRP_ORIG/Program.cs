using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MDVRP_ORIG.Functions;
namespace MDVRP_ORIG
{
    class Program
    {

        public static int PopulaitionSize = 10; //input
        public static List<Customer> Customer = new List<Customer>();//input
        public static List<Depot> Depot = new List<Depot>();//input

        static void Main(string[] args)
        {

            Chromosome ChromosomeSample = GenerateChromosomeSample();           

            // copy gereftan az chromosome sample va jaygasht zadan dar an
            for (int i = 0; i < PopulaitionSize; i++)
            {
                
                
                
            }
            

        }

        public static Chromosome GenerateChromosomeSample()
        {
            Chromosome ChromosomeSample = new Chromosome(Depot.Count);

            for (int i = 0; i < Customer.Count; i++)
            {
                double min = Functions.EuclideanDistance(Customer[i], Depot[0]); ;
                int k = 0;
                for (int j = 1; j < Depot.Count; j++)
                {
                    double temp = Functions.EuclideanDistance(Customer[i], Depot[j]);
                    if (min > temp)
                    {
                        min = temp;
                        k = j;
                    }
                }
                ChromosomeSample.ChromosomeList[k].Add(Customer[i]);
            }

            for (int i = 0; i < ChromosomeSample.ChromosomeList.Count; i++)
            {
                ChromosomeSample.ChromosomeList[i] = Functions.Matrix(ChromosomeSample.ChromosomeList[i]);
            }

            return ChromosomeSample;
        }



        
    }
}
