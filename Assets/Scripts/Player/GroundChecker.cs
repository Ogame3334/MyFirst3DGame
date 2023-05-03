using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private bool isGrounded;
    void Start()
    {
        
    }

    void Update()
    {
        Debug.Log(isGrounded);
    }

    public bool GetIsGrounded()
    {
        return this.isGrounded;
    }

    private void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
        Debug.Log("Enter");
    }
    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
        Debug.Log("Exit");
    }
}
