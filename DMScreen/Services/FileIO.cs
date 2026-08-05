using DMScreen.Models;
using System.Text.Json;

namespace DMScreen.Services
{
    public class FileIO
    {
        public void SerialiseItem(Item item)
        {
            // Here we serialise an item into a json string, and then call WriteFile to add it to the Items file
            string targetFile = "Items.json";
            string itemString = JsonSerializer.Serialize(item, new JsonSerializerOptions { WriteIndented = true});

            WriteFile(targetFile, itemString);
        }

        public List<string> ReadFile(string targetFile)
        {
            //Here we read from a file, the directory is specified because we never want files from any place other than that folder
            //The file name is variable depending on the parameter.
            string root = Path.Combine(AppContext.BaseDirectory, "UpdateFilesGoHere");
            string path = Path.Combine(root, targetFile);

            List<string> readLines = new List<string>();
            string line;
            using (StreamReader reader = new StreamReader(path))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    readLines.Add(line);
                }

            }

            return readLines;
        }

        private void WriteFile(string targetFile, string TextToAppend)
        {
            //Here we write to a file, and always put it in the UpdateFilesGoHere folder, the file name is determined in the parameters
            string root = Path.Combine(AppContext.BaseDirectory, "UpdateFilesGoHere");
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            string path = Path.Combine(root, targetFile);

            using(StreamWriter writer = new StreamWriter(path, true))
            {
               writer.WriteLine(TextToAppend);
            }
        }
    }
}
