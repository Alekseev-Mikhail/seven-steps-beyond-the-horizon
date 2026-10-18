using System;
using System.Collections.Generic;
using System.Linq;
using Encounter.Card;
using Encounter.TurnTakers;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Encounter
{
    public class EncounterSelectionManager : MonoBehaviour
    {
        private List<EncounterEnemy> _enemies;
        private EncounterPlayer _player;
        public bool IsSelectionStarted { get; private set; }
        public readonly UnityEvent<TurnTakerData[]> OnSelectedEvent = new();

        public void Init(List<EncounterEnemy> enemies, EncounterPlayer player)
        {
            if (_enemies != null || _player != null) throw new Exception("Multiple initialization");
            _enemies = enemies;
            _player = player;

            foreach (var enemy in _enemies)
            {
                enemy.OnSelectedEvent.AddListener(OnSelected);
            }
        }

        public void StartSelection(TargetSelectionType selectionType)
        {
            if (IsSelectionStarted) return;
            switch (selectionType)
            {
                case TargetSelectionType.EnemyCrewSingle:
                    IsSelectionStarted = true;
                    break;
                case TargetSelectionType.EnemyCrewAll:
                    OnSelectedEvent.Invoke(_enemies.Select(enemy => enemy.Data).ToArray());
                    break;
                case TargetSelectionType.PlayerCrewSingle or TargetSelectionType.PlayerCrewAll:
                    OnSelectedEvent.Invoke(new[] { _player.Data });
                    break;
            }
        }

        public void StopSelection()
        {
            if (!IsSelectionStarted) return;
            IsSelectionStarted = false;
            OnSelectedEvent.Invoke(null);
        }

        private void OnSelected(TurnTakerData data)
        {
            if (!IsSelectionStarted) return;

            IsSelectionStarted = false;
            OnSelectedEvent.Invoke(new[] { data });
        }
    }
}