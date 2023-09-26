using MoviesAndCustomers.src.FileReader;
using System.Xml;

namespace MoviesAndCustomersTests
{
    public class Tests
    {
        FileConverter fileConverter;
        
        [SetUp]
        public void Setup()
        {
            fileConverter = new FileConverter();
        }

        [Test]
        public void TabReader_ShouldCreateXMLFile()
        {
            // Arrange
            string basePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string xmlFilePath = @"Files\output.xml";
            string fullPathXml = Path.Combine(basePath, xmlFilePath);

            if (File.Exists(fullPathXml))
            {
                File.Delete(fullPathXml);
            }

            // Act
            FileConverter.TabReader();

            // Assert
            Assert.IsTrue(File.Exists(fullPathXml), "XML file should be created.");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fullPathXml);

            XmlNodeList rowNodes = xmlDoc.SelectNodes("/DATA/ROW");
            Assert.IsTrue(rowNodes.Count >= 1, "At least one ROW element should be present.");

            XmlNode productCodeNode = rowNodes[0].SelectSingleNode("PRODUCT_CODE");
            Assert.IsNotNull(productCodeNode, "PRODUCT_CODE element should exist in the first ROW.");

            string productCodeValue = productCodeNode.InnerText;
            Assert.IsFalse(string.IsNullOrWhiteSpace(productCodeValue), "PRODUCT_CODE value should not be empty or null.");

            File.Delete(fullPathXml);
        }
    }
}