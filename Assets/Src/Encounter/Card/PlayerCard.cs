using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Encounter.Card
{
    public class PlayerCard : BaseCard, IPointerDownHandler
    {
        private EncounterSelectionManager _selectionManager;

        public void Init(EncounterSelectionManager selectionManager)
        {
            if (_selectionManager != null) throw new Exception("Multiple initialization");
            
            _selectionManager = selectionManager;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_selectionManager.IsSelectionStarted) return;
            
            _selectionManager.OnSelectedEvent.AddListener(Play);
            _selectionManager.StartSelection(Ability.SelectionType);
            
            // TODO: remove this
            Debug.Log("pressed");
        }

        public override void Play(CreatureData[] data)
        {
            _selectionManager.OnSelectedEvent.RemoveListener(Play);
            if (data == null) return;
            base.Play(data);
            Ability.Play(data);
        }
    }
}