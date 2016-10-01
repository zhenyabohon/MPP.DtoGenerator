using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core.Services
{
    public class DtoParseService : IDtoParseService
    {
        private Dictionary<string, TypeEnum> types;

        private Dictionary<string, FormatEnum> formats;

        public DtoParseService()
        {
            InitializeFormats();
            InitializeTypes();
        }

        public List<DtoClassModel> GetDtoClassModels(string path)
        {
            var models = new List<DtoClassModel>();
            string source = File.ReadAllText(path);
            JObject deserializedObject = JObject.Parse(source);
            var x = deserializedObject["classDescriptions"];
            foreach (var item in x)
            {
                var model = new DtoClassModel();
                model.Name = item["className"].ToString();
                var properties = new List<PropertyModel>();
                foreach (var property in item["properties"])
                {
                    property["format"] = property["format"] ?? "";
                    properties.Add(new PropertyModel()
                    {
                        Name = property["name"].ToString(),
                        Type = types[property["type"].ToString()],
                        Format = formats[property["format"].ToString()]
                    });
                }
                model.Properties = properties;
                models.Add(model);
            }

            return models;
        }

        private void InitializeTypes()
        {
            this.types = new Dictionary<string, TypeEnum>();
            this.types.Add("integer", TypeEnum.Integer);
            this.types.Add("number", TypeEnum.Number);
            this.types.Add("string", TypeEnum.String);
            this.types.Add("boolean", TypeEnum.Boolean);
        }

        private void InitializeFormats()
        {
            this.formats = new Dictionary<string, FormatEnum>();
            this.formats.Add("int32", FormatEnum.Int32);
            this.formats.Add("int64", FormatEnum.Int64);
            this.formats.Add("float", FormatEnum.Float);
            this.formats.Add("double", FormatEnum.Double);
            this.formats.Add("byte", FormatEnum.Byte);
            this.formats.Add("date", FormatEnum.Date);
            this.formats.Add("string", FormatEnum.String);
            this.formats.Add("", FormatEnum.None);
        }
    }
}
