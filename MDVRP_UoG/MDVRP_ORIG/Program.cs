using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MDVRP_ORIG
{
    class Program
    {

        static void Main(string[] args)
        {
            int populaitionSize = 10; //input
            int capacity = 80; //input
            int n = 1800;

            Customer c1 = new Customer(1,37,52,7);
            Customer c2 = new Customer(2, 49, 49, 30);
            Customer c3 = new Customer(3, 52, 64, 16);
            Customer c4 = new Customer(4, 20, 26, 9);
            Customer c5 = new Customer(5, 40, 30, 21);
            Customer c6 = new Customer(6, 21, 47, 15);
            Customer c7 = new Customer(7, 17, 63, 19);
            Customer c8 = new Customer(8, 31, 62, 23);
            Customer c9 = new Customer(9, 52, 33, 11);
            Customer c10 = new Customer(10, 51, 21, 5);
            Customer c11 = new Customer(11, 42, 41, 19);
            Customer c12 = new Customer(12, 31, 32, 29);
            Customer c13 = new Customer(13, 5, 25, 23);
            Customer c14 = new Customer(14, 12, 42, 21);
            Customer c15 = new Customer(15, 36, 16, 10);
            Customer c16 = new Customer(16, 52, 41, 15);
            Customer c17 = new Customer(17, 27, 23, 3);
            Customer c18 = new Customer(18, 17, 33, 41);
            Customer c19 = new Customer(19, 13, 13, 9);
            Customer c20 = new Customer(20, 57, 58, 28);
            Customer c21 = new Customer(21, 62, 42, 8);
            Customer c22 = new Customer(22, 42, 57, 8);
            Customer c23 = new Customer(23, 16, 52, 16);
            Customer c24 = new Customer(24, 8, 52, 10);
            Customer c25 = new Customer(25, 7, 38, 28);
            Customer c26 = new Customer(26, 27, 68, 7);
            Customer c27 = new Customer(27, 30, 48, 15);
            Customer c28 = new Customer(28, 43, 67, 15);
            Customer c29 = new Customer(29, 58, 48, 6);
            Customer c30 = new Customer(30, 58, 27, 19);
            Customer c31 = new Customer(31, 37, 69, 11);
            Customer c32 = new Customer(32, 38, 46, 12);
            Customer c33 = new Customer(33, 46, 10, 23);
            Customer c34 = new Customer(34, 61, 33, 26);
            Customer c35 = new Customer(35, 62, 63, 17);
            Customer c36 = new Customer(36, 63, 69, 6);
            Customer c37 = new Customer(37, 32, 22, 9);
            Customer c38 = new Customer(38, 45, 35, 15);
            Customer c39 = new Customer(39, 59, 15, 14);
            Customer c40 = new Customer(40, 10, 17, 27);
            Customer c41 = new Customer(41, 5, 6, 7);
            Customer c42 = new Customer(42, 21, 10, 13);
            Customer c43 = new Customer(43, 15, 64, 11);
            Customer c44 = new Customer(44, 30, 15, 16);
            Customer c45 = new Customer(45, 39, 10, 10);
            Customer c46 = new Customer(46, 32, 39, 5);
            Customer c47 = new Customer(47, 25, 32, 25);
            Customer c48 = new Customer(48, 25, 55, 17);
            Customer c49 = new Customer(49, 48, 28, 18);
            Customer c50 = new Customer(50, 56, 37, 10);

            Depot d1 = new Depot(1, 20, 20, capacity);
            Depot d2 = new Depot(2, 30, 40, capacity);
            Depot d3 = new Depot(3, 50, 30, capacity);
            Depot d4 = new Depot(4, 60, 50, capacity);

            List<Customer> customer = new List<Customer>() { c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14, c15, c16, c17, c18, c19, c20, c21, c22, c23, c24, c25, c26, c27, c28, c29, c30, c31, c32, c33, c34, c35, c36, c37, c38, c39, c40, c41, c42, c43, c44, c45, c46, c47, c48, c49, c50 };//input
            List<Depot> depot = new List<Depot>() { d1, d2, d3, d4};//input

            //------------------------
            for (int i = 0; i < customer.Count; i++)
            {
                for (int j = 0; j < depot.Count; j++)
                {
                    Console.WriteLine(customer[i].Id + " - " + depot[j].Id + " = " + Functions.EuclideanDistance(customer[i], depot[j]));
                }
            }
            Console.WriteLine("  -------   ");
            for (int i = 0; i < customer.Count; i++)
            {
                for (int j = 0; j < customer.Count; j++)
                {
                    Console.WriteLine(customer[i].Id + " - " + customer[j].Id + " = " + Functions.EuclideanDistance(customer[i], customer[j]));
                }
            }
            Console.WriteLine("-----");
            //------------------------

            Chromosome chromosomeSample = GenerateChromosomeSample(depot,customer,capacity);

            for (int j = 0; j < chromosomeSample.Count; j++)
            {
                for (int k = 0; k < chromosomeSample[j].Count; k++)
                {
                    Console.Write(chromosomeSample[j][k].Id +"  ");
                }
                Console.Write(" }{ ");
            }
            Console.WriteLine();

            List<Chromosome> population = GeneratePopulation(populaitionSize, chromosomeSample);

            CalculationFitness(population);

            for (int i = 0; i < population.Count; i++)
            {
                for (int j = 0; j < population[i].Count; j++)
                {
                    for (int k = 0; k < population[i][j].Count; k++)
                    {
                        Console.Write(population[i][j][k].Id +"   ");
                    }
                    Console.Write(" }{  ");
                }
                Console.Write(population[i].Fitness + "   ");
                Console.WriteLine();
            }

            Console.WriteLine("  ");
            Console.WriteLine("-----");

            for (int i = 0; i < n; i++)
            {
                population = GenerateNewPopulation(population);
                //jahesh
                CalculationFitness(population);

                for (int o = 0; o < population.Count; o++)
                {
                    for (int j = 0; j < population[o].Count; j++)
                    {
                        for (int k = 0; k < population[o][j].Count; k++)
                        {
                            Console.Write(population[o][j][k].Id + "   ");
                        }
                        Console.Write(" }{  ");
                    }
                    Console.Write(population[o].Fitness + "   ");
                    Console.WriteLine();
                }

                Console.WriteLine("  ");
                Console.WriteLine("-----");
            }
            Console.ReadLine();

        }

        public static Chromosome GenerateChromosomeSample(List<Depot> depot , List<Customer> customer , int capacity)
        {
            Chromosome chromosomeSample = new Chromosome(depot.Count,capacity);
            for (int i = 0; i < depot.Count; i++)
            {
                chromosomeSample[i].Id = depot[i].Id;
                chromosomeSample[i].X = depot[i].X;
                chromosomeSample[i].Y = depot[i].Y;
                chromosomeSample[i].Capacity = depot[i].Capacity;
            }
            for (int i = 0; i < customer.Count; i++)
            {
                double min = Functions.EuclideanDistance(customer[i], chromosomeSample[0]); ;
                int k = 0;
                for (int j = 1; j < chromosomeSample.Count; j++)
                {
                    double temp = Functions.EuclideanDistance(customer[i], chromosomeSample[j]);
                    if (min > temp)
                    {
                        min = temp;
                        k = j;
                    }
                }
                chromosomeSample[k].Add(customer[i]);
            }
            return chromosomeSample;
        }

        public static List<Chromosome> GeneratePopulation (int populaitionSize , Chromosome chromosomeSample)
        {
            List<Chromosome> population = new List<Chromosome>();
            for (int i = 0; i < populaitionSize; i++)
            {
                Chromosome clone = chromosomeSample.Clone();
                for (int j = 0; j < clone.Count; j++)
                {
                    clone[j].RandomList();
                    clone[j].Routing();
                }

                Console.WriteLine("----");
                for (int j = 0; j < clone.Count; j++)
                {
                    for (int k = 0; k < clone[j].Count; k++)
                    {
                        Console.Write(clone[j][k].Id + "  ");
                    }
                }
                Console.WriteLine("----");

                population.Add(clone);
            }
            return population;
        }

        public static void CalculationFitness (List<Chromosome> populaitionList)
        {
            for (int k = 0; k < populaitionList.Count; k++)
            {
                double route = 0;
                double distance = 0;

                for (int i = 0; i < populaitionList[k].Count; i++)
                {
                    if (populaitionList[k][i].Count != 0)
                    {
                        distance += Functions.EuclideanDistance(populaitionList[k][i][0], populaitionList[k][i]);
                    }
                    
                    for (int j = 0; j < populaitionList[k][i].Count; j++)
                    {
                        if (populaitionList[k][i][j].IsNull == true)
                        {
                            route = route + 1;
                        }
                        if (j+1 != populaitionList[k][i].Count)
                        {
                            distance += Functions.EuclideanDistance(populaitionList[k][i][j], populaitionList[k][i][j + 1]);
                        }
                    }
                }
                populaitionList[k].Fitness = ((route) + (distance));
            }
        }

        public static List<Chromosome> GenerateNewPopulation (List<Chromosome> population)
        {
            List<Chromosome> newPop = new List<Chromosome>();
            double min = population[0].Fitness;
            int index = 0;
            for (int j = 0; j < population.Count; j++)
            {
                if (population[j].Fitness < min)
                {
                    min = population[j].Fitness;
                    index = j;
                }
            }
            newPop.Add(population[index]);
            while (newPop.Count < population.Count)
            {
                List<Chromosome> temp = Functions.CrossOver(Functions.Tournament(population, population.Count));
                newPop.Add(temp[0]);
                newPop.Add(temp[1]);
            }
            if (newPop.Count > population.Count)
            {
                newPop.RemoveAt(newPop.Count - 1);
            }
            return newPop;
        }
        
    }
}
