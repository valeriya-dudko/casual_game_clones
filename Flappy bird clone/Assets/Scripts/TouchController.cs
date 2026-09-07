using UnityEngine;
using UnityEngine.InputSystem;

public class TouchController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction tapAct;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        tapAct = playerInput.actions.FindAction("TouchTap");
    }

    private void OnEnable()
    {
        tapAct.performed += TouchTap;
    }

    private void OnDisable()
    {
        tapAct.performed -= TouchTap;
    }

    void TouchTap(InputAction.CallbackContext context)
    {
        bool isTapped = context.ReadValueAsButton();
        if (GameController.Instance.IsPlaying)
            PlayerController.Instance.Jump();
    }
}
