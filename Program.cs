using System;
using System.Text.Json;

namespace Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            string data = " [ {\"name\": \"John Doe\", \"occupation\": \"gardener\"}, " +
    "{\"name\": \"Peter Novak\", \"occupation\": \"driver\"} ]";

            using JsonDocument doc = JsonDocument.Parse(data);
            JsonElement root = doc.RootElement;
            Console.WriteLine(root[0].GetProperty("name"));
        }
    }
}
