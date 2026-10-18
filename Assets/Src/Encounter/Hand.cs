using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Encounter.Card;
using Encounter.Card.Factory;
using UnityEngine.Events;
using Utils;
using Random = System.Random;

namespace Encounter
{
    public class Hand
    {
        private readonly Random _random = new();
        private readonly TurnTakerData _data;
        private readonly ImmutableArray<BaseCard> _deck;
        
        public ImmutableArray<BaseCard> Cards { get; private set; }
        public readonly UnityEvent<BaseCard> OnCardPlayedEvent = new();

        public Hand(TurnTakerData data, ICardFactory factory)
        {
            if (data.gear.Length == 0) throw new Exception("No gear");

            var deck = new List<BaseCard>(data.gear.Length);
            foreach (var item in data.gear)
            {
                var abilities = item.GetCardAbilities();
                for (var i = 0; i < abilities.Length; i++)
                {
                    var card = factory.CreateCard(i, abilities[i].Type);
                    card.SetAbility(abilities[i]);
                    deck.Add(card);
                }
            }

            _data = data;
            _deck = deck.ToImmutableArray();
        }

        public void Form()
        {
            var cards = new BaseCard[Math.Min(_data.HandCapacity, _deck.Length)];

            var copiedDeck = new List<BaseCard>(_deck);
            for (var i = 0; i < _data.HandCapacity && i < _deck.Length; i++)
            {
                var randomIndex = _random.Next(0, copiedDeck.Count);
                cards[i] = copiedDeck[randomIndex];
                cards[i].OnCardPlayedEvent.AddListener(OnCardPlayed);
                copiedDeck.RemoveAt(randomIndex);
            }
            
            Cards = cards.ToImmutableArray();
        }

        private void OnCardPlayed(BaseCard card)
        {
            Cards = Cards.Remove(card);
            card.OnCardPlayedEvent.RemoveListener(OnCardPlayed);
            OnCardPlayedEvent.Invoke(card);
        }
    }
}