using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcGen_ConnectionPoint : MonoBehaviour
{
    public int generationRetryAttempts;
    private bool retryingGeneration = false;

    public enum ConnectionType
    {
        Horizontal,
        Vertical
    };

    public enum ConnectionSide
    {
        Left,
        Right,
        Top,
        Bottom
    };

    public ConnectionType connectionType;
    public ConnectionSide connectionSide;

    public GameObject blocker;

    public void GenerateConnectingTile(ProcGen_StartPoint startTile)
    {
        retryingGeneration = false;

        //Debug.Log("Attempting to generate new tile at " + transform.position);
        
        //Chooses a random prefab from the standard tile list
        int listSelectionNumber = Random.Range(0, startTile.standardTiles.Count);
        GameObject tileToSpawn = startTile.standardTiles[listSelectionNumber];
        //Debug.Log("Selected tile " + tileToSpawn + " will be spawned at " + transform.position);

        //Spawns the selected tile in
        //Debug.Log("Generating " + tileToSpawn + " at " + transform.position);
        GameObject spawnedTile = Instantiate(tileToSpawn, startTile.transform);


        //Checks each connector on the tile for the first matching connection point
        for (int i = 0; i < spawnedTile.GetComponent<ProcGen_Tile>().connectionPoints.Count; i++)
        {
            //Views the potential connection point within a variable.
            ProcGen_ConnectionPoint connectionPoint = spawnedTile.GetComponent<ProcGen_Tile>().connectionPoints[i].GetComponent<ProcGen_ConnectionPoint>();

            //Checks if the Connector matches the Type (Horizontal or Vertical) and is opposite to the connection side (Left/Right or Top/Bottom)
            if (connectionPoint.connectionSide != connectionSide && connectionPoint.connectionType == connectionType)
            {
                spawnedTile.GetComponent<ProcGen_Tile>().whoSpawnedMe = gameObject;

                //Moves the spawaned tile to the position of this connector
                spawnedTile.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);

                //Offsets the tile based on the tile's compatible connector -- This should mean that the two connection points share the same transform position.
                Vector3 connectionOffset = new Vector3(connectionPoint.gameObject.transform.localPosition.x, connectionPoint.gameObject.transform.localPosition.y, 0);
                spawnedTile.transform.position -= connectionOffset;

                //Calls the OverlapChecker object on the spawned tile to confirm that the tile isn't overlapping with another already established one
                bool isOverlapping = false;
                StartCoroutine(spawnedTile.GetComponent<ProcGen_Tile>().antiOverlapTrigger.GetComponent<ProcGen_OverlapChecker>().GenerationDelay(isOverlapping));

                //Completes the generation of the tile, provided the overlap check returned false
                if (!isOverlapping)
                {
                    //Adds the spawned tile to the list of active tiles in the scene, this is the last step in confirming this tile's position.
                    startTile.activeTiles.Add(spawnedTile);

                    Destroy(connectionPoint.gameObject);
                    Destroy(gameObject);
                    break;
                }
                //If there was an overlap detected, calls for the generation to be retried (provided the connector hasn't expended it's retry capacity already)
                else
                {
                    spawnedTile.GetComponent<ProcGen_Tile>().RetryGeneration();
                }
            }
            else
            {
                //Debug.Log(connectionPoint + " was unsuitable for connection");
                if (generationRetryAttempts > 0 && i == spawnedTile.GetComponent<ProcGen_Tile>().connectionPoints.Count - 1)
                {
                    Debug.Log("Destroying " + spawnedTile);
                    Destroy(spawnedTile);
                    retryingGeneration = true;
                }
                else if (generationRetryAttempts <= 0)
                {
                    Debug.Log("Retry attempts for " + gameObject + " have been exhausted, activating blocker instead");
                    Destroy(spawnedTile);
                    blocker.SetActive(true);
                    Destroy(gameObject);
                }
            }
        }
        
        if (retryingGeneration)
        {
            //Debug.Log("Retrying Tile Selection and Generation for " + gameObject);
            generationRetryAttempts--;
            GenerateConnectingTile(startTile);
        }
        else
        {
            //Debug.Log("Finished generation task for " + gameObject);
        }
    }
}
