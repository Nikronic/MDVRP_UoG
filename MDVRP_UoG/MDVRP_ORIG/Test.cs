using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;

namespace MDVRP_ORIG
{
    public static class Test
    {
        public static void Main()
        {
            Chromosome chromosome = GetFromTxt("chromosome.txt");
            Customer customer = new Customer(101010, 50, 50, 50) {IsNull = false};
            Insert(customer, chromosome);
            int ii = 0;
            int jj = 0;
            for (int i = 0; i < chromosome.Count; i++)
            {
                Depot depot = chromosome[i];
                for (int j = 0; j < depot.Count; j++)
                {
                    if (depot[j].Id == 101010)
                    {
                        ii = i;
                        jj = j;
                    }
                }
            }
            Console.WriteLine(ii.ToString()+ " "+jj.ToString());
        }
        
        private static Chromosome GetFromTxt(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(@path);
            string chromosomeInfo = lines[0];
            int capacity = int.Parse(chromosomeInfo.Split(' ')[2]);
            List<Depot> depot = new List<Depot>();
            int k = -1;
            for (int i=1; i<lines.Length; i++)
            {
                string[] line = lines[i].Split(' ');
                
                if (line.Length == 3)  // depot
                {
                    k += 1;
                    Depot temp = new Depot(0, int.Parse(line[1]), int.Parse(line[2]), capacity);
                    depot.Add(temp);
                    i += 1;
                }
                string[] args = lines[i].Split(' ');
                int id = int.Parse(args[0]);
                int x = int.Parse(args[1]);
                int y = int.Parse(args[2]);
                int cost = int.Parse(args[3]);
                bool Null = bool.Parse(args[4]);
                Customer customer = new Customer(id, x, y, cost) {IsNull = Null};
                depot[k].Add(customer);
            }

            Chromosome chromosome = new Chromosome(depot.Count, capacity) {ChromosomeList = depot};
            return chromosome;
        }


        private static double EuclideanDistance(Customer customer, Depot depot)
        {
            double distance = Math.Sqrt(Math.Pow((customer.X - depot.X), 2) + Math.Pow((customer.Y - depot.Y), 2));
            return distance;
        }

        private static double EuclideanDistance(Customer customer, Customer customer2)
        {
            double distance =
                Math.Sqrt(Math.Pow((customer.X - customer2.X), 2) + Math.Pow((customer.Y - customer2.Y), 2));
            return distance;
        }

        private static void Insert(Customer customer, Chromosome chromosome)
        {
            double min = EuclideanDistance(customer, chromosome[0]);
            int index = 0;
            for (int i = 1; i < chromosome.Count; i++)
            {
                double temp = EuclideanDistance(customer, chromosome[i]);
                if (temp < min)
                {
                    min = temp;
                    index = i;
                }
            }
            List<double> distance = new List<double>();
            List<int> customerCost = new List<int>();

            int k = 0;
            customerCost.Add(0);
            if (chromosome[index].Count != 0)
            {
                distance.Add(EuclideanDistance(chromosome[index][0], chromosome[index]));
            }
            

            for (int i = 0; i < chromosome[index].Count-1; i++)
            {
                if (chromosome[index][i].IsNull)
                {
                    customerCost.Add(0);
                    distance.Add(0);
                    k++;
                }
                customerCost[k] += chromosome[index][i].Cost;
                distance[k] += EuclideanDistance(chromosome[index][i], chromosome[index][i + 1]);
            }
            
            

            k = 0;
            int index2 = -1;
            min = -1;
            List<double> t3s = new List<double>();
            if ( chromosome[index].Count != 0 && customerCost[k] + customer.Cost <= chromosome[index].Capacity)
            {
                double t1 = EuclideanDistance(chromosome[index][0], chromosome[index]);
                double t2 = EuclideanDistance(chromosome[index][0], customer) + EuclideanDistance(customer, chromosome[index]);
                double t3 = distance[k] - t1 + t2;
                min = t3;
                index2 = 0;
                t3s.Add(t3);
            }
            for (int i = 0; i < chromosome[index].Count-1; i++)
            {
                if (chromosome[index][i].IsNull)
                {
                    k++;
                }
                if ( customerCost[k] + customer.Cost <= chromosome[index].Capacity)
                {
                    double t1 = EuclideanDistance(chromosome[index][i], chromosome[index][i + 1]);
                    double t2 = EuclideanDistance(chromosome[index][i], customer) + EuclideanDistance(customer, chromosome[index][i + 1]);
                    double t3 = distance[k] - t1 + t2;
                    t3s.Add(t3);
                    if (min == -1)
                    {
                        min = t3;
                        index2 = i+1;
                    }
                    else if (min > t3)
                    {
                        min = t3;
                        index2 = i+1;
                    }
                }
            }

            if (index2 == -1)
            {
                Customer nullCustomer = new Customer
                {
                    IsNull = true,
                    X = chromosome[index].X,
                    Y = chromosome[index].Y,
                    Id = 0
                };
                chromosome[index].Add(customer);
                chromosome[index].Add(nullCustomer);
            }
            else
            {
                chromosome[index].Insert(index2 , customer);
            }
        Console.WriteLine("");
        }
    }
}