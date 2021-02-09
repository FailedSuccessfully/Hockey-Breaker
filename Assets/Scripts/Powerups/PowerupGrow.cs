using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerupGrow : MonoBehaviour
{
    public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        this.Zero();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        if (other.Equals(gm.pCollider)){
            OnPickup(gm.player);
            this.enabled = false;
        }
    }

    void OnPickup(GameObject player){
        player.transform.localScale *= 1.5f;
        gm.powerups.Add(() => player.transform.localScale /= 1.5f, 10f);
        this.gameObject.SetActive(false);
    }

    private void OnDisable() {
        this.transform.parent = null;
    }
    private void Zero(){
        this.transform.position = Vector3.one * 100f;
    }
}