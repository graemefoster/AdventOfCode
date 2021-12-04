// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCodeDay3;
using Microsoft.VisualBasic;

var input = File.ReadAllLines("./input.txt").ToArray();

(string,string) CalculateImportant(IEnumerable<string> rows)
{
    return rows
        .Select(x => x.Select(x => x == '1').ToArray())
        .Pivot()
        .Select(x => (
            x.Count(val => val) >= x.Count(val => !val) ? '1' : '0', 
            x.Count(val => val) >= x.Count(val => !val) ? '0' : '1'))
        .ToArray()
        .Aggregate(("",""), (output, one) => (output.Item1 + one.Item1, output.Item2 + one.Item2));
}

var output1 = Convert.ToInt32(CalculateImportant(input).Item1, 2);
var output2 = Convert.ToInt32(CalculateImportant(input).Item2, 2);
var lifeSupportRating = output1 * output2;

Console.WriteLine($"Life Support Rating: {lifeSupportRating}");
Console.WriteLine($"Binary result 1: {output1}");
Console.WriteLine($"Binary result 2: {output2}");

var allOx = input;
var allCO2 = input;
var index = 0;
do
{
    var importantBitsOx = CalculateImportant(allOx);
    allOx = allOx.Where(x => x[index] == importantBitsOx.Item1[index]).ToArray();
    index = index + 1;
    
} while (allOx.Length > 1);

index = 0;
do
{
    var importantBitsCO2 = CalculateImportant(allCO2);
    allCO2 = allCO2.Where(x => x[index] == importantBitsCO2.Item2[index]).ToArray();
    Console.WriteLine(importantBitsCO2.Item2);
    Console.WriteLine(Strings.Join(allCO2, ", "));
    index = index + 1;
} while (allCO2.Length > 1);

Console.WriteLine($"Oxygen: {allOx.Single()}");
Console.WriteLine($"CO2: {allCO2.Single()}");

var output3 = Convert.ToInt32(allOx.Single(), 2);
var output4 = Convert.ToInt32(allCO2.Single(), 2);
lifeSupportRating = output3 * output4;
Console.WriteLine($"Life Support Rating: {lifeSupportRating}");

