using System;
using Encounter.Gear;
using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    [Serializable]
    public class CreatureData
    {
        [SerializeField] private int health;
        [SerializeField] private int maxHealth;
        [SerializeField] private int handCapacity;

        [SerializeReferenceDropdown] [SerializeReference]
        public IGearItem[] gear;

        public int Health => health;
        public int MaxHealth => maxHealth;
        public int HandCapacity => handCapacity;
        
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
    }
}