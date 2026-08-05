namespace DMScreen.Models
{
    public class Effect
    {
        public string Name { get; set; }
        public string Tier { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public Effect()
        {

        }

        public bool Validate(Effect effect)
        {
            bool validated = false;

            if (Name != string.Empty && Tier != string.Empty && Type != string.Empty && Description != string.Empty)
            {
                validated = true;
            }

            return validated;
        }
    }
}