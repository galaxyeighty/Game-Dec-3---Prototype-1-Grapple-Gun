using UnityEngine;
using UnityEngine.InputSystem;

public class GrappleGun : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private LineRenderer lr;
    private Vector3 grapplepoint;
    public LayerMask GrappleMask;
    private InputAction shoot;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        shoot = InputSystem.actions.FindAction("Grapple");
    }

    private void OnGrappleButtonPressed(InputAction.CallbackContext _)
    {
        shoot
    }

    private void HandleGrapple()
    {
        if ()
    }
}
