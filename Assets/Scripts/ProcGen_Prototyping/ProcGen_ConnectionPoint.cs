using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcGen_ConnectionPoint : MonoBehaviour
{
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
        Debug.Log("Attempting to generate new tile at " + transform.position);
        
        int listSelectionNumber = Random.Range(0, startTile.standardTiles.Count);

        GameObject tileToSpawnCP = startTile.standardTiles[listSelectionNumber];

        for (int i = 0; i < tileToSpawnCP.GetComponent<ProcGen_Tile>().connectionPoints.Count; i++)
        {
            if (tileToSpawnCP.GetComponent<ProcGen_Tile>().connectionPoints[i].GetComponent<ProcGen_ConnectionPoint>().connectionSide != connectionSide &&
                tileToSpawnCP.GetComponent<ProcGen_Tile>().connectionPoints[i].GetComponent<ProcGen_ConnectionPoint>().connectionType == connectionType)
            {
                //INSTANTIATE THE TILE HERE CONOR :D
            }
        }
    }
}
