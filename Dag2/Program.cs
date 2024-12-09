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
safeItems = 0;
lineIndex = 0;
foreach (var line in differntiatedLines)
{
    if(Safe(line))
    {
        safeItems++;
        Console.WriteLine($"Safe line: linenum={lineIndex+1}: {string.Join(',', lines[lineIndex])}");
    }
    else if(TryFixLine(line, out int[] fixedLine))
    {
        if(Safe(fixedLine))
        {
            safeItems++;
            Console.WriteLine($"Successfuly fixed line: linenum={lineIndex+1}: {string.Join(',', lines[lineIndex])}");
        }
        else
        {
            Console.WriteLine($"Fxed line not succesfull: linenum={lineIndex+1}: {string.Join(',', lines[lineIndex])}");
        }
    }
    else
    {
        Console.WriteLine($"Could not fixed line: linenum={lineIndex+1}: {string.Join(',', lines[lineIndex])}");
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

static bool TryFixLine(int[] line, out int[] fixedLine)
{
    bool lineFixed = false;
    List<int>fixedList = new List<int>();
    int currDirrection = 0;
    int skippedDifference = 0;
    int nrSkipped = 0;
    for(int i = 0; i < line.Length; i++)
    {
        bool skip = line[i] == 0 || line[i]*currDirrection < 0 || Math.Abs(line[i]) > 3;
        if (skip) {
            // Remember the skipped one, add it to the next (only if not the first level in line)
            if(i>0)
                skippedDifference = line[i];
            nrSkipped++;
        }
        else
        {
            fixedList.Add(line[i] + skippedDifference);
            skippedDifference = 0;
            currDirrection = line[i]/Math.Abs(line[i]);
        }
    }
    if(nrSkipped <2)
    {
        lineFixed = true;
        fixedLine = fixedList.ToArray();
        Console.WriteLine($"Line {string.Join(',', line)} fixed to {string.Join(',', fixedLine)}");
    }
    else {
        // nothing done
        fixedLine = line.ToArray();
    }
    return lineFixed;
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