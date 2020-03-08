using System;
using System.Linq;

namespace advent02
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "3,8,1001,8,10,8,105,1,0,0,21,46,55,68,89,110,191,272,353,434,99999,3,9,1002,9,3,9,1001,9,3,9,102,4,9,9,101,4,9,9,1002,9,5,9,4,9,99,3,9,102,3,9,9,4,9,99,3,9,1001,9,5,9,102,4,9,9,4,9,99,3,9,1001,9,5,9,1002,9,2,9,1001,9,5,9,1002,9,3,9,4,9,99,3,9,101,3,9,9,102,3,9,9,101,3,9,9,1002,9,4,9,4,9,99,3,9,1001,9,1,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,99,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,99,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,99,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,99";
            //string input = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";
            //Input from string to array
            string[] string_array = input.Split(",");
            int input_length = CountOfChar(input, ',') + 1;
            int[] main_array = new int[input_length];
            for (int i = 0; i < main_array.Length; i++)
                main_array[i] = int.Parse(string_array[i]);

            int[] array = new int[input_length];
            main_array.CopyTo(array, 0);

            //Phase setting sequence - all possible from digits 0, 1, 2, 3, 4 (evry digit only once in sequence)
            string temp = "";
            for (int i = 1234; i <= 43210; i++)
            {
                string temp_i = i.ToString();
                if (temp_i.Length == 4)
                    temp_i = "0" + temp_i;
                if (temp_i.Contains("0") && temp_i.Contains("1") && temp_i.Contains("2") && temp_i.Contains("3") && temp_i.Contains("4"))
                    temp += temp_i + " ";                 
            }
            string[] phase_seq = temp.Trim().Split(" ");

            //Day 1
            Console.WriteLine("Day 7 - Puzzle 1");
            AmpCycle(array, phase_seq, 0);
            Console.ReadKey();
        }

        static int AmpCycle(int[] array, string[] phase_seq, int input)
        {
            int[] results = new int[phase_seq.Length];
            

            for (int j = 0; j < phase_seq.Length; j++)
            {
                int result_for_seq = 0;
                int input2 = input;
                for (int k = 0; k < 5; k++)
                {
                    
                    int input1 = int.Parse(phase_seq[j].Substring(k, 1));
                    int output = ProgramIntcode(array, input1, input2);
                    if (k == 4)
                        result_for_seq = output;
                    else
                        input2 = output;
                }
                results[j] = result_for_seq;
            }

            int a = results.Max();
            int b = Array.IndexOf(results, a);
            Console.WriteLine("Phase: {0} thruster signal {1}", phase_seq[b], a);
            return a;
        }
        static int ProgramIntcode(int[] array, int input1, int input2)
        {
            int pointer = 0;
            int last_i = array.Length - 1;
            bool sec_input = false;
            int output = 0;

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
                    int value1;
                    if (!sec_input)
                        value1 = input1;
                    else
                        value1 = input2;
                    if (mode_par1 == 1)
                        value1 = index1;
                    array[index1] = value1;
                    pointer += 2;
                    sec_input = true;
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
                    output = value1;
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
            return output;
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
