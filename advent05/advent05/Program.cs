﻿using System;

namespace advent02
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "3,225,1,225,6,6,1100,1,238,225,104,0,1002,114,46,224,1001,224,-736,224,4,224,1002,223,8,223,1001,224,3,224,1,223,224,223,1,166,195,224,1001,224,-137,224,4,224,102,8,223,223,101,5,224,224,1,223,224,223,1001,169,83,224,1001,224,-90,224,4,224,102,8,223,223,1001,224,2,224,1,224,223,223,101,44,117,224,101,-131,224,224,4,224,1002,223,8,223,101,5,224,224,1,224,223,223,1101,80,17,225,1101,56,51,225,1101,78,89,225,1102,48,16,225,1101,87,78,225,1102,34,33,224,101,-1122,224,224,4,224,1002,223,8,223,101,7,224,224,1,223,224,223,1101,66,53,224,101,-119,224,224,4,224,102,8,223,223,1001,224,5,224,1,223,224,223,1102,51,49,225,1101,7,15,225,2,110,106,224,1001,224,-4539,224,4,224,102,8,223,223,101,3,224,224,1,223,224,223,1102,88,78,225,102,78,101,224,101,-6240,224,224,4,224,1002,223,8,223,101,5,224,224,1,224,223,223,4,223,99,0,0,0,677,0,0,0,0,0,0,0,0,0,0,0,1105,0,99999,1105,227,247,1105,1,99999,1005,227,99999,1005,0,256,1105,1,99999,1106,227,99999,1106,0,265,1105,1,99999,1006,0,99999,1006,227,274,1105,1,99999,1105,1,280,1105,1,99999,1,225,225,225,1101,294,0,0,105,1,0,1105,1,99999,1106,0,300,1105,1,99999,1,225,225,225,1101,314,0,0,106,0,0,1105,1,99999,1107,226,677,224,102,2,223,223,1006,224,329,101,1,223,223,1108,226,677,224,1002,223,2,223,1005,224,344,101,1,223,223,8,226,677,224,102,2,223,223,1006,224,359,1001,223,1,223,1007,226,677,224,1002,223,2,223,1005,224,374,101,1,223,223,1008,677,677,224,1002,223,2,223,1005,224,389,1001,223,1,223,1108,677,226,224,1002,223,2,223,1006,224,404,1001,223,1,223,1007,226,226,224,1002,223,2,223,1005,224,419,1001,223,1,223,1107,677,226,224,1002,223,2,223,1006,224,434,101,1,223,223,108,677,677,224,1002,223,2,223,1005,224,449,1001,223,1,223,1107,677,677,224,102,2,223,223,1005,224,464,1001,223,1,223,108,226,226,224,1002,223,2,223,1006,224,479,1001,223,1,223,1008,226,226,224,102,2,223,223,1005,224,494,101,1,223,223,108,677,226,224,102,2,223,223,1005,224,509,1001,223,1,223,8,677,226,224,1002,223,2,223,1006,224,524,101,1,223,223,7,226,677,224,1002,223,2,223,1006,224,539,101,1,223,223,7,677,226,224,102,2,223,223,1006,224,554,1001,223,1,223,7,226,226,224,1002,223,2,223,1006,224,569,101,1,223,223,107,677,677,224,102,2,223,223,1006,224,584,101,1,223,223,1108,677,677,224,102,2,223,223,1006,224,599,1001,223,1,223,1008,677,226,224,1002,223,2,223,1005,224,614,1001,223,1,223,8,677,677,224,1002,223,2,223,1006,224,629,1001,223,1,223,107,226,677,224,1002,223,2,223,1006,224,644,101,1,223,223,1007,677,677,224,102,2,223,223,1006,224,659,101,1,223,223,107,226,226,224,1002,223,2,223,1006,224,674,1001,223,1,223,4,223,99,226";

            ProgramIntcode(input);
            Console.ReadKey();
        }

        static int[] ProgramIntcode(string input)
        {
            string[] string_array = input.Split(",");
            int input_length = CountOfChar(input, ',') + 1;
            int[] main_array = new int[input_length];
            for (int i = 0; i < main_array.Length; i++)
                main_array[i] = int.Parse(string_array[i]);

            int[] array = new int[input_length];
            main_array.CopyTo(array, 0);

            int pointer = 0;
            int last_i = array.Length - 1;

            while (pointer < last_i)
            {
                int instruction = array[pointer];
                int mode_par1 = 0;
                int mode_par2 = 0;
                int mode_par3 = 0;

                if (instruction > 99)
                {
                    string instruction_string = instruction.ToString();
                    int missing_position = 5 - instruction_string.Length;
                    if (missing_position > 0)
                    {
                        for (int i = 0; i < missing_position; i++)
                            instruction_string = "0" + instruction_string;
                    }
                    instruction = int.Parse(instruction_string.Substring(3, 2));
                    mode_par1 = int.Parse(instruction_string.Substring(2, 1));
                    mode_par2 = int.Parse(instruction_string.Substring(1, 1));
                    mode_par3 = int.Parse(instruction_string.Substring(0, 1));
                }

                //opcode 99 = program is finished
                if (instruction == 99)
                    break;

                //opcode 1
                if (instruction == 1)
                {
                    int index1 = array[pointer + 1];
                    int index2 = array[pointer + 2];
                    int value1;
                    if (mode_par1 == 1)
                        value1 = index1;
                    else
                        value1 = array[index1];

                    int value2;
                    if (mode_par2 == 1)
                        value2 = index2;
                    else
                        value2 = array[index2];

                    int result = value1 + value2;
                    array[array[pointer + 3]] = result;
                    pointer += 4;
                }

                //opcode 2
                else if (instruction == 2)
                {
                    int index1 = array[pointer + 1];
                    int index2 = array[pointer + 2];
                    int value1;
                    if (mode_par1 == 1)
                        value1 = index1;
                    else
                        value1 = array[index1];

                    int value2;
                    if (mode_par2 == 1)
                        value2 = index2;
                    else
                        value2 = array[index2];

                    int result = value1 * value2;
                    array[array[pointer + 3]] = result;
                    pointer += 4;
                }

                //opcode 3
                else if (instruction == 3)
                {
                    int index1 = array[pointer + 1];
                    Console.WriteLine("Input:");
                    int value1 = int.Parse(Console.ReadLine());
                    if (mode_par1 == 1)
                        value1 = index1;
                    array[index1] = value1;
                    pointer += 2;
                }

                //opcode 4
                else if (instruction == 4)
                {
                    int index1 = array[pointer + 1];
                    int value1;
                    if (mode_par1 == 1)
                        value1 = index1;
                    else
                        value1 = array[index1];
                    int result = value1;
                    Console.Write("Output:"); 
                    Console.WriteLine(result);
                    pointer += 2;
                }

                //opcode 5
                else if (instruction == 5)
                {
                    int index1 = array[pointer + 1];
                    int index2 = array[pointer + 2];
                    int value1;
                    if (mode_par1 == 1)
                        value1 = index1;
                    else
                        value1 = array[index1];

                    int value2;
                    if (mode_par2 == 1)
                        value2 = index2;
                    else
                        value2 = array[index2];
                    
                    if (value1 != 0)
                        pointer = value2;
                    else
                        pointer += 3;
                }

                //opcode 6
                else if (instruction == 6)
                {
                    int index1 = array[pointer + 1];
                    int index2 = array[pointer + 2];
                    int value1;
                    if (mode_par1 == 1)
                        value1 = index1;
                    else
                        value1 = array[index1];

                    int value2;
                    if (mode_par2 == 1)
                        value2 = index2;
                    else 
                        value2 = array[index2];
                    
                    if (value1 == 0)
                        pointer = value2;
                    else
                        pointer += 3;
                }

                //opcode 7
                else if (instruction == 7)
                {
                    int index1 = array[pointer + 1];
                    int index2 = array[pointer + 2];
                    int index3 = array[pointer + 3];
                    int value1 = array[index1];
                    if (mode_par1 == 1)
                        value1 = index1;
                    int value2 = array[index2];
                    if (mode_par2 == 1)
                        value2 = index2;

                    if (value1 < value2)
                        array[index3] = 1;
                    else
                        array[index3] = 0;
                    pointer += 4;
                }

                //opcode 8
                else if (instruction == 8)
                {
                    int index1 = array[pointer + 1];
                    int index2 = array[pointer + 2];
                    int index3 = array[pointer + 3];
                    int value1 = array[index1];
                    if (mode_par1 == 1)
                        value1 = index1;
                    int value2 = array[index2];
                    if (mode_par2 == 1)
                        value2 = index2;

                    if (value1 == value2)
                        array[index3] = 1;
                    else
                        array[index3] = 0;
                    pointer += 4;
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
