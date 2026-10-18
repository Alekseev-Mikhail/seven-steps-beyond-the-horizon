using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Encounter
{
    public class EncounterBackground : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        private UnityEvent onBackgroundClick = new();
        
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("pisya");
            onBackgroundClick.Invoke();
        }
    }
}