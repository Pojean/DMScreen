namespace DMScreen.Models
{
    public class Item
    {
        public string Name { get; set; }
        public string Rarity { get; set; }
        public int EffectSlots { get; set; }
        public string Description { get; set; }
        public List<Effect> Effects { get; set; }

        public Item()
        {
            Effects = new List<Effect>();
        }
    }
}
