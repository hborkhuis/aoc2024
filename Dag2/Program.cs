// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;
using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");

FillLists(out List<int[]> lines);
List<int[]>differntiatedLines = Differentiate(lines);
int safeItems = 0;
int lineIndex = 0;
foreach (var line in differntiatedLines)
{
    if(Safe(line))
    {
        safeItems++;
        Console.WriteLine($"Safe line: linenum={lineIndex+1}: {string.Join(',', lines[lineIndex])}");
    }
    lineIndex++;
}
Console.WriteLine(safeItems);

static bool Safe(int[] line)
{
    // Check if difference at most 3
    bool safe = line.All(x => Math.Abs(x) <= 3);
    // Check if at least (minus)one and all increasing or decreasing
    safe &= line.All(x => x <0) || line.All(x => x > 0);
    return safe;
}

static void FillLists(out List<int[]> list)
{
    var reader = File.OpenText("input.txt");
    list = new List<int[]>();

    string? line = reader.ReadLine();
    while (line != null)
    {
        int[] lineItems = line.Split(" ")
            .Select(n => int.Parse(n))
            .ToArray();
        list.Add(lineItems);
        line = reader.ReadLine();
    }
}

static List<int[]> Differentiate(List<int[]> list)
{
    List<int[]> outputList = new List<int[]>(list.Count);
    foreach(int[] line in list)
    {
        int[] outputItems = new int[line.Length-1];
        for (int i = 0; i < line.Length-1; i++) {     
            outputItems [i] = line[i+1] - line [i];
        }
        outputList.Add (outputItems);
    }
    return outputList;
}