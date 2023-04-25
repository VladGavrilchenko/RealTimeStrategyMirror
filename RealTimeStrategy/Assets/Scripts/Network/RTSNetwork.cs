using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class RTSNetwork : NetworkManager
{
    [SerializeField] private GameObject unitSpawnerPrefab;
    [SerializeField] private GameOverHandle gameOverHandlePrefab;
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        GameObject unitSpawnerInstance =  Instantiate(unitSpawnerPrefab, conn.identity.transform.position, conn.identity.transform.rotation);
    
        NetworkServer.Spawn(unitSpawnerInstance, conn);
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            base.OnServerSceneChanged(sceneName);
            GameOverHandle gameOverHandleInstance = Instantiate(gameOverHandlePrefab);
            NetworkServer.Spawn(gameOverHandleInstance.gameObject);
        }
    }

}
