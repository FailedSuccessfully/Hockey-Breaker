using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BrickCollision : MonoBehaviour
{
    [SerializeField]
    internal Collider puck;

    private void Awake() {
        
    }

    internal void Init(){
        this.puck = GameManager.Manager().puck.GetComponent<Collider>();
    }
    protected virtual void OnCollisionEnter(Collision other) {
        if (other.collider.Equals(puck)){
            DisableMe();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.Equals(puck)){
            DisableMe();
        }
    }

    public virtual void DisableMe(){
        this.gameObject.SetActive(false);

        /*switch(0 Random.Range(0, 1000) % 6){
            case(0):
                GameObject pu =  GameManager.me.GetPowerup();
                pu.transform.parent = this.transform;
                pu.transform.localPosition = Vector3.zero;
                pu.SetActive(true);
                break;
        }*/
    }
    private void OnDisable() {
        GameManager.Manager().CheckBricks();
    }
}
