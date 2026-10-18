using System;
using Encounter.Card.Ability;

namespace Encounter.Gear
{
    [Serializable]
    public class Sword : IGearItem
    {
        private static readonly ICardAbility[] Abilities = { Attack.Instance };
        
        public static readonly Sword Instance = new();
        
        public ICardAbility[] GetCardAbilities()
        {
            return Abilities;
        }
    }
}