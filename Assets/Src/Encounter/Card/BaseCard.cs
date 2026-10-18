using System;
using Encounter.Card.Ability;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Encounter.Card
{
    public abstract class BaseCard : MonoBehaviour
    {
        protected ICardAbility Ability;
        public readonly UnityEvent<BaseCard> OnCardPlayedEvent = new();

        public void SetAbility(ICardAbility ability)
        {
            if (Ability != null || ability == null) throw new Exception("Ability already set");
            Ability = ability;
        }   

        public virtual void Play(CreatureData[] data)
        {
            OnCardPlayedEvent.Invoke(this);
        }
    }
}