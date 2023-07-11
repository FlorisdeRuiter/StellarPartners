using MyBox;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    public float2 CurrentInputDirection;
    protected float3 _appliedMoveDirection;
    protected CharacterController _characterController;
    [SerializeField] private InputActionReference _moveReference;

    [Separator("Movement Parameters")]
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _gravityForce;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ApplyMovement();
        HandleMovement();
    }

    public virtual void HandleMovement() { }

    private void OnMove(InputAction.CallbackContext context)
    {
        CurrentInputDirection = context.ReadValue<Vector2>();
    }

    public virtual void ApplyMovement()
    {
        _characterController.Move(_appliedMoveDirection * Time.deltaTime);
    }

    private void OnEnable()
    {
        PlayableCharactersManager.Instance.CurrentActiveCharacterInput.actions[_moveReference.action.name].performed += OnMove;
        PlayableCharactersManager.Instance.CurrentActiveCharacterInput.actions[_moveReference.action.name].canceled += OnMove;
    }

    private void OnDisable()
    {
        PlayableCharactersManager.Instance.CurrentActiveCharacterInput.actions[_moveReference.action.name].performed -= OnMove;
        PlayableCharactersManager.Instance.CurrentActiveCharacterInput.actions[_moveReference.action.name].canceled -= OnMove;
    }
}