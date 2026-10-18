using UnityEngine;
using UnityEngine.Events;

public class EndTurnButton : MonoBehaviour
{
    public UnityEvent OnEndTurn;

    private void OnMouseDown()
    {
        OnEndTurn.Invoke();
    }
}