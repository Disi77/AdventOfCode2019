using System;

namespace advent09
{
    class Program
    {
        static void Main(string[] args)
        {
            //Input            
            string input = "3,8,1005,8,329,1106,0,11,0,0,0,104,1,104,0,3,8,102,-1,8,10,1001,10,1,10,4,10,1008,8,0,10,4,10,1002,8,1,29,2,1102,1,10,1,1009,16,10,2,4,4,10,1,9,5,10,3,8,1002,8,-1,10,101,1,10,10,4,10,108,0,8,10,4,10,101,0,8,66,2,106,7,10,1006,0,49,3,8,1002,8,-1,10,101,1,10,10,4,10,108,1,8,10,4,10,1002,8,1,95,1006,0,93,3,8,102,-1,8,10,1001,10,1,10,4,10,108,1,8,10,4,10,102,1,8,120,1006,0,61,2,1108,19,10,2,1003,2,10,1006,0,99,3,8,1002,8,-1,10,1001,10,1,10,4,10,1008,8,0,10,4,10,101,0,8,157,3,8,102,-1,8,10,1001,10,1,10,4,10,1008,8,1,10,4,10,1001,8,0,179,2,1108,11,10,1,1102,19,10,3,8,102,-1,8,10,1001,10,1,10,4,10,1008,8,1,10,4,10,101,0,8,209,2,108,20,10,3,8,1002,8,-1,10,101,1,10,10,4,10,108,1,8,10,4,10,101,0,8,234,3,8,102,-1,8,10,101,1,10,10,4,10,108,0,8,10,4,10,1002,8,1,256,2,1102,1,10,1006,0,69,2,108,6,10,2,4,13,10,3,8,102,-1,8,10,101,1,10,10,4,10,1008,8,0,10,4,10,1002,8,1,294,1,1107,9,10,1006,0,87,2,1006,8,10,2,1001,16,10,101,1,9,9,1007,9,997,10,1005,10,15,99,109,651,104,0,104,1,21101,387395195796,0,1,21101,346,0,0,1105,1,450,21101,0,48210129704,1,21101,0,357,0,1105,1,450,3,10,104,0,104,1,3,10,104,0,104,0,3,10,104,0,104,1,3,10,104,0,104,1,3,10,104,0,104,0,3,10,104,0,104,1,21101,0,46413147328,1,21102,404,1,0,1106,0,450,21102,179355823323,1,1,21101,415,0,0,1105,1,450,3,10,104,0,104,0,3,10,104,0,104,0,21102,1,838345843476,1,21101,0,438,0,1105,1,450,21101,709475709716,0,1,21101,449,0,0,1105,1,450,99,109,2,22102,1,-1,1,21102,40,1,2,21101,0,481,3,21101,0,471,0,1105,1,514,109,-2,2105,1,0,0,1,0,0,1,109,2,3,10,204,-1,1001,476,477,492,4,0,1001,476,1,476,108,4,476,10,1006,10,508,1101,0,0,476,109,-2,2106,0,0,0,109,4,2101,0,-1,513,1207,-3,0,10,1006,10,531,21101,0,0,-3,21201,-3,0,1,21201,-2,0,2,21101,1,0,3,21101,550,0,0,1105,1,555,109,-4,2106,0,0,109,5,1207,-3,1,10,1006,10,578,2207,-4,-2,10,1006,10,578,21201,-4,0,-4,1105,1,646,22101,0,-4,1,21201,-3,-1,2,21202,-2,2,3,21101,597,0,0,1105,1,555,22102,1,1,-4,21101,0,1,-1,2207,-4,-2,10,1006,10,616,21101,0,0,-1,22202,-2,-1,-2,2107,0,-3,10,1006,10,638,22102,1,-1,1,21101,638,0,0,106,0,513,21202,-2,-1,-2,22201,-4,-2,-4,109,-5,2106,0,0";
            
            //Input from string to array
            string[] string_array = input.Split(",");
            int input_length = CountOfChar(input, ',') + 1;
            Int64[] int_array = new Int64[input_length+1000];
            for (int i = 0; i < int_array.Length-1000; i++)
            {
                int_array[i] = Int64.Parse(string_array[i]);
            }

            /* Run Program
             * Each move of robot = 1 input and 2 output
             * Robot - input 0, if robot over BLACK panel
             *         input 1, if robot over WHITE panel
             * Program - 1. output 0, if robot paints panel BLACK
             *                     1, if robot paints panel WHITE
             *           2. output 0, turn LEFT 90 degrees
             *                     1, turn RIGHT 90 degrees
             *  Robot starts facing UP. After turn left/right, robot move FORWARD one step
             */
            Int64 output = 0;
            Int64 pointer = 0;
            Int64 relative_base = 0;
            Int64[,] path = new Int64[40,60];
            int robot_x = 20;
            int robot_y = 10;
            
            string direction = "^";

            for (int i = 0; i < path.GetLength(0); i++)
            {
                for (int j = 0; j < path.GetLength(1); j++)
                {
                    path[i, j] = 2; // Set black
                }
            }
            path[robot_x, robot_y] = 1;
            while (output != 99)
            {
                int robot_input = GetRobotInput(robot_x, robot_y, path);
                (output, pointer, relative_base) = ProgramIntcode(int_array, robot_input, pointer, relative_base);
                PanelPaint(robot_x, robot_y, output, path);

                (output, pointer, relative_base) = ProgramIntcode(int_array, robot_input, pointer, relative_base);
                (robot_x, robot_y, direction) = RobotMove(robot_x, robot_y, output, direction);
            }

            Console.WriteLine();
            PrintRegistration(path);
   
            Console.WriteLine("End of program");
            Console.ReadKey();
        }

