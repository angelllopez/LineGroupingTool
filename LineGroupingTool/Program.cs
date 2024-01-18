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

            // Write grouped lines to the output file
            File.WriteAllLines(outputFilePath, groupedLines);

            Console.WriteLine("Lines grouped and written to output file.");
        }

        private static List<string> GroupSimilarLines(List<string> lines)
        {
            // Use a dictionary to group similar lines
            Dictionary<string, List<string>> lineGroups = new Dictionary<string, List<string>>();

            foreach (string line in lines)
            {
                // Use the line itself as the key for simplicity
                string key = line;

                if (lineGroups.ContainsKey(key))
                {
                    // Add the line to the existing group
                    lineGroups[key].Add(line);
                }
                else
                {
                    // Create a new group for the line
                    lineGroups[key] = new List<string> { line };
                }
            }

            // Flatten the grouped lines
            List<string> groupedLines = new List<string>();

            foreach (var group in lineGroups.Values)
            {
                // Concatenate lines in each group with a separator
                string groupedLine = string.Join(Environment.NewLine, group);
                groupedLines.Add(groupedLine);
            }

            return groupedLines;
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
