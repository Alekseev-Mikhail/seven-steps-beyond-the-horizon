using Utils;

namespace Encounter.Card.Ability
{
    public interface ICardAbility
    {
        CardAbilityType Type { get; }
        TargetSelectionType SelectionType { get; }
        
        public void Play(CreatureData[] targets);
    }
}