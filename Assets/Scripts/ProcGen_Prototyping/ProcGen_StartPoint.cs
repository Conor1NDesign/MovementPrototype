using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcGen_StartPoint : MonoBehaviour
{
    private Transform spawnPoint;

    [Header("Map Generation Settings")]
    [Tooltip("The number of times the Generation Event will run -- ie. The bigger the number, the bigger the map.")]
    public int generationLoops;
    [Tooltip("The % Chance of a special tile being selected to spawn instead of a standard one.")]
    [Range(0, 100)]
    public float specialTileChance;


    public List<GameObject> startingTiles;

    public List<GameObject> standardTiles;

    //public List<GameObject> specialTiles;

    //[HideInInspector]
    public List<GameObject> connectionPoints;

    private bool firstGenerationLoop = true;

    public void Awake()
    {
        spawnPoint = transform;
        GenerateStartingTile();
    }

    public void GenerateStartingTile()
    {
        int spawnIndex = Random.Range(0, startingTiles.Count);
        GameObject chosenStartTile = startingTiles[spawnIndex].gameObject;
        Instantiate(chosenStartTile, gameObject.transform);
        GameObject startingTile = GameObject.FindGameObjectWithTag("StartingTile");
        
        for (int i = 0; i < startingTile.GetComponent<ProcGen_Tile>().connectionPoints.Count; i++)
        {
            connectionPoints.Add(startingTile.GetComponent<ProcGen_Tile>().connectionPoints[i]);
        }
        TileGenerationLoop();
    }

    public void TileGenerationLoop()
    {
        if (generationLoops > 0)
        {
            Debug.Log("Number of CPs to generate tiles for is: " + connectionPoints.Count);
            for (int i = 0; i < connectionPoints.Count; i++)
            {
                Debug.Log("Generating tile for Connection Point " + connectionPoints[i]);
                connectionPoints[i].GetComponent<ProcGen_ConnectionPoint>().GenerateConnectingTile(gameObject.GetComponent<ProcGen_StartPoint>());
            }
            generationLoops--;

            if (!firstGenerationLoop)
                GenerateCPList();

            firstGenerationLoop = false;
            TileGenerationLoop();
        }
        else if (generationLoops <= 0)
        {
            return;
        }
    }

    public void GenerateCPList()
    {
        Debug.Log("Attempting to generate new list of Connection Points");
        connectionPoints = new List<GameObject>();

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Connector").Length; i++)
        {
            connectionPoints.Add(GameObject.FindGameObjectsWithTag("Connector")[i]);
        }
    }
}
