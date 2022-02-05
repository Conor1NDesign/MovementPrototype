using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CCPlayerController_3Quarter : MonoBehaviour
{
    private PlayerInput playerInput;

    private string playerDevice;

    public void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerDevice = playerInput.currentControlScheme;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
