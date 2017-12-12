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

        static void Main(string[] args)
        {
            int populaitionSize = 10; //input
            List<Customer> customer = new List<Customer>();//input
            List<Depot> depot = new List<Depot>();//input

            Chromosome ChromosomeSample = GenerateChromosomeSample(depot,customer);

            List<Chromosome> population = GeneratePopulation(populaitionSize, ChromosomeSample);

            


        }

        public static Chromosome GenerateChromosomeSample(List<Depot> depot , List<Customer> customer)
        {
            Chromosome ChromosomeSample = new Chromosome(depot.Count);
            
            for (int i = 0; i < customer.Count; i++)
            {
                double min = Functions.EuclideanDistance(customer[i], depot[0]); ;
                int k = 0;
                for (int j = 1; j < depot.Count; j++)
                {
                    double temp = Functions.EuclideanDistance(customer[i], depot[j]);
                    if (min > temp)
                    {
                        min = temp;
                        k = j;
                    }
                }
                ChromosomeSample.ChromosomeList[k].Add(customer[i]);
            }

            for (int i = 0; i < ChromosomeSample.ChromosomeList.Count; i++)
            {

//                ChromosomeSample.ChromosomeList[i] = Functions.Matrix(ChromosomeSample.ChromosomeList[i]);
            }

            return ChromosomeSample;
        }

        public static List<Chromosome> GeneratePopulation (int PopulaitionSize , Chromosome chromosomeSample)
        {

            List<Chromosome> population = new List<Chromosome>();

            var random = new Random();
            for (int i = 0; i < PopulaitionSize; i++)
            {
                Chromosome clone = chromosomeSample.Clone();
                for (int j = 0; j < clone.ChromosomeList.Count; j++)
                {
                    Depot temp = new Depot();
                    temp.Id = clone.ChromosomeList[j].Id;
                    temp.X = clone.ChromosomeList[j].X;
                    temp.Y = clone.ChromosomeList[j].Y;
                    while (clone.ChromosomeList[j].Count == 0)
                    {
                        int ran = random.Next(clone.ChromosomeList[j].Count);
                        //------nagesh------
                    }
                }
                int x = random.Next();
            }

            return population;
        }


    }
}
