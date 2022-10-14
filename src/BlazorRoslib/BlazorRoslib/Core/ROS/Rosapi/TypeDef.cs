using System;
using BlazorRoslib.Core.ROS.Services;

namespace BlazorRoslib.Core.ROS.Rosapi
{
    public class TypeDef
    {
        public List<TypeDefField> Fields { get; } = new List<TypeDefField>();

        public string Type { get; set; } = "Unknown";

        public static TypeDef FromTypeDefResponse(TypeDefResponse response)
        {
            TypeDef typeDef = new TypeDef();
            if (response.typedefs?.FirstOrDefault() is TypeDefEntry first)
            {
                typeDef.Type = first.type;
                for (int i = 0; i < (first?.fieldnames?.Length ?? 0); i++)
                {
                    typeDef.Fields.Add(new TypeDefField()
                    {
                        FieldName = first?.fieldnames[i] ?? "Unknown",
                        Type = first?.fieldtypes[i] ?? "Unknown"
                    });
                }
            }
            return typeDef;
        }
    }
}

