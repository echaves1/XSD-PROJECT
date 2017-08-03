using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace Teste
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlTextReader reader = new XmlTextReader("GEN0001.XSD");
            XmlSchema schema = XmlSchema.Read(reader, ValidationCallback);
            //schema.Write(Console.Out);

            
            foreach (var item in schema.Items)
            {                
                XmlSchemaComplexType complexType = item as XmlSchemaComplexType;            
                if (complexType != null)
                {
                    Console.WriteLine(complexType.Name + "--------------------");
                    XmlSchemaSequence sequence = complexType.Particle as XmlSchemaSequence;
                    foreach (var seqItem in sequence.Items)
                    {
                        XmlSchemaElement element = seqItem as XmlSchemaElement;
                        Console.WriteLine(element.Name);
                    }
                    foreach (var attItem in complexType.Attributes)
                    {
                        XmlSchemaAttribute attr = attItem as XmlSchemaAttribute;
                        Console.WriteLine(attr.Name);
                    }
                }

                XmlSchemaSimpleType simpleType = item as XmlSchemaSimpleType;
                if (simpleType != null)
                {
                    Console.WriteLine(simpleType.Name + " **************");
                    XmlSchemaSimpleTypeRestriction rest = simpleType.Content as XmlSchemaSimpleTypeRestriction;
                    
                    Console.WriteLine(rest.BaseTypeName);

                    foreach (var facet in rest.Facets)
                    {
                        XmlSchemaFacet facet2 = facet as XmlSchemaFacet;
                        Console.WriteLine(facet2.Value + " => " + facet2);
                    }

                }
            }

            Console.ReadLine();
        }

        private static void ValidationCallback(object sender, ValidationEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
