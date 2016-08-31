using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Event for when this object collides with another object.
    void OnCollisionEnter(Collision collision)
    {
        // Get the object that we hit.
        var hit = collision.gameObject;
        
        // Get the PlayerMove component from the object...if it's a player....otherwise it will be null
        var hitPlayer = hit.GetComponent<PlayerMove>();
        // If it's not null....
        if (hitPlayer!= null)
        {
            var combat = hit.GetComponent<Combat>();
            // Call the player object's combat object's takeDamage to .... a duh
            combat.TakeDamage(10);

            // Delete the bullet.  This will be destroy bullets on each client, because they were spawned by the network.
            Destroy(gameObject);
        }


    }
}
