using Core.Models;
using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using Core.Services;

namespace Core
{
    public class DtoClassCreator
    {
        private List<Tuple<FormatEnum, TypeEnum, Type>> mappings;

        public DtoClassCreator()
        {
            mappings = GenerateMappings();
            DtoParseService parser = new DtoParseService();
        }

        public void GenerateDtoClass(DtoClassModel dtoModel, string outputPath)
        {
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();

            TextWriter writer = new StringWriter();

            CodeTypeDeclaration constructor = new CodeTypeDeclaration();
            constructor.Name = dtoModel.Name;
            constructor.TypeAttributes = System.Reflection.TypeAttributes.Sealed | System.Reflection.TypeAttributes.Public;
            constructor.IsClass = true;
            
            List<CodeMemberProperty> members = new List<CodeMemberProperty>();
            foreach (var property in dtoModel.Properties)
            {
                CodeMemberProperty member = new CodeMemberProperty();
                member.Name = property.Name;
                
                member.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                member.HasGet = true;
                member.HasSet = true;
                member.Type = new CodeTypeReference(GetDotNetType(property.Format, property.Type));
                members.Add(member);
            }

            constructor.Members.AddRange(members.ToArray());

            codeProvider.GenerateCodeFromType(constructor, writer, new CodeGeneratorOptions());

            File.AppendAllText($"{outputPath}{dtoModel.Name}.cs", writer.ToString());

            writer.Dispose();
        }

        private List<Tuple<FormatEnum, TypeEnum, Type>> GenerateMappings()
        {
            List<Tuple<FormatEnum, TypeEnum, Type>> mappings = new List<Tuple<FormatEnum, TypeEnum, Type>>();
            mappings.Add(new Tuple<FormatEnum, TypeEnum, Type>(FormatEnum.Int32, TypeEnum.Integer, typeof(int)));
            mappings.Add(new Tuple<FormatEnum, TypeEnum, Type>(FormatEnum.Int64, TypeEnum.Integer, typeof(long)));
            mappings.Add(new Tuple<FormatEnum, TypeEnum, Type>(FormatEnum.Float, TypeEnum.Number, typeof(float)));
            mappings.Add(new Tuple<FormatEnum, TypeEnum, Type>(FormatEnum.Double, TypeEnum.Number, typeof(double)));
            mappings.Add(new Tuple<FormatEnum, TypeEnum, Type>(FormatEnum.Byte, TypeEnum.String, typeof(byte)));
            mappings.Add(new Tuple<FormatEnum, TypeEnum, Type>(FormatEnum.String, TypeEnum.String, typeof(string)));
            mappings.Add(new Tuple<FormatEnum, TypeEnum, Type>(FormatEnum.Date, TypeEnum.String, typeof(DateTime)));
            mappings.Add(new Tuple<FormatEnum, TypeEnum, Type>(FormatEnum.None, TypeEnum.Boolean, typeof(bool)));
            return mappings;
        }

        private Type GetDotNetType(FormatEnum format, TypeEnum type)
        {
            return mappings.FirstOrDefault(x => x.Item1 == format && x.Item2 == type).Item3;
        }

    }
}
