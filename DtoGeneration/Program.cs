using Core;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            IDtoGenerationService generator = new DtoGenerationService();
            string jsonPath;
            string folder;
            Console.Write("Json path: ");
            jsonPath = Console.ReadLine();
            Console.Write("Output folder path: ");
            folder = Console.ReadLine();
            generator.GenerateDtoFiles(jsonPath, folder);

        }
    }
}
