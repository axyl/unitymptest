using UnityEngine;
using UnityEngine.Networking;

public class Combat : NetworkBehaviour {

    // Public variables..
    public const int maxHealth = 100;   //const - can't be changed.
    public bool destroyOnDeath;
    [SyncVar]       // Synchronise this value between all clients and server.
    public int health = maxHealth;

    // Public method called by the bullet.
    public void TakeDamage(int amount)
    {
        // only run on the server.
        if (!isServer) return;

        // take away the value of amount from health...
        health -= amount;

        // Are we dead?
        if (health< 0)
        {
            Debug.Log("Dead");
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            } else
            {
                health = maxHealth;
                // This will be invoked on all clients.
                RpcRespawn();
            }
        }
    }

    /// <summary>
    /// Respawns the player presuming that they're dead.  Health has to be set server side, already done.
    /// </summary>
    [ClientRpc] // Always run this method on client, not server.  (All clients.)
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            // Move the player back to the zero location.
            transform.position = Vector3.zero;
        }
    }
    
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
