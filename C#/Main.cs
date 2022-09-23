using System;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using ElevatorProject;

namespace ElevatorProject
{
    class Program
    {
        static void Main()
        {
            // Ler JSON.
            string filePath = @"C:\\Users\\soare\\OneDrive\\Documents\\Codes\\Visual Studio\\Apisul\\Apisul\\input.json";

            Reader fReader = new Reader();
            Process fInfo = new Process();

            List<Data> answers = fReader.getData(filePath);

            // Preencher as respostas.
            fInfo.answer(answers);
        }
    }
}