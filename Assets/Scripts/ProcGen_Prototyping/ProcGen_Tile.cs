using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcGen_Tile : MonoBehaviour
{
    public List<GameObject> connectionPoints;

    public GameObject antiOverlapTrigger;

    //[HideInInspector]
    public GameObject whoSpawnedMe;

    public void Awake()
    {
        if (antiOverlapTrigger == null)
        {
            Debug.LogError("The tile named '" + gameObject + "' is missing an anti-overlap trigger, please add one to the prefab before attempting to generate a map.");
        }
    }

    public void RetryGeneration()
    {
        //Disables this tile's overlap trigger to prevent it from instantly triggering the new tile
        antiOverlapTrigger.SetActive(false);

        //Re-attempt tile generation
        GameObject startingTile = GameObject.FindGameObjectWithTag("StartingPoint");
        whoSpawnedMe.GetComponent<ProcGen_ConnectionPoint>().generationRetryAttempts--;
        whoSpawnedMe.GetComponent<ProcGen_ConnectionPoint>().GenerateConnectingTile(startingTile.GetComponent<ProcGen_StartPoint>());

        //I am kill :(
        Destroy(gameObject);
    }
}
