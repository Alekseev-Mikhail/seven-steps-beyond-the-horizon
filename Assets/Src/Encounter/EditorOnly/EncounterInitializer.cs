using UnityEngine;
using Utils;

namespace Encounter.EditorOnly
{
    public class EncounterInitializer : MonoBehaviour
    {
        [SerializeField] private CreatureData playerData;
        [SerializeField] private CreatureData[] enemyData;

        public void Init()
        {
            GameManager.PlayerData = playerData;
            GameManager.EncounterEnemies = enemyData;
        }
    }
}