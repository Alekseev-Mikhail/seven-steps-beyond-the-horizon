using System;
using System.Collections.Generic;
using Encounter.Card;
using Encounter.Card.Factory;
using Encounter.EditorOnly;
using Encounter.TurnTakers;
using UnityEngine;

namespace Encounter
{
    public class Encounter : MonoBehaviour
    {
        [SerializeField] private EncounterInitializer encounterInitializer;
        [SerializeField] private CardPrefabStorage cardPrefabStorage;
        [SerializeField] private EncounterSelectionManager selectionManager;
        [SerializeField] private GameObject enemyPrefab;
        private List<TurnTaker> _turnTakers;
        private int _currentTurnTaker;

        private void Awake()
        {
            if (encounterInitializer != null) encounterInitializer.Init();

            if (GameManager.EncounterEnemies == null || GameManager.EncounterEnemies.Length == 0)
                throw new Exception("No enemy in encounter");

            _turnTakers = new List<TurnTaker>(GameManager.EncounterEnemies.Length + 1);

            var playerObj = new GameObject("Player");
            var player = playerObj.AddComponent<EncounterPlayer>();
            player.OnDeathEvent.AddListener(OnDefeat);
            _turnTakers.Add(player);

            var enemies = InitEnemies();

            selectionManager.Init(enemies, player);
            PlayerCardFactory.Init(cardPrefabStorage, selectionManager);

            player.Init(GameManager.PlayerData);
        }

        private EncounterEnemy[] InitEnemies()
        {
            var enemies = new EncounterEnemy[GameManager.EncounterEnemies.Length];
            for (var i = 0; i < GameManager.EncounterEnemies.Length; i++)
            {
                var enemyObj = Instantiate(enemyPrefab);
                var enemy = enemyObj.AddComponent<EncounterEnemy>();

                enemyObj.name = "Enemy " + i;
                enemyObj.transform.position = new Vector3(i * 3f - 7, 2, -2);

                enemy.Init(GameManager.EncounterEnemies[i]);
                enemy.SetSelectionManager(selectionManager);
                enemy.OnDeathEvent.AddListener(OnEnemyDeath);

                enemies[i] = enemy;
                _turnTakers.Add(enemy);
            }

            return enemies;
        }

        private void Start()
        {
            _turnTakers[_currentTurnTaker].OnFinishedEvent.AddListener(OnTurnFinished);
            _turnTakers[_currentTurnTaker].StartTurn();
        }

        private void OnTurnFinished()
        {
            _turnTakers[_currentTurnTaker].OnFinishedEvent.RemoveListener(OnTurnFinished);

            _currentTurnTaker++;
            if (_currentTurnTaker >= _turnTakers.Count) _currentTurnTaker = 0;

            _turnTakers[_currentTurnTaker].OnFinishedEvent.AddListener(OnTurnFinished);
            _turnTakers[_currentTurnTaker].StartTurn();
        }

        private void OnEnemyDeath(TurnTaker turnTaker)
        {
            if (_turnTakers.Count == 2) OnVictory();
            
            var index = _turnTakers.IndexOf(turnTaker);
            if (_currentTurnTaker >= index) _currentTurnTaker--;
            _turnTakers.RemoveAt(index);
        }

        private void OnVictory()
        {
            Debug.Log("Victory");
        }

        private void OnDefeat(TurnTaker turnTaker)
        {
            Debug.Log("Game over");
        }
    }
}