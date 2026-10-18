using System;
using Encounter.Card;
using Encounter.Card.Factory;
using UnityEngine;

namespace Encounter.TurnTakers
{
    public sealed class EncounterPlayer : TurnTaker
    {
        [SerializeField] private EndTurnButton endTurnButton;
        protected override ICardFactory CardFactory => PlayerCardFactory.Instance;

        protected override void OnInit()
        {
            endTurnButton.OnEndTurn.AddListener(OnEndTurn);
            Hand.OnCardPlayedEvent.AddListener(OnCardPlayed);
        }

        protected override void OnStartTurn()
        {
            PositionCards();
            SetIsCardsVisible(true);
            // TODO: remove this
            Debug.Log("player turn");
            Debug.Log(Data.Health);
        }

        private void OnEndTurn()
        {
            SetIsCardsVisible(false);
            OnFinishedEvent.Invoke(this);
        }

        private void OnCardPlayed(BaseCard card)
        {
            card.gameObject.SetActive(false);
            PositionCards();
        }

        private void PositionCards()
        {
            for (var i = 0; i < Hand.Cards.Length; i++)
            {
                // TODO: refactor
                Hand.Cards[i].transform.position = new Vector3(i * 3f - 7, -2, -2);
            }
        }

        private void SetIsCardsVisible(bool isVisible)
        {
            foreach (var card in Hand.Cards)
            {
                card.gameObject.SetActive(isVisible);
            }
            endTurnButton.gameObject.SetActive(isVisible);
        }
    }
}