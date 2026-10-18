using System;
using Encounter.Gear;
using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    [Serializable]
    public class TurnTakerData
    {
        [SerializeField, Min(0)] private int health;
        [SerializeField, Min(0)] private int maxHealth;
        [SerializeField, Min(0)] private int handCapacity;
        [SerializeField, Range(0, 1)] private float agility;

        [SerializeReferenceDropdown] [SerializeReference]
        public IGearItem[] gear;

        public int Health => health;
        public int MaxHealth => maxHealth;
        public int HandCapacity => handCapacity;
        public float Agility => agility;

        private float _readiness;
        public bool IsReady => _readiness >= 0.999;
        
        public readonly UnityEvent OnDeathEvent = new();
        
        public void AlterHealth(int delta)
        {
            if (-delta > Health)
            {
                health = 0;
                OnDeathEvent.Invoke();
                return;
            }

            if (delta > maxHealth - health)
            {
                health = maxHealth;
                return;
            }

            health += delta;
            if (health == 0) OnDeathEvent.Invoke();
        }

        public void GetReadyToAttack()
        {
            _readiness += agility;
            if (_readiness > 1) _readiness = 1;
        }

        public void ResetReadiness()
        {
            _readiness = 0;
        }
    }
}