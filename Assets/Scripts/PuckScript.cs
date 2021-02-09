using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckScript : MonoBehaviour
{
    public float maxSpeed;
    private Rigidbody rb;
    public float newX, newZ;
    // Start is called before the first frame update
    private void FixedUpdate() {
       rb.velocity = LimitSpeed(rb.velocity);
    }

    private void Awake() {
        rb = this.GetComponent<Rigidbody>();
    }

    private Vector3 LimitSpeed(Vector3 velocityIn){
       newX = Mathf.Clamp(velocityIn.x, -maxSpeed, maxSpeed);
       newZ = Mathf.Clamp(velocityIn.z, -maxSpeed, maxSpeed);
       return new Vector3(newX, velocityIn.y, newZ);
    }

    internal void Zero(){
        this.rb.constraints = RigidbodyConstraints.FreezeRotation;
        this.transform.localPosition = Vector3.zero;
        rb.velocity = Vector3.zero;
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        this.rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }
}
