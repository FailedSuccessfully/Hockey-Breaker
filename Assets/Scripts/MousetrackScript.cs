using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MousetrackScript : MonoBehaviour
{
    public float speed = 15;
    NavMeshAgent agent;
    private RaycastHit hit;
    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = this.speed;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = false;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (agent.enabled){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity)){
                agent.SetDestination(hit.point);
        }
        }
    }
}
