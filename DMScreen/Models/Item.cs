namespace DMScreen.Models
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Effects { get; set; }

        public Item()
        {
            Effects = new List<string>();
        }
    }
}
