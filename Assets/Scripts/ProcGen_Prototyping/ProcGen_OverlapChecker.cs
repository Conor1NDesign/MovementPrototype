using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcGen_OverlapChecker : MonoBehaviour
{
    [HideInInspector]
    public bool isOverlapping = false;
    [HideInInspector]
    public bool clearGeneration = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "OverlapChecker" && !clearGeneration)
        {
            Debug.Log("Tile Collision Detected!");
            isOverlapping = true;
        }
    }

    //Look, I know this is messy. But it's 1am and the only way I can think of making the generation just wait a fucking second and make sure there's no tile overlapping going on.
    public IEnumerator GenerationDelay(bool overlapping)
    {
        Debug.Log("YES SIR THE COROUTINE IS STARTING");
        yield return new WaitForSeconds(5);
        Debug.Log("WHAT THE FUCK WHY WON'T YOU WORK DICKHEAD");
        if (isOverlapping)
        {
            yield return true;
        }
        else
        {
            yield return false;
        }
    }
}