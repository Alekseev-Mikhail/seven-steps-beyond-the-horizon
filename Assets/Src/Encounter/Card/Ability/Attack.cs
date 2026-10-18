using UnityEngine;
using Utils;

namespace Encounter.Card.Ability
{
    public class Attack : ICardAbility
    {
        public static readonly Attack Instance = new();
        
        public CardAbilityType Type => CardAbilityType.Attack;
        public TargetSelectionType SelectionType => TargetSelectionType.EnemyCrewSingle;

        public void Play(TurnTakerData[] targets)
        {
            var target = targets[0];
            target.AlterHealth(-5);
            
            // TODO: remove this
            Debug.Log(target.Health);
        }
    }
}