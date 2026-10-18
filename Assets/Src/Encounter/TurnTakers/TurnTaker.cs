using System;
using Encounter.Card.Factory;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Encounter.TurnTakers
{
    public abstract class TurnTaker : MonoBehaviour
    {
        public TurnTakerData Data { get; private set; }
        protected Hand Hand { get; private set; }
        protected abstract ICardFactory CardFactory { get; }

        public readonly UnityEvent<TurnTaker> OnFinishedEvent = new();
        public readonly UnityEvent<TurnTaker> OnDeathEvent = new();

        public void Init(TurnTakerData data)
        {
            if (Data != null) throw new Exception("Multiple initialization");

            Data = data;
            Hand = new Hand(Data, CardFactory);
            Data.OnDeathEvent.AddListener(OnDeath);

            OnInit();
        }

        public void StartTurn()
        {
            Hand.Form();
            OnStartTurn();
        }

        private void OnDeath()
        {
            Data.OnDeathEvent.RemoveListener(OnDeath);
            OnDeathEvent.Invoke(this);
            OnAfterDeath();
        }

        protected abstract void OnStartTurn();

        protected virtual void OnInit()
        {
        }

        protected virtual void OnAfterDeath()
        {
        }
    }
}