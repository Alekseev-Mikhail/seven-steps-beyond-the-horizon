using System.Linq;
using Encounter.Card;
using Encounter.Card.Factory;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Encounter.TurnTakers
{
    public class EncounterEnemy : TurnTaker
    {
        protected override ICardFactory CardFactory => EnemyCardFactory.Instance;

        public readonly UnityEvent<TurnTakerData> OnSelectedEvent = new();

        private EncounterSelectionManager _selectionManager;
        private float _startTime;
        private bool _isTurning;
        
        public void SetSelectionManager(EncounterSelectionManager selectionManager)
        {
            _selectionManager = selectionManager;
        }
        
        private void FixedUpdate()
        {
            if (!_isTurning) return;
            if (Time.time - _startTime < 1f) return;

            _isTurning = false;
            _selectionManager.OnSelectedEvent.AddListener(PlayCard);
            _selectionManager.StartSelection(TargetSelectionType.PlayerCrewSingle);
            OnFinishedEvent.Invoke(this);
        }

        private void OnMouseDown()
        {
            OnSelectedEvent.Invoke(Data);
        }
        
        private void PlayCard(TurnTakerData[] data)
        {
            // TODO: remove this
            Debug.Log("ENEMY TURN! end");
            Hand.Cards.First().Play(data);
            _selectionManager.OnSelectedEvent.RemoveListener(PlayCard);
        }
        
        protected override void OnStartTurn()
        {
            _isTurning = true;
            _startTime = Time.time;
        }

        protected override void OnAfterDeath()
        {
            gameObject.SetActive(false);
        }
    }
}