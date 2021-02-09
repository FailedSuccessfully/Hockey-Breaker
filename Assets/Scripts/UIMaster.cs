using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMaster : MonoBehaviour
{
    private static UIMaster me;

    [SerializeField]
    Button next, back;

    public static UIMaster Master() {
        return me;
    }

    private void Awake() {
        me = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(){
        GameManager.Manager().Begin();
        ToggleMenu(false);
    }

    public void ToggleMenu(bool on = true){
         GameObject.FindWithTag("Menu").GetComponent<Canvas>().enabled = on;
    }
    public void ToggleResults(bool on = true){
        GameObject.FindWithTag("Results").GetComponent<Canvas>().enabled = on;
    }

    internal void SetResults(GameStates state){
        GameObject.FindWithTag("Results").GetComponent<Canvas>().GetComponentInChildren<Text>().text = GameManager.Manager().gameState.ToString();
        if (GameManager.Manager().gameState == GameStates.Lose || GameManager.Manager().IsLastLevel()) {
            next.gameObject.SetActive(false);
        }
        else
            next.gameObject.SetActive(true);
    }

    public void ClickNext(){
        GameManager.Manager().NextLevel();
        ToggleResults(false);
    }

    public void BackToMenu(){
        ToggleMenu();
        ToggleResults(false);
    }
}
