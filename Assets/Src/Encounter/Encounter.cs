using System;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private EncounterPlayer player;

        private List<EncounterEnemy> _enemies;
        private List<TurnTaker> _playerCrew;

        private void Awake()
        {
            if (encounterInitializer != null) encounterInitializer.Init();

            if (GameManager.EncounterEnemies == null || GameManager.EncounterEnemies.Length == 0)
                throw new Exception("No enemy in encounter");

            _enemies = new List<EncounterEnemy>(GameManager.EncounterEnemies.Length);
            _playerCrew = new List<TurnTaker>(1);

            InitEnemies();

            selectionManager.Init(_enemies, player);
            PlayerCardFactory.Init(cardPrefabStorage, selectionManager);

            player.Init(GameManager.PlayerData);
            player.OnDeathEvent.AddListener(OnPlayerCrewMemberDeath);
            _playerCrew.Add(player);
        }

        private void InitEnemies()
        {
            for (var i = 0; i < GameManager.EncounterEnemies.Length; i++)
            {
                var enemyObj = Instantiate(enemyPrefab);
                var enemy = enemyObj.AddComponent<EncounterEnemy>();

                enemyObj.name = "Enemy " + i;
                // TODO: refactor
                enemyObj.transform.position = new Vector3(i * 3f - 7, 2, -2);

                enemy.Init(GameManager.EncounterEnemies[i]);
                enemy.SetTurnTakers(_enemies, _playerCrew);
                enemy.OnDeathEvent.AddListener(OnEnemyDeath);

                _enemies.Add(enemy);
            }
        }

        private void Start()
        {
            IncreaseReadinessUntilReady();
        }

        private void IncreaseReadinessUntilReady()
        {
            while (true)
            {
                _playerCrew.ForEach(turnTaker => turnTaker.Data.GetReadyToAttack());
                _enemies.ForEach(turnTaker => turnTaker.Data.GetReadyToAttack());

                var turnTaker = FindReadyTurnTaker();
                if (turnTaker == null) continue;
                StartTurn(turnTaker);
                break;
            }
        }

        private TurnTaker FindReadyTurnTaker()
        {
            foreach (var turnTaker in _playerCrew.Where(turnTaker => turnTaker.Data.IsReady))
            {
                return turnTaker;
            }

            return _enemies.FirstOrDefault(turnTaker => turnTaker.Data.IsReady);
        }

        private void OnTurnFinished(TurnTaker previousTurnTaker)
        {
            previousTurnTaker.OnFinishedEvent.RemoveListener(OnTurnFinished);
            
            var turnTaker = FindReadyTurnTaker();
            if (turnTaker == null)
            {
                IncreaseReadinessUntilReady();
                return;
            }
            StartTurn(turnTaker);
        }

        private void StartTurn(TurnTaker turnTaker)
        {
            turnTaker.Data.ResetReadiness();
            turnTaker.OnFinishedEvent.AddListener(OnTurnFinished);
            turnTaker.StartTurn();
        }
        
        private void OnEnemyDeath(TurnTaker turnTaker)
        {
            if (turnTaker is not EncounterEnemy enemy) return;
            _enemies.Remove(enemy);
            if (_enemies.Count == 0) CompleteEncounter(true);
        }
        
        private void OnPlayerCrewMemberDeath(TurnTaker member)
        {
            _playerCrew.Remove(member);
            if (_playerCrew.Count == 0) CompleteEncounter(false);
        }
        
        private void CompleteEncounter(bool didPlayerWin)
        {
            if (didPlayerWin)
            {
                Debug.Log("Victory");
                return;
            }
            Debug.Log("Game over");
        }
    }
}