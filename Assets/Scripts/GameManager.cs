using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.AI;
using UnityEngine.UI;

internal enum GameStates {Menu, Play, Win, Lose};

public class GameManager : MonoBehaviour
{
    [SerializeField]
    internal LevelScript[] levels;
    [SerializeField]
    internal GameObject player;
    [SerializeField]
    internal GameObject powerupG;
    [SerializeField]
    internal GameObject powerupS;
    internal Collider pCollider;
    [SerializeField]
    public PuckScript puck;

    
    public int maxLives = 3;
    private int lives;
    [SerializeField]
    private int levelIndex = -1;
    public Canvas cvs;
    private Text slives;
    public static GameManager me;
    internal GameStates gameState{
        get;
        private set;
    } = GameStates.Menu;

    

    internal Dictionary<Action, float> powerups;

    public static GameManager Manager() {
        return me;
    }
    private void Awake() {
        me = this;
        powerups = new Dictionary<Action, float>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        pCollider = player.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (KeyValuePair<Action, float> timer in powerups.ToArray()){
            powerups[timer.Key] -= Time.deltaTime;
            Debug.Log($"{timer.Key.ToString()}, {timer.Value}");
        }


        foreach(var a in powerups.Where((x, y) => {
            return x.Value <= 0f;
        }).ToArray()){
            a.Key.Invoke();
            powerups.Remove(a.Key);
        }
    }

    private void LateUpdate() {
        if (gameState != GameStates.Menu)
            CheckState();
    }

    internal void LoseLife(){
        this.lives--;
        slives.text = this.lives.ToString();
        ResetPoisition();

        if (lives <= 0){
            gameState = GameStates.Lose;
            puck.gameObject.SetActive(false);
        }
    }

    internal void ResetPoisition(){
        player.GetComponent<NavMeshAgent>().enabled = false;
        player.transform.localPosition = Vector3.zero;
        puck.Zero();
        powerups.Add(() => player.GetComponent<NavMeshAgent>().enabled = true, 0.5f);
    }

    internal GameObject GetPowerup(){
        if (UnityEngine.Random.Range(0f, 100f) % 2f == 0f){
            return powerupG;
        }
        return powerupS;
    }

    public void Begin(){
        foreach (LevelScript level in levels){
            level.gameObject.SetActive(false);
        }
        gameState = GameStates.Play;
        lives = maxLives;
        slives = GameObject.FindGameObjectWithTag("lives").GetComponent<Text>();
        slives.text = lives.ToString();
        levelIndex = 0;
        LoadLevel();
    }

    public bool IsLastLevel(){
        return levelIndex >= levels.Count() - 1;
    }

    private void CheckState(){
        switch (gameState) {

            case (GameStates.Lose):
                Lose();
                break;

            case (GameStates.Win):
                Win();
                break;
                
            default:
                break;
        }
    }

    private void Lose(){
        UIMaster.Master().SetResults(gameState);
        gameState = GameStates.Menu;
        UIMaster.Master().ToggleResults();
    }

    private void Win(){
        UIMaster.Master().SetResults(gameState);
        gameState = GameStates.Menu;
        UIMaster.Master().ToggleResults();

    }

    public void CheckBricks(){
        if (levels[levelIndex].bricks.GetComponentInChildren<Rigidbody>(false) == null){
            gameState = GameStates.Win;
            puck.gameObject.SetActive(false);
        }
    }

    private void LoadLevel(){
        foreach( LevelScript level in levels.Where((l) => l.enabled))
            level.gameObject.SetActive(false);
        levels[levelIndex].gameObject.SetActive(true);
        puck.gameObject.SetActive(true);
        puck.Zero();
        player.GetComponent<NavMeshAgent>().enabled = true;
    }

    public void NextLevel(){
        levelIndex++;
        if (levelIndex < levels.Count()){
            LoadLevel();
        }
        else
            gameState = GameStates.Win;
        
    }
}