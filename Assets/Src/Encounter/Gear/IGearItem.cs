using Encounter.Card.Ability;

namespace Encounter.Gear
{
    public interface IGearItem
    {
        public ICardAbility[] GetCardAbilities();
    }
}