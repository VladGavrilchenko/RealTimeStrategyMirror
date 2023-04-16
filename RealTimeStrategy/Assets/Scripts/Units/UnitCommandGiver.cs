using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitCommandGiver : MonoBehaviour
{
    [SerializeField] private UnitSelectionHandler unitSelectionHandler;
    [SerializeField] private LayerMask layerMask;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!Mouse.current.rightButton.wasPressedThisFrame) { return; }

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            return;
        }

        TryMove(hit.point);
    }

    private void TryMove(Vector3 point)
    {
        foreach(Unit unity in unitSelectionHandler.SelectedUnits)
        {
            unity.GetUnitMover().CmdMove(point);
        }
    }
}
