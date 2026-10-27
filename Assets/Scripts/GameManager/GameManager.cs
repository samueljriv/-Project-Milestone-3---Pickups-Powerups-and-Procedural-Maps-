using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<PlayerController> players;
    public GameObject playerControllerPrefab;
    public GameObject aiControllerPrefab;
    public GameObject sniperAIControllerPrefab;
    public GameObject tankPawnPrefab;
    public Transform playerSpawnTransform;

    public void Awake() {
        if (instance == null)
        {
            instance = this;
            playerSpawnTransform = GetComponent<Transform>();
            players = new List<PlayerController>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnPlayer();
        SpawnAI();
        SpawnSniper();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayer()
    {
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        GameObject newPawnObj = Instantiate(tankPawnPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation) as GameObject;
        Controller newController = newPlayerObj.GetComponent<Controller>();
        newPawnObj.name = "Player";
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();
        newController.pawn = newPawn;
    }

    public void SpawnAI()
    {
        GameObject newAIObj = Instantiate(aiControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        GameObject newPawnObj = Instantiate(tankPawnPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation) as GameObject;
        Controller newController = newAIObj.GetComponent<Controller>();
        newPawnObj.name = "Enemy";
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();
        newController.pawn = newPawn;
    }

    public void SpawnSniper()
    {
        GameObject newAIObj = Instantiate(sniperAIControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        GameObject newPawnObj = Instantiate(tankPawnPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation) as GameObject;
        Controller newController = newAIObj.GetComponent<Controller>();
        newPawnObj.name = "Enemy";
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();
        newController.pawn = newPawn;
    }
}
