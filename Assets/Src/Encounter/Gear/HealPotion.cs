using System;
using Encounter.Card.Ability;

namespace Encounter.Gear
{
    [Serializable]
    public class HealPotion : IGearItem
    {
        private static readonly ICardAbility[] Abilities = { Heal.Instance };
        
        public static readonly HealPotion Instance = new();
        
        public ICardAbility[] GetCardAbilities()
        {
            return Abilities;
        }
    }
}