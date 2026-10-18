using UnityEngine;

namespace Encounter.Card.Factory
{
    public class EnemyCardFactory : ICardFactory
    {
        public static EnemyCardFactory Instance { get; } = new();
        
        public BaseCard CreateCard(int index, CardAbilityType abilityType)
        {
            var obj = new GameObject("Enemy card " + index);
            var card = obj.AddComponent<EnemyCard>();
            obj.SetActive(false);
            return card;
        }
    }
}