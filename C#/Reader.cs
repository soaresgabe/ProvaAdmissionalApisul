using System;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using ElevatorProject;
using System.Collections.Generic;

namespace ElevatorProject
{
    class Reader 
    {

        // Read the JSON file.
        public dynamic getData(string input)
        {
            if (File.Exists(input))
            {
                var answers = JsonConvert.DeserializeObject<List<Data>>
                (File.ReadAllText(input));

                if (answers != null)
                    return answers;
            }

            Console.WriteLine("Houve um erro na leitura do arquivo.");

            return 0;
        }
    }
}