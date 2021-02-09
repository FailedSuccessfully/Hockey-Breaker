using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBrick : BrickCollision
{
    [SerializeField]
    private GameObject ExplosionFX;
    [SerializeField]
    [Range(1f, 1.1f)]
    private float rate = 1.025f;
    [SerializeField]
    [Range(1f, 3f)]
    private float size = 2f;
    private SphereCollider explosion;
    private void Awake() {
        
        explosion = this.GetComponent<SphereCollider>();
    }
    private void Update() {
        if (explosion.enabled){
            if (explosion.radius < size)
                explosion.radius *= rate;
            else
                this.gameObject.SetActive(false);
        }
    }
    protected override void OnCollisionEnter(Collision other)
    {
            Explode();
        //base.OnCollisionEnter(other);
    }

    public override void DisableMe()
    {
        Explode();
        this.ExplosionFX.SetActive(true);
    }

    private void Explode(){
        ExplosionFX.SetActive(true);
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        explosion.enabled = true;
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag.ToLower().Contains("brick")){
            other.GetComponent<BrickCollision>().DisableMe();
        }
    }
}

