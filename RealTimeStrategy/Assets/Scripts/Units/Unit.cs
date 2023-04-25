using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;
using System;

public class Unit : NetworkBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private UnityEvent onSelected;
    [SerializeField] private Targeter targeter;
    [SerializeField] private UnityEvent onDeselected;
    [SerializeField] private UnitMover unitMover;

    public static event Action<Unit> ServerOnUnitSpawned;
    public static event Action<Unit> ServerOnUnitDespawned;

    public static Action<Unit> AuthorityOnUnitSpawned;
    public static Action<Unit> AuthorityOnUnitDespawned;

    public UnitMover GetUnitMover()
    {
        return unitMover;
    }

    public Targeter GetTargeter()
    {
        return targeter;
    }
    #region Server

    public override void OnStartServer()
    {
        ServerOnUnitSpawned?.Invoke(this);
        health.ServerOnDie += ServerHandleDie;
    }

    public override void OnStopServer()
    {
        ServerOnUnitDespawned?.Invoke(this);
        health.ServerOnDie -= ServerHandleDie;
    }

    [Server]
    private void ServerHandleDie()
    {
        NetworkServer.Destroy(gameObject);
    }

    #endregion
    #region Client


    public override void OnStartAuthority()
    {
        AuthorityOnUnitSpawned?.Invoke(this);
    }

    public override void OnStopClient()
    {
        if (!hasAuthority) { return; }

        AuthorityOnUnitDespawned?.Invoke(this);
    }

    [Client]
    public void Select()
    {
        if (!hasAuthority) { return; }
        onSelected?.Invoke();
    }

    [Client]
    public void Deselect()
    {
        if (!hasAuthority) { return; }
        onDeselected?.Invoke();
    }

    #endregion
}
