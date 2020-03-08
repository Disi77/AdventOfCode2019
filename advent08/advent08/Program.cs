using System;
using System.Linq;

namespace advent08
{
    class Program
    {
        static void Main(string[] args)
        {
            string temp;
            System.IO.StreamReader text = new System.IO.StreamReader(@"C:\Users\cihalpet\source\repos\advent08\data.txt");
            temp = text.ReadToEnd();

            int count_rows = 6;
            int count_columns = 25;
            int count_layers = temp.Length / (count_rows * count_columns);

            int[,,] data = new int[count_layers, count_rows, count_columns];

            //x = layer, y = row, z = column
            for (int i = 0; i < temp.Length; i++)
            {
                for (int x = 0; x < count_layers; x++)
                {
                    for (int y = 0; y < count_rows; y++)
                    {
                        for (int z = 0; z < count_columns; z++)
                        {
                            data[x, y, z] = int.Parse(temp.Substring(i, 1));
                            i++;
                        }
                    }
                }
            }

            //Searching of layer with fewest 0 digits
            int num = 0;
            int[] count_of_num_in_layer = new int[data.GetLength(0)];

            for (int i = 0; i < data.GetLength(0); i++)
            {
                count_of_num_in_layer[i] = CountOfNumInLayer(data, i, num);
            }
            int layer_fewest_num_value = count_of_num_in_layer.Min();
            int layer_fewest_num_index = Array.IndexOf(count_of_num_in_layer, layer_fewest_num_value);

            Console.WriteLine("Layer with fewest {0} digits is <{1}> and contains <{2}> {0} digits", num, layer_fewest_num_index, layer_fewest_num_value);

            int result_puzzle1 = CountOfNumInLayer(data, layer_fewest_num_index, 1) * CountOfNumInLayer(data, layer_fewest_num_index, 2);
            Console.WriteLine("Finální výsledek je {0}", result_puzzle1);

            //Decoding the picture
            //0 is black, 1 is white, and 2 is transparent
            int[,] picture;
            picture = DecodingDataToPicture(data);

            for (int x = 0; x < picture.GetLength(0); x++)
            {
                for (int y = 0; y < picture.GetLength(1); y++)
                {
                    if (picture[x, y] == 1)
                        Console.Write("☻");
                    else
                        Console.Write(" ");
                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }
        static int[,] DecodingDataToPicture(int[,,] data)
        {
            int[,] picture = new int[data.GetLength(1), data.GetLength(2)];
            for (int y = 0; y < data.GetLength(1); y++)
            {
                for (int z = 0; z < data.GetLength(2); z++)
                {
                    for (int x = 0; x < data.GetLength(0); x++)
                    {
                        if (data[x, y, z] != 2)
                        {
                            picture[y, z] = data[x, y, z];
                            break;
                        }    
                    }
                }
            }
            return picture;
        }

        static int CountOfNumInLayer(int[,,] data, int layer, int num)
        {
            int count_rows = data.GetLength(1);
            int count_columns = data.GetLength(2);            

            int result = 0;
            for (int y = 0; y < count_rows; y++)
            {
                for (int z = 0; z < count_columns; z++)
                {
                    if (data[layer, y, z] == num)
                        result++;
                }
            }
            return result;
        }

    }
}
