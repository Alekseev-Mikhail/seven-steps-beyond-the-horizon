using System;
using Object = UnityEngine.Object;

namespace Encounter.Card.Factory
{
    public class PlayerCardFactory : ICardFactory
    {
        public static readonly PlayerCardFactory Instance = new();
        private static CardPrefabStorage _storage;
        private static EncounterSelectionManager _selectionManager;

        public static void Init(CardPrefabStorage storage, EncounterSelectionManager selectionManager)
        {
            if (storage == null || selectionManager == null) throw new NullReferenceException("Card prefab storage or encounter selection manager cannot be null");
            _storage = storage;
            _selectionManager = selectionManager;
        }
        
        public BaseCard CreateCard(int index, CardAbilityType abilityType)
        {
            if (_storage == null || _storage.CardPrefabs.Count == 0) throw new Exception("No card prefabs");
            
            _storage.CardPrefabs.TryGetValue(abilityType, out var prefab);
            var obj = Object.Instantiate(prefab);
            var card = obj.AddComponent<PlayerCard>();
            
            obj.name = "Player card " + index;
            obj.SetActive(false);
            card.Init(_selectionManager);
            
            return card;
        }
    }
}