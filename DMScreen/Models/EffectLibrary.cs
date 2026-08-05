using DMScreen.Services;
using System.Text.Json;

namespace DMScreen.Models
{
    public class EffectLibrary
    {
        public List<Effect> effectsLibrary { get; set; }

        public EffectLibrary()
        {
            effectsLibrary = new List<Effect>();  
        }

        public void SaveLibrary()
        {
            FileIO.SerialiseEffectLibrary(this);
        }

        public void LoadLibrary()
        {
            string jsonLibrary = FileIO.ReadFile("Effects.json");
            EffectLibrary local = JsonSerializer.Deserialize<EffectLibrary>(jsonLibrary);
            effectsLibrary = local.effectsLibrary;
        }

        public void Add(Effect effect)
        {
            if (effect.Validate(effect))
            {
                effectsLibrary.Add(effect);
                SaveLibrary();
            }
        }
    }
}
