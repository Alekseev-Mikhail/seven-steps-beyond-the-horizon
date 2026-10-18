using System.Collections.Generic;
using UnityEngine;

namespace Encounter.Card
{
    [CreateAssetMenu(fileName = "CardPrefabStorage", menuName = "Custom", order = 0)]
    public class CardPrefabStorage : ScriptableObject
    {
       [SerializeField] public Dictionary<CardAbilityType, GameObject> CardPrefabs;
    }
}