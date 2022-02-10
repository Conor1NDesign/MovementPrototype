using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcGen_OverlapChecker : MonoBehaviour
{
    private bool overlappingATile = false;
    private bool clearGeneration = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "OverlapChecker" && !clearGeneration)
        {
            Debug.Log("Tile Collision Detected!");
            overlappingATile = true;
        }
    }

    public bool CheckForOverlap(bool overlapping)
    {
        if (overlappingATile)
            return (true);
        else
        {
            clearGeneration = true;
            Debug.Log("Tile generation cleared");
        }
            return (false);
    }
}