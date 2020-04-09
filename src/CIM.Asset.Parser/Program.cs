﻿using System;
using System.Xml;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace CIM.Asset.Parser
{
    class Program
    {
        public static void Main(string[] args)
        {
            RegisterCodePages();

            var xmlReader = CreateXmlReader("../cim-model/com.xml");
            LoadClasses(xmlReader);
        }

        private static XmlTextReader CreateXmlReader(string path)
        {
            var reader = new StreamReader("../cim-model/cim.xml", System.Text.Encoding.GetEncoding("windows-1252"), true);
            return new XmlTextReader(reader);
        }

        private static void RegisterCodePages()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("windows-1252");
        }

        private static void LoadClasses(XmlTextReader xmlReader)
        {
            var xElement = XElement.Load(xmlReader);

            var classes = xElement.Descendants().OfType<XElement>()
                .Where(x => x.Name.LocalName == "Class");

            var cimEntities = classes?
                .Select(x => new CimEntity { Name = x.Attribute("name")?.ToString(), XmiId = x.Attribute("xmi.id")?.ToString() });

            foreach (var cimEntity in cimEntities)
            {
                Console.WriteLine(cimEntity.XmiId + " " + cimEntity.Name);
            }
        }
    }
}
