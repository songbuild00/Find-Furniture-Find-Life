using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class XRButtonPressHandler : MonoBehaviour
{
    public InputActionReference conditionUIActionReference;
    public InputActionReference shopUIActionReference;

    private void OnEnable()
    {
        conditionUIActionReference.action.Enable();
        conditionUIActionReference.action.performed += OnConditionUIPerformed;

        shopUIActionReference.action.Enable();
        shopUIActionReference.action.performed += OnShopUIPerformed;
    }

    private void OnDisable()
    {
        conditionUIActionReference.action.performed -= OnConditionUIPerformed;
        conditionUIActionReference.action.Disable();
        
        shopUIActionReference.action.performed -= OnShopUIPerformed;
        shopUIActionReference.action.Disable();
    }

    private void OnConditionUIPerformed(InputAction.CallbackContext context)
    {
        GameManager.Instance.ToggleConditionTextUI();
    }

    private void OnShopUIPerformed(InputAction.CallbackContext context)
    {
        GameManager.Instance.ToggleShopUI();
    }
}
