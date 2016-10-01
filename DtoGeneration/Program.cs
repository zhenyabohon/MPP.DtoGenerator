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
            IDtoParseService parser = new DtoParseService();
            DtoClassCreator creator = new DtoClassCreator();
            foreach (var method in parser.GetDtoClassModels(@"C:\Logs\json.txt")) { 
                creator.GenerateDtoClass(method, @"C:\Logs\");
            }

            
        }
    }
}
