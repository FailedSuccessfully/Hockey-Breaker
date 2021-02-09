using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ApplyForceOnColission : MonoBehaviour
{

    public GameObject Puck;
    public float force = 3f;
    private Rigidbody _rbpuck, _rbpaddle;
    private NavMeshAgent _nmapaddle;
    private float magnitude;
    private void Awake() {
        _rbpuck = Puck.GetComponent<Rigidbody>();
        _rbpaddle = this.GetComponent<Rigidbody>();
        _nmapaddle = this.GetComponentInParent<NavMeshAgent>();
    }
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.Equals(Puck)){
            Vector3 dir = _nmapaddle.velocity;
            magnitude = Vector3.Magnitude(_nmapaddle.velocity);
            dir.y = 0f;
            _rbpuck.AddForceAtPosition(dir * force * magnitude , other.contacts[0].point, ForceMode.Impulse);
        }
        else{
            Debug.Log(other);
        }
}


}
