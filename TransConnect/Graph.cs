using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;

public class Graph
{
    private Dictionary<string, List<Tuple<string, double>>> adjacencyList;
    private Dictionary<string, string> previous; // Pour stocker les nœuds précédents

    public Graph()
    {
        adjacencyList = new Dictionary<string, List<Tuple<string, double>>>();
        previous = new Dictionary<string, string>(); // Initialiser previous
    }

    public void AddEdge(string source, string destination, double weight)
    {
        if (!adjacencyList.ContainsKey(source))
        {
            adjacencyList[source] = new List<Tuple<string, double>>();
        }
        adjacencyList[source].Add(new Tuple<string, double>(destination, weight));
    }

    public void LoadFromExcel(string filePath)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using (var package = new ExcelPackage(new FileInfo(filePath)))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
            int rowCount = worksheet.Dimension.Rows;
            for (int row = 2; row <= rowCount; row++)
            {
                string city1 = worksheet.Cells[row, 1].Text.Trim().ToUpper();
                string city2 = worksheet.Cells[row, 2].Text.Trim().ToUpper();
                double distance = double.Parse(worksheet.Cells[row, 3].Text);
                AddEdge(city1, city2, distance);
                AddEdge(city2, city1, distance);

                Console.WriteLine($"Chargement des villes : {city1} - {city2} : {distance} km");
            }
        }
    }

    public bool ContainsCity(string city)
    {
        return adjacencyList.ContainsKey(city);
    }

    public List<string> GetCities()
    {
        return new List<string>(adjacencyList.Keys);
    }

    public Dictionary<string, double> Dijkstra(string start)
    {
        var distances = new Dictionary<string, double>();
        var priorityQueue = new SortedSet<Tuple<double, string>>();

        previous.Clear(); // Effacer les précédents nœuds

        foreach (var node in adjacencyList.Keys)
        {
            distances[node] = double.PositiveInfinity;
            previous[node] = null;
        }
        distances[start] = 0;
        priorityQueue.Add(new Tuple<double, string>(0, start));

        while (priorityQueue.Count > 0)
        {
            var (currentDistance, currentNode) = priorityQueue.Min;
            priorityQueue.Remove(priorityQueue.Min);

            foreach (var neighbor in adjacencyList[currentNode])
            {
                var newDist = currentDistance + neighbor.Item2;
                if (newDist < distances[neighbor.Item1])
                {
                    priorityQueue.Remove(new Tuple<double, string>(distances[neighbor.Item1], neighbor.Item1));
                    distances[neighbor.Item1] = newDist;
                    previous[neighbor.Item1] = currentNode; // Mettre à jour le précédent nœud
                    priorityQueue.Add(new Tuple<double, string>(newDist, neighbor.Item1));
                }
            }
        }

        return distances;
    }

    public List<string> GetShortestPath(string start, string end)
    {
        var path = new List<string>();
        var currentNode = end;

        while (currentNode != null)
        {
            path.Add(currentNode);
            currentNode = previous[currentNode];
        }

        path.Reverse();
        return path;
    }
}
