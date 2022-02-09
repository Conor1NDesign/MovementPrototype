using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcGen_Tile : MonoBehaviour
{
    public List<GameObject> connectionPoints;

    public Collider antiOverlapTrigger;

    public void Awake()
    {
        antiOverlapTrigger = GetComponent<BoxCollider>();
    }
}
