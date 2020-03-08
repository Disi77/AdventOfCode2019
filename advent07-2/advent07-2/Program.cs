using System;
using System.Linq;

namespace advent07_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Inputs only for puzzle 1
            string program_input_d1 = "3,8,1001,8,10,8,105,1,0,0,21,46,55,68,89,110,191,272,353,434,99999,3,9,1002,9,3,9,1001,9,3,9,102,4,9,9,101,4,9,9,1002,9,5,9,4,9,99,3,9,102,3,9,9,4,9,99,3,9,1001,9,5,9,102,4,9,9,4,9,99,3,9,1001,9,5,9,1002,9,2,9,1001,9,5,9,1002,9,3,9,4,9,99,3,9,101,3,9,9,102,3,9,9,101,3,9,9,1002,9,4,9,4,9,99,3,9,1001,9,1,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,99,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,99,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,99,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,99";
            //string program_input_d1 = "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0";

            //Input puzzle 1 from string to array
            string[] string_array_d1 = program_input_d1.Split(",");
            int input_length_d1 = CountOfChar(program_input_d1, ',') + 1;
            int[] main_program_array_d1 = new int[input_length_d1];
            for (int i = 0; i < main_program_array_d1.Length; i++)
                main_program_array_d1[i] = int.Parse(string_array_d1[i]);

            //Phase setting sequence 1 - all possible from digits 0, 1, 2, 3, 4 (evry digit only once in sequence)
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

            //puzzle 1
            Console.WriteLine("Day 7 - Puzzle 1");
            int[] results = new int[phase_seq.Length];

            for (int j = 0; j < phase_seq.Length; j++)
            {
                Amp[] amplifiers = new Amp[5];
                for (int amp = 0; amp < 5; amp++)
                {
                    amplifiers[amp] = new Amp();
                    amplifiers[amp].program_array = new int[main_program_array_d1.Length];
                    main_program_array_d1.CopyTo(amplifiers[amp].program_array, 0);
                }
                results[j] = AmpCycleDay1(amplifiers, phase_seq[j], 0);
            }
            int results_max = results.Max();
            int index_max_phase = Array.IndexOf(results, results_max);
            Console.WriteLine("Phase: {0} thruster signal {1}", phase_seq[index_max_phase], results_max);
            //------------------------------------------------------------------------------------------------------

            //Inputs only for puzzle 2
            string program_input_d2 = "3,8,1001,8,10,8,105,1,0,0,21,46,55,68,89,110,191,272,353,434,99999,3,9,1002,9,3,9,1001,9,3,9,102,4,9,9,101,4,9,9,1002,9,5,9,4,9,99,3,9,102,3,9,9,4,9,99,3,9,1001,9,5,9,102,4,9,9,4,9,99,3,9,1001,9,5,9,1002,9,2,9,1001,9,5,9,1002,9,3,9,4,9,99,3,9,101,3,9,9,102,3,9,9,101,3,9,9,1002,9,4,9,4,9,99,3,9,1001,9,1,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,99,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,99,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,99,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,99";
            //string program_input_d2 = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";
            //string program_input_d2 = "3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10";

            //Input puzzle 2 from string to array
            string[] string_array_d2 = program_input_d2.Split(",");
            int input_length_d2 = CountOfChar(program_input_d2, ',') + 1;
            int[] main_program_array_d2 = new int[input_length_d2];
            for (int i = 0; i < main_program_array_d2.Length; i++)
                main_program_array_d2[i] = int.Parse(string_array_d2[i]);                                    

            //Phase setting sequence 2 - all possible from digits 5, 6, 7, 8, 9 (evry digit only once in sequence)
            string temp2 = "";
            for (int i2 = 56789; i2 <= 98765; i2++)
            {
                string temp_i2 = i2.ToString();
                if (temp_i2.Contains("5") && temp_i2.Contains("6") && temp_i2.Contains("7") && temp_i2.Contains("8") && temp_i2.Contains("9"))
                    temp2 += temp_i2 + " ";
            }
            string[] phase_seq2 = temp2.Trim().Split(" ");

            //puzzle 2
            Console.WriteLine("Day 7 - Puzzle 2");
            int[] results2 = new int[phase_seq2.Length];

            for (int j = 0; j < phase_seq2.Length; j++)
            {
                Amp[] amplifiers2 = new Amp[5];
                for (int amp2 = 0; amp2 < 5; amp2++)
                {
                    amplifiers2[amp2] = new Amp();
                    amplifiers2[amp2].program_array = new int[main_program_array_d2.Length];
                    main_program_array_d2.CopyTo(amplifiers2[amp2].program_array, 0);
                }
                results2[j] = AmpCycleDay2(amplifiers2, phase_seq2[j]);
            }
            int results_max2 = results2.Max();
            int index_max_phase2 = Array.IndexOf(results2, results_max2);
            Console.WriteLine("Phase: {0} thruster signal {1}", phase_seq2[index_max_phase2], results_max2);

            Console.WriteLine("Konec programu");
            Console.ReadKey();
        }

        static int AmpCycleDay2(Amp[] amplifiers, string phase)
        {
            int[] is99 = new int[5];
            int kolo = 0;
            while (true)
            {                
                for (int k = 0; k < 5; k++)
                {
                    amplifiers[k].phase = int.Parse(phase.Substring(k, 1));
                    if (k == 0 && kolo == 0)
                        amplifiers[0].input_signal = 0;
                    else if (k == 0 && kolo != 0)
                        amplifiers[0].input_signal = amplifiers[4].output_signal;
                    else
                        amplifiers[k].input_signal = amplifiers[k - 1].output_signal;                                 

                    if (kolo == 0)
                    {
                        (amplifiers[k].output_signal, amplifiers[k].program_array, amplifiers[k].pointer) =
                         ProgramIntcode(amplifiers[k].program_array, 
                                        amplifiers[k].phase, 
                                        amplifiers[k].input_signal, 
                                        amplifiers[k].pointer);
                    }
                    else
                    {
                        (amplifiers[k].output_signal, amplifiers[k].program_array, amplifiers[k].pointer) = 
                         ProgramIntcode(amplifiers[k].program_array, 
                                        amplifiers[k].input_signal, 
                                        amplifiers[k].input_signal, 
                                        amplifiers[k].pointer);
                    }                    
                    if (amplifiers[k].program_array[amplifiers[k].pointer] == 99)
                        is99[k] = 1;                   
                }
                kolo ++;
                if (is99.Sum() == 5)
                    return amplifiers[4].output_signal;
            }      
        }

        static int AmpCycleDay1(Amp[] amplifiers, string phase, int input)
        {
            for (int k = 0; k < 5; k++)
            {
                amplifiers[k].phase = int.Parse(phase.Substring(k, 1));
                if (k == 0)
                    amplifiers[k].input_signal = input;
                else
                    amplifiers[k].input_signal = amplifiers[k - 1].output_signal;

                (amplifiers[k].output_signal, amplifiers[k].program_array, amplifiers[k].pointer) = 
                 ProgramIntcode(amplifiers[k].program_array,
                                amplifiers[k].phase,
                                amplifiers[k].input_signal,
                                0);
            }          
            return amplifiers[4].output_signal;
        }

        static void PrintProgramArray(int[] array)
        {
            for (int j = 0; j < array.Length; j++)
            {

                Console.Write(j);
                if (array[j] < 0)
                    Console.Write("  ");
                else if (array[j] < 10)
                    Console.Write(" ");
                else if (array[j] < 100)
                    Console.Write("  ");
                else if (array[j] < 1000)
                    Console.Write("   ");
                else if (array[j] < 10000)
                    Console.Write("    ");
                if (j < 10)
                    Console.Write(" ");
            }
            Console.WriteLine();
                
            foreach (int i in array)
                Console.Write("{0}  ",i);
            Console.WriteLine();
        }
        
        static (int, int[], int) ProgramIntcode(int[] program_array, int phase, int input_signal, int pointer)
        {
            bool second_input = false;
            int output = input_signal;

            while (true)
            {
                int instruction = program_array[pointer];
                int mode_par1 = 0;
                int mode_par2 = 0;

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
                }

                //opcode 99 = program is finished
                if (instruction == 99)
                    break;

                //opcode 1
                if (instruction == 1)
                {
                    int index1 = program_array[pointer + 1];
                    int index2 = program_array[pointer + 2];
                    int index3 = program_array[pointer + 3];

                    int value1;
                    if (mode_par1 == 1)
                        value1 = index1;
                    else
                        value1 = program_array[index1];

                    int value2;
                    if (mode_par2 == 1)
                        value2 = index2;
                    else
                        value2 = program_array[index2];

                    program_array[index3] = value1 + value2;
                    pointer += 4;
                }

                //opcode 2
                else if (instruction == 2)
                {
                    int index1 = program_array[pointer + 1];
                    int index2 = program_array[pointer + 2];
                    int index3 = program_array[pointer + 3];

                    int value1;
                    if (mode_par1 == 1)
                        value1 = index1;
                    else
                        value1 = program_array[index1];

                    int value2;
                    if (mode_par2 == 1)
                        value2 = index2;
                    else
                        value2 = program_array[index2];

                    program_array[index3] = value1 * value2;
                    pointer += 4;
                }

                //opcode 3
                else if (instruction == 3)
                {
                    int index1 = program_array[pointer + 1];
                    int value1;
                    if (!second_input)
                    {
                        value1 = phase;
                        second_input = true;
                    }
                        
                    else
                        value1 = input_signal;
                    //if (mode_par1 == 1)
                    //    value1 = index1;

                    program_array[index1] = value1;
                    pointer += 2;                    
                }

                //opcode 4
                else if (instruction == 4)
                {
                    int index1 = program_array[pointer + 1];
                    if (mode_par1 == 1)
                        output = index1;
                    else
                        output = program_array[index1];
                    pointer += 2;
                    break;
                }

                //opcode 5
                else if (instruction == 5)
                {
                    int index1 = program_array[pointer + 1];
                    int index2 = program_array[pointer + 2];

                    int value1;
                    if (mode_par1 == 1)
                        value1 = index1;
                    else
                        value1 = program_array[index1];

                    int value2;
                    if (mode_par2 == 1)
                        value2 = index2;
                    else
                        value2 = program_array[index2];

                    if (value1 != 0)
                        pointer = value2;
                    else
                        pointer += 3;
                }

                //opcode 6
                else if (instruction == 6)
                {
                    int index1 = program_array[pointer + 1];
                    int index2 = program_array[pointer + 2];

                    int value1;
                    if (mode_par1 == 1)
                        value1 = index1;
                    else
                        value1 = program_array[index1];

                    int value2;
                    if (mode_par2 == 1)
                        value2 = index2;
                    else
                        value2 = program_array[index2];

                    if (value1 == 0)
                        pointer = value2;
                    else
                        pointer += 3;
                }

                //opcode 7
                else if (instruction == 7)
                {
                    int index1 = program_array[pointer + 1];
                    int index2 = program_array[pointer + 2];
                    int index3 = program_array[pointer + 3];

                    int value1;
                    if (mode_par1 == 1)
                        value1 = index1;
                    else
                        value1 = program_array[index1];

                    int value2;
                    if (mode_par2 == 1)
                        value2 = index2;
                    else
                        value2 = program_array[index2];
                    
                    if (value1 < value2)
                        program_array[index3] = 1;
                    else
                        program_array[index3] = 0;
                    pointer += 4;
                }

                //opcode 8
                else if (instruction == 8)
                {
                    int index1 = program_array[pointer + 1];
                    int index2 = program_array[pointer + 2];
                    int index3 = program_array[pointer + 3];

                    int value1;
                    if (mode_par1 == 1)
                        value1 = index1;
                    else
                        value1 = program_array[index1];

                    int value2;
                    if (mode_par2 == 1)
                        value2 = index2;
                    else
                        value2 = program_array[index2];

                    if (value1 == value2)
                        program_array[index3] = 1;
                    else
                        program_array[index3] = 0;
                    pointer += 4;
                }
            }
            return (output, program_array, pointer);
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
