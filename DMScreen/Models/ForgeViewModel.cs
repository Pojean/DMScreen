namespace DMScreen.Models
{
    public class ForgeViewModel
    {
        public EffectsViewModel effects;
        public ItemInProgress item;

        public ForgeViewModel()
        {
            item = new ItemInProgress();
            effects = new EffectsViewModel();
        }
    }
}
