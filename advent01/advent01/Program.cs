using System;

namespace advent01
{
    class Program
    {
        static void Main(string[] args)
        {
            string numbers = "";
            string line;
            System.IO.StreamReader text = new System.IO.StreamReader(@"C:\Users\cihalpet\source\repos\advent01\data.txt");

            while ((line = text.ReadLine()) != null)
            {
                numbers += line;
                numbers += " ";
            }

            numbers.Trim();
            string[] array = numbers.Split(' ');
            int[] array_nums = new int[array.Length];

            for (int i = 0; i < array.Length-1; i++)
            {
                //Console.WriteLine("-{0}-", array[i]);
                int temp = int.Parse((array[i]));
                array_nums[i] = temp;
            }

            double total_fuel = 0;
            for (int i = 0; i < array_nums.Length; i++)
            {
                if (array_nums[i] > 0)
                {
                    double fuel_for_module = (Math.Floor((double)array_nums[i] / 3) - 2);
                    total_fuel += FuelForFuel(fuel_for_module);
                }
            }

            Console.WriteLine(total_fuel);





            Console.ReadKey();
        }

        static double FuelForFuel(double fuel)
        {
            double fuel_total = fuel;
            while (true)
            {
                fuel = (Math.Floor(fuel / 3) - 2);
                if (fuel <= 0)
                    return fuel_total;
                else
                    fuel_total += fuel;
            }
        }
    }
}
