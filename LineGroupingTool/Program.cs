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
            // Specify the input and output file paths
            string inputFilePath = "input.txt";
            string outputFilePath = "output.txt";

            // Read lines from the input file
            List<string> lines = File.ReadAllLines(inputFilePath).ToList();

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
    }
}
