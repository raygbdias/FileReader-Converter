using System.Xml;

namespace MoviesAndCustomers.src.FileReader
{
    public class FileConverter
    {
        public static void TabReader()
        {
            string basePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string relativePath = @"Files\products-private-file.tab";
            string fullPathTab = Path.Combine(basePath, relativePath);

            string xmlFilePath = @"Files\output.xml";
            string fullPathXml = Path.Combine(basePath, xmlFilePath);

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement rootElement = xmlDoc.CreateElement("DATA");
            xmlDoc.AppendChild(rootElement);


            string[] lines = File.ReadAllLines(fullPathTab);

            if (lines.Length < 2)
            {
                Console.WriteLine("Tab file does not contain header and content.");
                return;
            }

            string[] headerColumns = lines[0].Split('\t');

            for (int i = 1; i < lines.Length; i++)
            {
                string[] dataColumns = lines[i].Split('\t');

                XmlElement rowElement = xmlDoc.CreateElement("ROW");

                for (int j = 0; j < headerColumns.Length; j++)
                {
                    XmlElement childElement = xmlDoc.CreateElement(headerColumns[j]);
                    childElement.InnerText = dataColumns[j];
                    rowElement.AppendChild(childElement);
                }

                rootElement.AppendChild(rowElement);
            }

            xmlDoc.Save(fullPathXml);

            Console.WriteLine($"XML file '{fullPathXml}' created successfully.");
        }
    }
}

