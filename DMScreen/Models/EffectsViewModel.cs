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
    }
}
