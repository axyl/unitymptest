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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
	}

    /// <summary>
    /// Fire method if player is firing...
    /// 
    /// Called by Update if the user has pressed the fire button
    /// 
    /// </summary>
    void Fire()
    {
        // Create the new bullet object from the prefab...
        var bullet = (GameObject)Instantiate(
            bulletPrefab,   
            transform.position - transform.forward,
            Quaternion.identity);

        // Give it some velocity away from the player...
        bullet.GetComponent<Rigidbody>().velocity = -transform.forward * 4;

        // Destroy the bullet after two seconds  - What if it's already destroyed?
        Destroy(bullet, 2.0f);
    }
}
