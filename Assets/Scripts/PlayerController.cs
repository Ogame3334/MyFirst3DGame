using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private float speed = 10f;
    void Start()
    {
        
    }

    void Update()
    {
        ControllInput();
    }

    void ControllInput()
    {
        playerRB.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed;
    }
}
