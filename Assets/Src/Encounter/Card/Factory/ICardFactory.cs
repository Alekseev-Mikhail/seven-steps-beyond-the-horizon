namespace Encounter.Card.Factory
{
    public interface ICardFactory
    {
        public BaseCard CreateCard(int index, CardAbilityType abilityType);
    }
}