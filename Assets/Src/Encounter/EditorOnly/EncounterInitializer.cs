using UnityEngine;
using Utils;

namespace Encounter.EditorOnly
{
    public class EncounterInitializer : MonoBehaviour
    {
        [SerializeField] private TurnTakerData playerData;
        [SerializeField] private TurnTakerData[] enemyData;

        public void Init()
        {
            GameManager.PlayerData = playerData;
            GameManager.EncounterEnemies = enemyData;
        }
    }
}