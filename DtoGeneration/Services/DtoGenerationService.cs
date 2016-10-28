using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DtoGeneration;

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
            var resetEvents = new ManualResetEvent[models.Count];
            for (int i = 0; i < models.Count; i++)
            {
                resetEvents[i] = new ManualResetEvent(false);

            }


            var semaphore = new Semaphore(ConfigurationSettingsManager.MaxThreads, ConfigurationSettingsManager.MaxThreads);

            foreach (var model in models)
            {
                semaphore.WaitOne();
                ThreadPool.QueueUserWorkItem(x =>
                {
                    var dto = creator.GenerateDtoClass(model, ConfigurationSettingsManager.Namespace);
                    File.WriteAllText($"{outputDirectory}\\{model.Name}.cs", dto);
                    resetEvents[models.IndexOf(model)].Set();
                    semaphore.Release();
                });        

            }

            foreach (var resetEvent in resetEvents)
            {
                resetEvent.WaitOne();
                resetEvent.Dispose();
            }
            Console.WriteLine("Complete");
            Console.Read();
        }


    }
}
