using System.Collections.Generic;
using System.Linq;
using Encounter.Card.Factory;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Encounter.TurnTakers
{
    public class EncounterEnemy : TurnTaker
    {
        public readonly UnityEvent<TurnTakerData> OnSelectedEvent = new();
        protected override ICardFactory CardFactory => EnemyCardFactory.Instance;
        private List<EncounterEnemy> _enemies;
        private List<TurnTaker> _playerCrew;

        private float _startTime;
        private bool _isItsTurn;

        public void SetTurnTakers(List<EncounterEnemy> enemies, List<TurnTaker> playerCrew)
        {
            _enemies = enemies;
            _playerCrew = playerCrew;
        }
        
        private void FixedUpdate()
        {
            if (!_isItsTurn) return;
            if (Time.time - _startTime < 1f) return;

            // TODO: remove this
            Debug.Log("ENEMY TURN! end");
            Hand.Cards.First().Play(new[] { _playerCrew.First().Data });

            _isItsTurn = false;
            OnFinishedEvent.Invoke(this);
        }

        private void OnMouseDown()
        {
            OnSelectedEvent.Invoke(Data);
        }

        protected override void OnStartTurn()
        {
            _isItsTurn = true;
            _startTime = Time.time;
        }

        protected override void OnAfterDeath()
        {
            gameObject.SetActive(false);
        }
    }
}