        static void PrintRegistration(Int64[,] path)
        {
            for (int i = 20; i < path.GetLength(0)-10; i++)
            {
                for (int j = 10; j < path.GetLength(1); j++)
                {
                    if (path[i, j] == 1)
                        Console.Write("{0}", path[i, j]);
                    else
                        Console.Write(" ");
                }
                Console.WriteLine();
            }      
        }

        static void PrintResult(Int64[,] path)
        {
            int result = 0;

            for (int i = 30; i < path.GetLength(0)-30; i++)
            {
                for (int j = 30; j < path.GetLength(1)-30; j++)
                {
                    if (path[i, j] == 1)
                        result++;
                    else if (path[i, j] == 0)
                        result++;
                }
            }
            Console.WriteLine("Count of 1s and 0s: {0}", result);
        }

        static (int, int, string) RobotMove(int robot_x, int robot_y, Int64 output, string direction)
        {
            //< ^ > v
            switch (direction)
            {
                case "^":
                    if (output == 0)
                        direction = "<";
                    else if (output == 1)
                        direction = ">";
                    break;

                case "<":
                    if (output == 0)
                        direction = "v";
                    else if (output == 1)
                        direction = "^";
                    break;

                case "v":
                    if (output == 0)
                        direction = ">";
                    else if (output == 1)
                        direction = "<";
                    break;

                case ">":
                    if (output == 0)
                        direction = "^";
                    else if (output == 1)
                        direction = "v"; 
                    break;
            }

            switch (direction)
            {
                case "^":
                    robot_x += -1;
                    break;

                case "<":
                    robot_y += -1;
                    break;

                case "v":
                    robot_x += 1;
                    break;

                case ">":
                    robot_y += 1;
                    break;
            }
            return (robot_x, robot_y, direction);        
        }

        static int GetRobotInput(int robot_x, int robot_y, Int64[,] path)
        {
            if (path[robot_x, robot_y] == 2 || path[robot_x, robot_y] == 0)
                return 0;
            if (path[robot_x, robot_y] == 1)
                return 1;
            else
                return 666;
        }

        static void PanelPaint(int robot_x, int robot_y, Int64 output, Int64[,] path)
        {
            if (output == 0)
                path[robot_x, robot_y] = 0;
            else if (output == 1)
                path[robot_x, robot_y] = 1;
        }

