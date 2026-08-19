namespace DMScreen.Models
{
    public class ItemInProgress
    {
        public string Name { get; set; }
        public string Rarity { get; set; }
        public int EffectSlots { get; set; }
        public string Description { get; set; }
        public List<Effect> Effects { get; set; }

        public ItemInProgress()
        {
            Effects = new List<Effect>();
        }

        public void SetRarity(string rarity, bool masterCrafted)
        {
            Rarity = rarity;
            switch(rarity)
            {
                case "Common": { EffectSlots = 1; } break;
                case "Uncommon": { EffectSlots = 2; } break;
                case "Rare": { EffectSlots = 3; } break;
                case "Very Rare": { EffectSlots = 4; } break;
                case "Legendary": { EffectSlots = 5; } break;
                case "Artifact": { EffectSlots = 6; } break;
                default: break;
            }

            if(masterCrafted)
            {
                EffectSlots += 1;
            }
        }
    }
}
