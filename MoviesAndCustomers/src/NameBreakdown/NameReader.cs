using MoviesAndCustomers.src.Model;
using Newtonsoft.Json;

namespace MoviesAndCustomers.src.NameBreakdown
{
    public class NameReader
    {
        public static readonly List<string> nameTitles = new List<string>
        {
            "MR", "MRS", "MISS", "MS", "DR", "PROF", "REV", "SIR", "LADY", "CAPT",
            "MAJOR", "COL", "MX", "HON", "PRES", "AMB", "SEN", "REP", "GOV", "MAYOR"
        };

        public static readonly List<string> nameComplements = new List<string>
        {
            "SR", "JR", "I", "II", "III", "IV", "V"
        };

        public static void NameBreakdown()
        {
            List<NameModels> nameModelsList = new List<NameModels>();

            string basePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string fileNamePath = @"Files\NAMES.txt";
            string fullNamePath = Path.Combine(basePath, fileNamePath);
            string fileJsonPath = @"Files\names_output.json";
            string fullJsonPath = Path.Combine(basePath, fileJsonPath);

            using (StreamReader fileName = new StreamReader(fullNamePath))
            {
                string[] ListName = fileName.ReadToEnd().Split("\r\n");

                foreach (string eachName in ListName)
                {
                    if (string.IsNullOrEmpty(eachName))
                        continue;

                    NameModels nameModels = new NameModels();
                    string eachNameModify = eachName.Replace("\t", " ");
                    nameModels.FullName = eachNameModify;
                    List<string> splitName = eachNameModify.Split(" ").ToList();

                    foreach (string part in splitName)
                    {
                        switch (part)
                        {
                            case string title when nameTitles.Contains(title):
                                nameModels.Title = title;
                                break;

                            case string complement when nameComplements.Contains(complement):
                                nameModels.Complement = complement;
                                break;

                            default:
                                break;
                        }
                    }

                    splitName.RemoveAll(part => part == nameModels.Title || part == nameModels.Complement);

                    if (splitName.Count >= 1)
                        nameModels.FirstName = splitName[0];

                    if (splitName.Count >= 2)
                        nameModels.LastName = splitName.Last();

                    if (splitName.Count > 2)
                        nameModels.MiddleName = string.Join(" ", splitName.Skip(1).Take(splitName.Count - 2));

                    nameModelsList.Add(nameModels);
                }
            }

            string json = JsonConvert.SerializeObject(nameModelsList);
            File.WriteAllText(fullJsonPath, json);
        }
    }
}
