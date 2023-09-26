using MoviesAndCustomers.src.Model;
using MoviesAndCustomers.src.NameBreakdown;
using Newtonsoft.Json;

namespace MoviesAndCustomersTests
{
    public class NameReaderTest
    {
        NameReader nameReader;
        [SetUp]
        public void Setup()
        {
            nameReader = new NameReader();
        }

        [Test]
        public void NameBreakdown_ShouldCreateAJsonFileWithNameBreakdown()
        {
            // Arrange
            string basePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string jsonPath = @"Files\names_output.json";
            string fullJsonPath = Path.Combine(basePath, jsonPath);

            if (File.Exists(fullJsonPath))
            {
                File.Delete(fullJsonPath);
            }

            // Act
            NameReader.NameBreakdown();

            // Assert
            Assert.IsTrue(File.Exists(fullJsonPath), "JSON file should be created.");

            List<NameModels> nameModelsList;
            using (StreamReader file = new StreamReader(fullJsonPath))
            {
                string json = file.ReadToEnd();
                nameModelsList = JsonConvert.DeserializeObject<List<NameModels>>(json);
            }

            Assert.IsNotNull(nameModelsList, "Deserialized list should not be null.");
            Assert.IsTrue(nameModelsList.Count >= 1, "At least one item should be present in the JSON.");

            File.Delete(fullJsonPath);
        }
    }
}
