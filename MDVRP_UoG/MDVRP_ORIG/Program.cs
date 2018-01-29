﻿using System;
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
            int capacity = 10; //input
            int n = 100;

            //List<Depot> ChromosomeList1 = new List<Depot>();
            //ChromosomeList1[0] = new Depot();

            Customer c1 = new Customer(1,3,4,5);
            Customer c2 = new Customer(2, 6, 2, 5);
            Customer c3 = new Customer(3, 7, 6, 4);
            Customer c4 = new Customer(4, 2, 11, 7);
            Customer c5 = new Customer(5, 8, 15, 1);
            Customer c6 = new Customer(6, 11, 17, 2);
            Customer c7 = new Customer(7, 13, 9, 80);

            Depot d1 = new Depot(1,2,5,10);
            Depot d2 = new Depot(1, 4, 7, 10);
            Depot d3 = new Depot(1, 10, 5, 10);

            List<Customer> customer = new List<Customer>() { c1, c2, c3, c4, c5, c6, c7 };//input
            List<Depot> depot = new List<Depot>() { d1, d2, d3 };//input

            Chromosome chromosomeSample = GenerateChromosomeSample(depot,customer,capacity);
         

            List<Chromosome> population = GeneratePopulation(populaitionSize, chromosomeSample);

            // ta inja debag shode

            CalculationFitness(population);

            for (int i = 0; i < n; i++)
            {
                population = GenerateNewPopulation(population);
                //jahesh
                CalculationFitness(population);
                for (int j = 0; j < population.Count; j++)
                {
                    Console.WriteLine(population[j].Fitness);
                }
            }

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
                population.Add(clone);
            }
            return population;
        }

        public static void CalculationFitness (List<Chromosome> populaitionList)
        {
            for (int k = 0; k < populaitionList.Count; k++)
            {
                double distance = 0;
                double route = 0;
                for (int i = 0; i < populaitionList.Count; i++)
                {
                    for (int j = 0; j < populaitionList[k][i].Count - 1; j++)
                    {
                        if (populaitionList[k][i][j].IsNull == true)
                        {
                            route = route + 1;
                        }
                        distance += Functions.EuclideanDistance(populaitionList[k][i][j], populaitionList[k][i][j + 1]);
                    }
                }
                populaitionList[k].Fitness = ((100 * route) + ((0.001f) * distance));
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