        static (Int64, Int64, Int64) ProgramIntcode(Int64[] program_array, int input_signal, Int64 pointer, Int64 relative_base)
        {
            Int64 output = input_signal;
            while (true)
            {
                Int64 instruction = program_array[pointer];
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
                {
                    return (99, pointer, relative_base);
                }

                //opcode 1
                if (instruction == 1)
                {
                    Int64 index1 = program_array[pointer + 1];
                    if (mode_par1 == 2)
                        index1 += relative_base;
                    Int64 index2 = program_array[pointer + 2];
                    if (mode_par2 == 2)
                        index2 += relative_base;
                    Int64 index3 = program_array[pointer + 3];
                    if (mode_par3 == 2)
                        index3 += relative_base;

                    Int64 value1;
                    if (mode_par1 == 1)
                        value1 = index1;
                    else
                        value1 = program_array[index1];

                    Int64 value2;
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
                    Int64 index1 = program_array[pointer + 1];
                    if (mode_par1 == 2)
                        index1 += relative_base;
                    Int64 index2 = program_array[pointer + 2];
                    if (mode_par2 == 2)
                        index2 += relative_base;
                    Int64 index3 = program_array[pointer + 3];
                    if (mode_par3 == 2)
                        index3 += relative_base;

                    Int64 value1;
                    if (mode_par1 == 1)
                        value1 = index1;
                    else
                        value1 = program_array[index1];

                    Int64 value2;
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
                    Int64 index1 = program_array[pointer + 1];
                    if (mode_par1 == 2)
                        index1 += relative_base;

                    int value1 = input_signal;
                    program_array[index1] = value1;
                    pointer += 2;
                }

                //opcode 4
                else if (instruction == 4)
                {
                    Int64 index1 = program_array[pointer + 1];
                    if (mode_par1 == 2)
                        index1 += relative_base;

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
                    Int64 index1 = program_array[pointer + 1];
                    if (mode_par1 == 2)
                        index1 += relative_base;
                    Int64 index2 = program_array[pointer + 2];
                    if (mode_par2 == 2)
                        index2 += relative_base;

                    Int64 value1;
                    if (mode_par1 == 1)
                        value1 = index1;
                    else
                        value1 = program_array[index1];

                    Int64 value2;
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
                    Int64 index1 = program_array[pointer + 1];
                    if (mode_par1 == 2)
                        index1 += relative_base;
                    Int64 index2 = program_array[pointer + 2];
                    
                    if (mode_par2 == 2)
                        index2 += relative_base;

                    Int64 value1;
                    if (mode_par1 == 1)
                        value1 = index1;
                    else
                        value1 = program_array[index1];

                    Int64 value2;
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
                    Int64 index1 = program_array[pointer + 1];
                    if (mode_par1 == 2)
                        index1 += relative_base;
                    Int64 index2 = program_array[pointer + 2];
                    if (mode_par2 == 2)
                        index2 += relative_base;
                    Int64 index3 = program_array[pointer + 3];
                    if (mode_par3 == 2)
                        index3 += relative_base;

                    Int64 value1;
                    if (mode_par1 == 1)
                        value1 = index1;
                    else
                        value1 = program_array[index1];

                    Int64 value2;
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
                    Int64 index1 = program_array[pointer + 1];
                    if (mode_par1 == 2)
                        index1 += relative_base;
                    Int64 index2 = program_array[pointer + 2];
                    if (mode_par2 == 2)
                        index2 += relative_base;
                    Int64 index3 = program_array[pointer + 3];
                    if (mode_par3 == 2)
                        index3 += relative_base;

                    Int64 value1;
                    if (mode_par1 == 1)
                        value1 = index1;
                    else
                        value1 = program_array[index1];

                    Int64 value2;
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
                //opcode 9

                else if (instruction == 9)
                {
                    Int64 index1 = program_array[pointer + 1];
                    if (mode_par1 == 2)
                        index1 += relative_base;

                    if (mode_par1 == 1)
                        relative_base += index1;
                    else
                        relative_base += program_array[index1];
                    pointer += 2;
                }
                else 
                {
                    Console.WriteLine("ERROR, wrong instruction");
                    Console.WriteLine("Instruction {0}", instruction);
                    Console.WriteLine("Pointer {0}", pointer);
                    break;
                }

            }
            return (output, pointer, relative_base);
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
