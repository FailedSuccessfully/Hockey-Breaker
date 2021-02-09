using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    public GameManager gm;
    public GameObject puck;
    private Collider trigger, puckCol;
    private Renderer gateRend;
    private Color defaultColor;
    // Start is called before the first frame update
    void Start()
    {
        trigger = this.GetComponentInChildren<Collider>();
        puckCol = puck.GetComponentInChildren<Collider>();
        gateRend = this.GetComponentInChildren<Renderer>();
        defaultColor = gateRend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.Equals(puckCol)){
            gateRend.material.color = Color.red;
        gm.LoseLife();
        }
    }

    private void OnTriggerExit(Collider other) {
        gateRend.material.color = defaultColor;
    }
}
