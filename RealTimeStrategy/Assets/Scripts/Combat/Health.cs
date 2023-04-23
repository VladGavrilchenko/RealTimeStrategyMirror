using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class Health : NetworkBehaviour
{
    [SerializeField] private int maxHealth;

    [SyncVar]
    private int currentHealth;

    public Action ServerOnDie;


    #region Server

    public override void OnStartServer()
    {
        currentHealth = maxHealth;
    }

    [Server]
    public void DealDamage(int damageAmount)
    {
        if(currentHealth ==0 ) { return; }

        currentHealth = Mathf.Max(currentHealth - damageAmount ,0);

        if(currentHealth != 0 ) { return; }

        ServerOnDie?.Invoke();

        Debug.Log("Die");
    }
    #endregion

    #region Client

    #endregion
}
