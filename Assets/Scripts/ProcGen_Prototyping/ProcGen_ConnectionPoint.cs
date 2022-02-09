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
    }
}
