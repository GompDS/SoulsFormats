using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace SoulsFormats
{
    /// <summary>
    /// Defines the identity of a collection of GXParams in order to make the values easy to interpret and edit.
    /// A GXListDef is read from a json file containing data which was collected by RunDevelopment. 
    /// </summary>
    public class GXListDef : List<GXListDef.GXParamDef>
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public class GXParamDef
        {
            public string ID { get; set; }
            public int Unk04 { get; set; }
            public string Category { get; set; } = null;
            public List<ValueDef> Items { get; set; } = new List<ValueDef>();
        }
    
        public enum ValueType
        {
            Unknown = 0,
            Int = 1,
            Float = 2,
            Enum = 3, // int with specific values
            Bool = 4, // int (0 - 1)
        }

        public class ValueDef
        {
            public string Name { get; set; }
            public ValueType Type { get; set; }
            public float Min { get; set; } = 0;
            public float Max { get; set; } = 0;
            public Dictionary<int, string> Enum { get; set; } = null;
        }

        public static GXListDef Read(string filename)
        {
            using (StreamReader stream = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<GXListDef>(new JsonTextReader(stream));
            }
        }
    }
}