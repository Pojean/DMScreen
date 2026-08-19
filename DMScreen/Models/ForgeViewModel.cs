namespace DMScreen.Models
{
    public class ForgeViewModel
    {
        public EffectsViewModel effects;
        public ItemInProgress item;

        public string ErrorMessage;

        public ForgeViewModel()
        {
            item = new ItemInProgress();
            effects = new EffectsViewModel();
            ErrorMessage = string.Empty;
        }
    }
}
