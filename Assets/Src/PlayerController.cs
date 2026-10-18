using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Camera _camera;
    private Rigidbody2D _rigidbody;
    private InputAction _moveAction;
    private InputAction _loadEncounterAction;

    private void Start()
    {
        _camera = transform.GetComponentInChildren<Camera>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _moveAction = InputSystem.actions.FindAction("Move");

        _loadEncounterAction = InputSystem.actions.FindAction("Jump");
    }

    private void Update()
    {
        // direction is already normalized
        var direction = _moveAction.ReadValue<Vector2>();
        _rigidbody.linearVelocity = direction * speed;

        if (!_loadEncounterAction.triggered) return;
        _camera.gameObject.SetActive(false);
        SceneManager.LoadScene("Encounter", LoadSceneMode.Additive);
    }
}