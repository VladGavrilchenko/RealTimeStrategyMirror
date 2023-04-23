using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Targeter : NetworkBehaviour
{
    private Targetable target;

    public Targetable GetTarget()
    {
        return target;
    }


    [Command]
    public void CmdSetTarget(GameObject targetGameObjcet)
    {
        if (!targetGameObjcet.TryGetComponent<Targetable>(out Targetable newTargetable))
        {
            return;
        }

        target = newTargetable;
    }

    [Server]
    public void ClearTarget()
    {
        target = null;
    }

}
