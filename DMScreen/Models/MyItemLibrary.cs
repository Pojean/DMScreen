using DMScreen.Services;
using System.Text.Json;

namespace DMScreen.Models
{
    public class MyItemLibrary
    {
        public List<Item> itemLibrary { get; set; }

        public MyItemLibrary()
        {
            itemLibrary = new List<Item>();
        }
        public void SaveLibrary()
        { 
            FileIO.SerialiseItemLibrary(this);
        }

        public void LoadLibrary()
        {
            string jsonLibrary = FileIO.ReadFile("MyItems.json");
            MyItemLibrary local = JsonSerializer.Deserialize<MyItemLibrary>(jsonLibrary);
            itemLibrary = local.itemLibrary;
        }
    }
}
