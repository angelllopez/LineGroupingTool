using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

namespace LineGroupingTool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Start the program
            Console.CursorVisible = false;
            Console.WriteLine("Line Grouping Tool");
            Console.WriteLine("============================================");

            Console.WriteLine("Press any key to start...");

            // Move cursor at the end of the previous line.
            Console.SetCursorPosition(0, Console.CursorTop - 1);

            // Wait for user input.
            Console.ReadKey();

            // Write blank spaces to clear the line.
            Console.Write(new string(' ', Console.WindowWidth));

            // Move cursor at the beginning of the previous line.
            Console.SetCursorPosition(0, Console.CursorTop - 0);

            // Using Path.Combine to build paths for better cross-platform support.
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string inputFilePath = Path.Combine(baseDirectory, "input.txt");
            string outputFilePath = Path.Combine(baseDirectory, "output.txt");

            // Verify that the input file exist.
            if (!File.Exists(inputFilePath))
            {
                HandleException(new Exception("Input file not found."));
                return;
            }

            // Read lines from the input file
            List<string> lines = File.ReadAllLines(inputFilePath).ToList();

            // Verify that the input file is not empty.
            if (lines.Count == 0)
            {
                HandleException(new Exception("Input file is empty."));
                return;
            }

            // Group similar lines
            var groupedLines = GroupSimilarLines(lines);

            // Write each group to a separate output file.
            int groupNumber = 1;

            foreach (var group in groupedLines)
            {
                string groupFileName = $"output-{groupNumber}.txt";
                string groupFilePath = Path.Combine(baseDirectory, groupFileName);

                // Write grouped lines to the output file
                File.WriteAllLines(groupFilePath, group);

                Console.WriteLine($"Group {groupNumber} written to output file.");
                groupNumber++;
            }
        }

        private static List<List<string>> GroupSimilarLines(List<string> lines)
        {
            // Use a dictionary to group similar lines
            Dictionary<string, List<string>> lineGroups = new Dictionary<string, List<string>>();

            foreach (string line in lines)
            {
                bool groupFound = false;

                // Check if the line matches any of the existing groups.
                foreach (var group in lineGroups.Keys.ToList())
                {
                    if (AreSimilarLines(line, group))
                    {
                        // Add the line to the existing group
                        lineGroups[group].Add(line);
                        groupFound = true;
                        break;
                    }
                }

                // If the line does not match any of the existing groups, create a new group for the line.
                if (!groupFound)
                {
                    lineGroups[line] = new List<string> { line };
                }
                
                // Return the grouped lines as a list of lists.
                return lineGroups.Values.ToList();
            }

            // Return the grouped lines as a list of lists.
            return lineGroups.Values.ToList();
        }

        static bool AreSimilarLines(string line1, string line2)
        {
            // Check if the lines are equal.
            if (line1 != line2)
            {
                return false;
            }

            // Check if the lines have the same length.
            if (line1.Length != line2.Length)
            {
                return false;
            }

            // Check if the lines have the same number of words.
            if (line1.Split(' ').Length != line2.Split(' ').Length)
            {
                return false;
            }

            // Check if the lines have the same number of characters.
            if (line1.Count() != line2.Count())
            {
                return false;
            }

            // Check if the lines have the same number of digits.
            if (line1.Count(char.IsDigit) != line2.Count(char.IsDigit))
            {
                return false;
            }

            // Check if the lines have the same number of letters.
            if (line1.Count(char.IsLetter) != line2.Count(char.IsLetter))
            {
                return false;
            }

            // Check if the lines have the same number of uppercase letters.
            if (line1.Count(char.IsUpper) != line2.Count(char.IsUpper))
            {
                return false;
            }

            // Check if the lines have the same number of lowercase letters.
            if (line1.Count(char.IsLower) != line2.Count(char.IsLower))
            {
                return false;
            }

            // Check if the lines have the same number of punctuation characters.
            if (line1.Count(char.IsPunctuation) != line2.Count(char.IsPunctuation))
            {
                return false;
            }

            // Check if the lines have the same number of whitespace characters.
            if (line1.Count(char.IsWhiteSpace) != line2.Count(char.IsWhiteSpace))
            {
                return false;
            }

            // Check if the lines have the same number of symbols.
            if (line1.Count(char.IsSymbol) != line2.Count(char.IsSymbol))
            {
                return false;
            }

            // Check if the lines have the same number of separator characters.
            if (line1.Count(char.IsSeparator) != line2.Count(char.IsSeparator))
            {
                return false;
            }

            // Check if the lines have the same number of control characters.
            if (line1.Count(char.IsControl) != line2.Count(char.IsControl))
            {
                return false;
            }

            // Check if the lines have the same number of surrogate characters.
            if (line1.Count(char.IsSurrogate) != line2.Count(char.IsSurrogate))
            {
                return false;
            }

            return true;
        }
 

        // This method is executed when an exception is thrown.
        private static void HandleException(Exception ex)
        {
            // Set red foreground color.
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("An error occurred.");
            Console.WriteLine(ex.Message);

            // Reset foreground color.
            Console.ResetColor();
            
            // Skip a line.
            Console.WriteLine();

            // Wait for user input.
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

    }
}
