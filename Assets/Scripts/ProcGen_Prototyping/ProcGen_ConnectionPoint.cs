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
        Debug.Log("Selected tile " + tileToSpawn + " will be spawned at " + transform.position);

        //Checks each connector on the tile for the first matching connection point
        for (int i = 0; i < tileToSpawn.GetComponent<ProcGen_Tile>().connectionPoints.Count; i++)
        {
            ProcGen_ConnectionPoint connectionPoint = tileToSpawn.GetComponent<ProcGen_Tile>().connectionPoints[i].GetComponent<ProcGen_ConnectionPoint>();

            //Checks if the Connector matches the Type (Horizontal or Vertical) and is opposite to the connection side (Left/Right or Top/Bottom)
            if (connectionPoint.connectionSide != connectionSide && connectionPoint.connectionType == connectionType)
            {
                //Spawns the selected tile in
                Debug.Log("Generating " + tileToSpawn + " at " + transform.position);
                GameObject spawnedTile = Instantiate(tileToSpawn, startTile.transform);

                //Moves the spawaned tile to the position of this connector
                spawnedTile.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);

                Vector3 connectionOffset = new Vector3(connectionPoint.gameObject.transform.position.x, connectionPoint.gameObject.transform.position.y, 0);
                spawnedTile.transform.position -= connectionOffset;
                break;
            }
            else
            {
                Debug.Log(connectionPoint + " was unsuitable for connection");
                if (generationRetryAttempts > 0 && i == tileToSpawn.GetComponent<ProcGen_Tile>().connectionPoints.Count - 1)
                {
                    retryingGeneration = true;
                    generationRetryAttempts--;
                }
                else if (generationRetryAttempts == 0)
                {
                    Debug.Log("Retry attempts for " + gameObject + " have been exhausted, activating blocker instead");
                    blocker.SetActive(true);
                }
            }
        }
        
        if (retryingGeneration)
        {
            Debug.Log("Retrying Tile Selection and Generation for " + gameObject);
            GenerateConnectingTile(startTile);
        }
        else
        {
            Debug.Log("Finished generation task for " + gameObject);
        }
    }
}
