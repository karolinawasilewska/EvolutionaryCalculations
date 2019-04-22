using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Lab5.Objects;
using static Lab5.Objects.Criteria;

namespace Lab5.Helpers
{
    public class FileReaderHelper
    {
        public List<Criteria> Read()
        {
            List<Criteria> criterias = new List<Criteria>();
            const Int32 BufferSize = 128;
            using (var fileStream = File.OpenRead(@"C:\Users\kwasi\source\repos\Lab1\Lab5\criteria.txt"))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] criteriaArray = line.Split('|');
                    criterias.Add(new Criteria()
                    {
                        SelectionMode = (SelectionModes)(int.Parse(criteriaArray[0])),
                        StopCriteria = (StopCriterias)(int.Parse(criteriaArray[1])),
                        ContestSize = int.Parse(criteriaArray[2]),
                        PopulationSize = int.Parse(criteriaArray[3]),
                        GenerationCount=int.Parse(criteriaArray[4]),
                        Time= TimeSpan.Parse(criteriaArray[5]),
                        MinRange=double.Parse(criteriaArray[6]),
                        MaxRange= double.Parse(criteriaArray[7])
                    });
                }
            }
            return criterias;
        }
    }
}
