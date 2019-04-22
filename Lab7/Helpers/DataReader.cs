using System;
using System.IO;

namespace Lab8.Helpers
{
    public class DataReader
    {
        static public City[] ReadData() {

            City[] cities = new City[ENVIRONMENT.IndividualSize];
            
            //const Int32 BufferSize = 128;
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../../data.txt");
            int index = 0;
            using (var fileStream = File.OpenRead(path))
            using (var streamReader = new StreamReader(fileStream))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] lineArray = line.Split(' ');
                    cities[index] = new City()
                    {
                        Index = int.Parse(lineArray[0]),
                        Longitude = double.Parse(lineArray[1].Replace('.', ',')),
                        Latitude = double.Parse(lineArray[2].Replace('.', ',')),
                    };
                    index++;
                }
            }
            return cities;
        } 
    }
}
