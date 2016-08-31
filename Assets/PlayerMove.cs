using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour {

    public GameObject bulletPrefab;    // Public...link this via the Unity UI

    // Use this for initialization
    void Start () {
	
	}

    /// <summary>
    /// Executes if the object is local only... (not networked)
    /// </summary>
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

    // Update is called once per frame
    void Update () {

        if (!isLocalPlayer) return;

        var x = Input.GetAxis("Horizontal") * 0.1f;
        var z = Input.GetAxis("Vertical") * 0.1f;

        transform.Translate(x, 0, z);

        // Move the camera to track too.  This can't be the best way!
        //Camera.main.transform.parent = transform;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // The code is running on the client, but this function is executed on the server.
            CmdFire();
        }
        
        
	}

    /// <summary>
    /// Fire method if player is firing... run on the server.
    /// </summary>
    [Command]
    void CmdFire()
    {
        // Running on the server.
        // Create the new bullet object from the prefab...
        var bullet = (GameObject)Instantiate(
            bulletPrefab,   
            transform.position - transform.forward,
            Quaternion.identity);

        // Give it some velocity away from the player...
        bullet.GetComponent<Rigidbody>().velocity = -transform.forward * 4;

        // Spawn the bullet object on all clients.
        NetworkServer.Spawn(bullet);

        // Destroy the bullet after two seconds  - What if it's already destroyed?
        Destroy(bullet, 2.0f);
    }
}
