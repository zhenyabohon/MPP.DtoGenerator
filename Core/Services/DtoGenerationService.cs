using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Services
{
    public class DtoGenerationService : IDtoGenerationService
    {
        private IDtoParseService jsonParseSerice;

        private DtoClassCreator creator;

        public DtoGenerationService()
        {
            jsonParseSerice = new DtoParseService();
            creator = new DtoClassCreator();
        }

        public void GenerateDtoFiles(string jsonPath, string outputDirectory)
        {
            List<DtoClassModel> models = jsonParseSerice.GetDtoClassModels(jsonPath);
            ThreadPool.SetMaxThreads(int.Parse(ConfigurationManager.AppSettings["MaxThreads"]), int.Parse(ConfigurationManager.AppSettings["MaxThreads"]));
            foreach (var model in models)
            {
                ThreadPool.QueueUserWorkItem(x => creator.GenerateDtoClass(model, outputDirectory));
            }
        }


    }
}
