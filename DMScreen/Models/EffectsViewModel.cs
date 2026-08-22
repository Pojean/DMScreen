namespace DMScreen.Models
{
    public class EffectsViewModel
    {
        public List<List<Effect>> SortedEffects;
        private List<Effect> Offense;
        private List<Effect> Defense;
        private List<Effect> Restorative;
        private List<Effect> Utility;
        public EffectsViewModel()
        {
            SortedEffects = new List<List<Effect>>();
            Offense = new List<Effect>();
            Defense = new List<Effect>();
            Restorative = new List<Effect>();
            Utility = new List<Effect>();
            SortedEffects.Add(Offense);
            SortedEffects.Add(Defense);
            SortedEffects.Add(Restorative);
            SortedEffects.Add(Utility);
        }

        public void SortEffectsLibrary(EffectLibrary effectLibrary)
        {
            foreach (Effect effect in effectLibrary.effectsLibrary)
            {
                switch (effect.Type.ToLower())
                {
                    case "offense": { SortedEffects[0].Add(effect); } break;
                    case "defense": { SortedEffects[1].Add(effect); } break;
                    case "restorative": { SortedEffects[2].Add(effect); }; break;
                    case "utility": { SortedEffects[3].Add(effect); } break;
                    default: break;
                }
            }
        }

        public List<List<Effect>> SortByConditions(string Rarity, string searchTerm)
        {
            List<List<Effect>> local = SortedEffects;            //sort for rarity

            local = SortForTerm(searchTerm, local);
            local = SortForRarity(Rarity, local);

            return local;
        }

        private List<List<Effect>> SortForRarity(string Rarity, List<List<Effect>> library)
        {
            if (Rarity != "None")
            {
                foreach(List<Effect> list in library)
                {
                    list.RemoveAll(obj => obj.Tier != Rarity); //we use a delegate to remove all objects where the object.tier does not match string Rarity
                }
            }

            return library;
        }

        private List<List<Effect>> SortForTerm(string term, List<List<Effect>> library)
        {
            if(term != null)
            {
                foreach(List<Effect> list in library)
                {
                    list.RemoveAll(obj => !obj.Name.Contains(term, StringComparison.OrdinalIgnoreCase)); //we do the same here, but we remove any object where object.Name does not contain the string term
                    // StringCOmparison.OrdinalIgnoreCase ensures that we ignore upper and lower case lettering. 
                }
            }

            return library;
        }
    }
}
