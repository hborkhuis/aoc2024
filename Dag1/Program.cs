// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;


Part1();
Part2();

void Part1()
{
    FillSortedLists(out var first, out var second);
    first.Sort();
    second.Sort();
    int totalDist = 0;
    for (int i = 0; i < first.Count; i++)
    {
        totalDist += Math.Abs(first[i] - second[i]);
    }
    Console.WriteLine($"Total distance: {totalDist}");
}

// Part 2
void Part2()
{
    FillSortedLists(out var first, out var second);
    int similarity = 0;
    for (int i = 0; i < first.Count; i++)
    {
        int count = second.Count(x => x == first[i]);
        similarity += first[i] * count;
    }
    Console.WriteLine($"Similarity: {similarity}");
}

void FillSortedLists(out List<int> firstList, out List<int> secondList)
{
    Regex regex = new Regex("(?<first>\\d+) +(?<second>\\d+)");
    var reader = File.OpenText("input.txt");
    string? line = reader.ReadLine();
    firstList = new List<int>();
    secondList = new List<int>();

    while (line != null)
    {
        Match m = regex.Match(line);
        firstList.Add(int.Parse(m.Groups["first"].Value));
        secondList.Add(int.Parse(m.Groups["second"].Value));
        line = reader.ReadLine();
    }
}