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

        public bool Validate()
        {
            bool validated = true;

            if(Name == null)
            { validated = false; }

            if(Rarity == null)
            { validated = false; }

            if(EffectSlots == 0)
            { validated = false; }

            if(Description == null)
            {  validated = false;}

            if(Effects == null)
            { validated = false;}

            return validated;
        }
    }
}
