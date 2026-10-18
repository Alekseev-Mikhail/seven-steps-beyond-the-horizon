using UnityEngine;
using Utils;

namespace Encounter.Card.Ability
{
    public class Heal : ICardAbility
    {
        public static readonly Heal Instance = new();
        
        public CardAbilityType Type => CardAbilityType.Heal;
        public TargetSelectionType SelectionType => TargetSelectionType.PlayerCrewSingle;

        public void Play(TurnTakerData[] targets)
        {
            var target = targets[0];
            target.AlterHealth(1);
            
            // TODO: remove this
            Debug.Log(target.Health);
        }
    }
}