using UnityEngine;using UnityEngine.Networking;

public class Combat : NetworkBehaviour {    // Public variables..    public const int maxHealth = 100;   //const - can't be changed.
    [SyncVar]       // Synchronise this value between all clients and server.
    public int health = maxHealth;    // Public method called by the bullet.    public void TakeDamage(int amount)    {        // only run on the server.        if (!isServer) return;        health -= amount;        if (health< 0)        {            health = 0;            Debug.Log("Dead");        }    }        // Use this for initialization	void Start () {		}		// Update is called once per frame	void Update () {		}}