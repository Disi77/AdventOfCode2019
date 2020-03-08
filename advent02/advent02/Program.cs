using System;

namespace advent02
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,1,9,19,1,13,19,23,2,23,9,27,1,6,27,31,2,10,31,35,1,6,35,39,2,9,39,43,1,5,43,47,2,47,13,51,2,51,10,55,1,55,5,59,1,59,9,63,1,63,9,67,2,6,67,71,1,5,71,75,1,75,6,79,1,6,79,83,1,83,9,87,2,87,10,91,2,91,10,95,1,95,5,99,1,99,13,103,2,103,9,107,1,6,107,111,1,111,5,115,1,115,2,119,1,5,119,0,99,2,0,14,0";
            int input_noun = 12;
            int input_verb = 2;

            int[] result_array = ProgramIntcode(input, input_noun, input_verb);

            Console.WriteLine("Day2 - Puzzle1 = {0}",result_array[0]);

            int wanted_noun = 0;
            int wanted_verb = 0;

            for (int i = 0; i < 100; i++)
            {
                bool test_continue = true;
                for (int j = 0; j < 100; j++)
                {
                    test_continue = true;
                    int[] test_array = ProgramIntcode(input, i, j);
                    if (test_array[0] == 19690720)
                    {
                        wanted_noun = i;
                        wanted_verb = j;
                        test_continue = false;
                        break;
                    }                    
                }
                if (!test_continue)
                    break;
            }


            Console.WriteLine("Day2 - Puzzle2 = {0}", wanted_noun * 100 + wanted_verb);
            Console.ReadKey();
        }

        static int[] ProgramIntcode(string input, int input_noun, int input_verb)
        { 
            string[] string_array = input.Split(",");
            int input_length = CountOfChar(input, ',')+1;
            int[] main_array = new int[input_length];
            for (int i = 0; i < main_array.Length; i++)
                main_array[i] = int.Parse(string_array[i]);

            int noun = input_noun;
            int verb = input_verb;

            main_array[1] = noun;
            main_array[2] = verb;

            int[] array = new int[input_length];
            main_array.CopyTo(array, 0);

            for (int pointer = 0; pointer < array.Length; pointer += 4)
            {
                int last_i = array.Length - 1;

                //opcode 99 = program is finished
                if (array[pointer] == 99)
                    break;
                
                //wrong opcode
                if (array[pointer] != 1 && array[pointer] != 2)
                {
                    Console.WriteLine("Something wrong!!");
                    break;
                }

                int index1 = array[pointer + 1];
                int index2 = array[pointer + 2];

                //opcode 1
                if (array[pointer] == 1 && (pointer + 3) <= last_i)
                {
                    int result = array[index1] + array[index2];
                    array[array[pointer + 3]] = result;
                }

                //opcode 2
                else if (array[pointer] == 2 && (pointer + 3) <= last_i)
                {
                    int result = array[index1] * array[index2];
                    array[array[pointer + 3]] = result;
                }
            }
            return array;
        }
        static int CountOfChar(string input, char ch)
        {
            int count = 0;
            foreach (char item in input)
            {
                if (ch == item)
                    count++;              
            }
            return count;     
        }

    }
}
