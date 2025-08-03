using System;
using System.Collections.Generic;

public static class Recursion
{
    // Problem 1: Sum of Squares
    public static int SumSquaresRecursive(int n)
    {
        if (n <= 0) return 0;
        return n * n + SumSquaresRecursive(n - 1);
    }

    // Problem 2: Permutations Choose
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        for (int i = 0; i < letters.Length; i++)
        {
            string newLetters = letters.Substring(0, i) + letters.Substring(i + 1);
            PermutationsChoose(results, newLetters, size, word + letters[i]);
        }
    }

    // Problem 3: Count Ways to Climb Stairs (1, 2, 3 steps)
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        if (s == 0) return 1;
        if (s < 0) return 0;

        if (remember == null) remember = new Dictionary<int, decimal>();
        if (remember.ContainsKey(s)) return remember[s];

        decimal ways = CountWaysToClimb(s - 1, remember) +
                       CountWaysToClimb(s - 2, remember) +
                       CountWaysToClimb(s - 3, remember);

        remember[s] = ways;
        return ways;
    }

    // Problem 4: Wildcard Binary Generator
    public static void WildcardBinary(string pattern, List<string> results)
    {
        int index = pattern.IndexOf('*');
        if (index == -1)
        {
            results.Add(pattern);
            return;
        }

        WildcardBinary(pattern.Substring(0, index) + "0" + pattern.Substring(index + 1), results);
        WildcardBinary(pattern.Substring(0, index) + "1" + pattern.Substring(index + 1), results);
    }

    // Problem 5: Solve Maze Paths from (0,0) to (x,y)
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        if (currPath == null)
            currPath = new List<ValueTuple<int, int>>();

        if (!maze.IsValidMove(currPath, x, y))
            return;

        currPath.Add((x, y));

        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());
        }
        else
        {
            SolveMaze(results, maze, x + 1, y, new List<(int, int)>(currPath));
            SolveMaze(results, maze, x - 1, y, new List<(int, int)>(currPath));
            SolveMaze(results, maze, x, y + 1, new List<(int, int)>(currPath));
            SolveMaze(results, maze, x, y - 1, new List<(int, int)>(currPath));
        }
    }
}
