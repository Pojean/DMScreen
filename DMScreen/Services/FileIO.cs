using AspNetCoreGeneratedDocument;
using DMScreen.Models;
using System.Text.Json;

namespace DMScreen.Services
{
    public static class FileIO
    {
        private static string GetUpdateFileGoHerePath()
        {
            string rootPath = Path.Combine(AppContext.BaseDirectory, "UpdateFilesGoHere");
            return rootPath;
        }
        public static void SerialiseItemLibrary(ItemLibrary items)
        {
            // Here we serialise an item into a json string, and then call WriteFile to add it to the Items file
            string targetFile = "Items.json";
            string itemString = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true});

            WriteFile(targetFile, itemString);
        }

        public static void SerialiseItemLibrary(MyItemLibrary items) //overload for MyItemLibrary
        {
            // Here we serialise an item into a json string, and then call WriteFile to add it to the Items file
            string targetFile = "MyItems.json";
            string itemString = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });

            WriteFile(targetFile, itemString);
        }

        public static void SerialiseEffectLibrary(EffectLibrary effects)
        {
            //Here we serialise an effect into a json string, and then call WriteFile to add it to the Effects file
            string targetFile = "Effects.json";
            string effectString = JsonSerializer.Serialize(effects, new JsonSerializerOptions { WriteIndented = true });

            WriteFile(targetFile, effectString);
        }

        public static string ReadFile(string targetFile)
        {
            //Here we read from a file, the directory is specified because we never want files from any place other than that folder
            //The file name is variable depending on the parameter.
            string root = GetUpdateFileGoHerePath();
            string path = Path.Combine(root, targetFile);

            string line;
            using (StreamReader reader = new StreamReader(path))
            {
                line = reader.ReadToEnd();

            }

            return line;
        }

        private static void WriteFile(string targetFile, string content)
        {
            //here we write to a file, but rather than line by line we write all characters at once, because of how we need to handle JSON's
            //JSON's should be handled as the full file, not line by line, JSON objects can be multiple lines

            string root = GetUpdateFileGoHerePath();
            if(!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            string path = Path.Combine(root, targetFile);

            using (StreamWriter writer = new StreamWriter(path, false))
            {
                writer.Write(content);
            }

        }
    }
}
