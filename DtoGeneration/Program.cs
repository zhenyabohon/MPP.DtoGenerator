using Core;
using Core.Models;
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
            DtoClassCreator creator = new DtoClassCreator();
            creator.GenerateDtoClass(new Core.Models.DtoClassModel()
            {
                Name = "Khovansky",
                Properties = new List<PropertyModel>()
                {
                    new PropertyModel()
                    {
                        Name = "LiverDamage",
                        Format = FormatEnum.Int32,
                        Type = TypeEnum.Integer
                    }
                }
            }, @"C:\Logs\");
        }
    }
}
