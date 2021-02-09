using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    [SerializeField]
    internal GameObject bricks;
    // Start is called before the first frame update
    void Start()
    {
        
        foreach (BrickCollision brick in bricks.GetComponentsInChildren<BrickCollision>(true)){
            brick.Init();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable() {
        
        foreach (BrickCollision brick in bricks.GetComponentsInChildren<BrickCollision>(true)){
            brick.gameObject.SetActive(true);
        }
    }
}
