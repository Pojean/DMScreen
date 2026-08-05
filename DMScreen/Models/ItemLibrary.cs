using DMScreen.Services;
using System.Text.Json;

namespace DMScreen.Models
{
    public class ItemLibrary
    {
        public List<Item> itemLibrary { get; set; }

        public ItemLibrary()
        {
            itemLibrary = new List<Item>();
        }
        public void SaveLibrary()
        {
            FileIO.SerialiseItemLibrary(this);
        }

        public void LoadLibrary()
        {
            string jsonLibrary = FileIO.ReadFile("Items.json");
            ItemLibrary local = JsonSerializer.Deserialize<ItemLibrary>(jsonLibrary);
            itemLibrary = local.itemLibrary;
        }
    }
}