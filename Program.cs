﻿using System;
using System.Reflection.Metadata;
using System.Runtime;
using System.IO;
class Program
{
    static void Main(string[] args)
    {
        int suma = 0;
        string charToNumber = "0";
        string filename;
        if (args.Length > 0) filename = args[0];
        else
        {
            Console.WriteLine("Prosím napište název souboru: ");
            filename = Console.ReadLine();
        }
        string fileContent = File.ReadAllText(filename);
        string[] rows = fileContent.Split('\n');
        int rowLength = rows[0].Length;
        int rowCount = rows.Length;
        char[,] map = new char[rowLength, rowCount];
        int numberOfSymbol = 0;
        for (int y = 0; y < rowCount - 1; y++)  //Vytvoření mapy
        {
            for (int x = 0; x < rowLength - 1; x++)
            {
                map[y, x] = rows[y][x];
                if (isOdd(map[y, x]))
                {
                    numberOfSymbol++;
                }
            }
        }
        int[,] symbol = new int[numberOfSymbol, 2];
        int counter = 0;
        for (int y = 0; y < rowCount - 1; y++)
        {
            for (int x = 0; x < rowLength - 1; x++)
            {
                if (isOdd(map[y, x]))
                {
                    symbol[counter, 0] = y;
                    symbol[counter, 1] = x;
                    counter++;
                }
            }
        }
        int startX = -1;
        int endX = -1;
        for (int y = 0; y < rowCount - 1; y++) //Sčítání
        {
            bool isNumber = false;
            for (int x = 0; x < rowLength - 1; x++)
            {
                if (isNumber && startX == -1) startX = x - 1;
                if (char.IsDigit(map[y, x]))
                {
                    isNumber = true;
                    charToNumber = charToNumber + map[y, x];
                }
                else if (isNumber)
                {
                    endX = x - 1;
                    for (int i = 0; i < numberOfSymbol; i++)
                    {
                        if (symbol[i, 0] >= y - 1 && symbol[i, 0] <= y + 1)
                        {
                            if (symbol[i, 1] >= startX - 1 && symbol[i, 1] <= endX + 1) suma = suma + int.Parse(charToNumber);
                        }

                    }
                    charToNumber = "0";
                    isNumber = false;
                    startX = -1;
                    endX = -1;
                }
            }
        }

        Console.WriteLine(suma);
        bool isOdd(char x)
        {
            if (x == '@' || x == '*' || x == '/' || x == '=' || x == '$' || x == '+' || x == '%' || x == '#' || x == '&' || x == '-') return true;
            else return false;
        }
    }

}
