using System;
using Encounter.Card;
using Encounter.Card.Factory;
using UnityEngine;

namespace Encounter.TurnTakers
{
    public sealed class EncounterPlayer : TurnTakers.TurnTaker
    {
        protected override ICardFactory CardFactory => PlayerCardFactory.Instance;
        
        private EndTurnButton _endTurnButton;

        private void Awake()
        {
            var obj = GameObject.FindWithTag("EndTurnButton");
            if (obj == null) throw new Exception("End turn button not found");
            
            _endTurnButton = obj.GetComponent<EndTurnButton>();
            if (_endTurnButton == null) throw new Exception("End turn button does not have the component");
        }

        protected override void OnInit()
        {
            _endTurnButton.OnEndTurn.AddListener(OnEndTurn);
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
            OnFinishedEvent.Invoke();
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
                Hand.Cards[i].transform.position = new Vector3(i * 3f - 7, -2, -2);
            }
        }

        private void SetIsCardsVisible(bool isVisible)
        {
            foreach (var card in Hand.Cards)
            {
                card.gameObject.SetActive(isVisible);
            }
            _endTurnButton.gameObject.SetActive(isVisible);
        }
    }
}