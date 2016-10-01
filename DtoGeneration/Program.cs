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
            generator.GenerateDtoFiles(@"C:\Logs\json.txt", @"C:\Logs\");

        }
    }
}
