using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private float speed = 1f;
    private Vector2 sensitivity = new Vector2(3f, 3f);
    private Quaternion cameraRot, characterRot;
    private float minY = -90f;
    private float maxY = 90f;

    private bool cursorLock = true;
    private void Start()
    {
        cameraRot = playerCamera.transform.localRotation;
        characterRot = this.transform.localRotation;
    }

    private void Update()
    {
        if (cursorLock)
        {
            CameraInput();
        }

        UpdateCursorLock();
    }

    private void FixedUpdate()
    {
        MoveInput();
    }

    private void MoveInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        //playerRB.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed + new Vector3(0, playerRB.velocity.y, 0) ;
        Vector3 comFoward = new Vector3(playerCamera.transform.forward.x, 0, playerCamera.transform.forward.z).normalized;
        Vector3 pos = comFoward * z + playerCamera.transform.right * x;
        transform.position += pos * speed * Time.deltaTime;
    }
    Vector3 CalcVelocity()
    {
        float x = Input.GetAxisRaw("Horizontal") * speed;
        float z = Input.GetAxisRaw("Vertical") * speed;

        return new Vector3();
    }
    private void CameraInput()
    {
        float xRot = Input.GetAxis("Mouse X") * this.sensitivity.x;
        float yRot = Input.GetAxis("Mouse Y") * this.sensitivity.y;

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        characterRot *= Quaternion.Euler(0, xRot, 0);

        cameraRot = ClampRotation(cameraRot);

        playerCamera.transform.localRotation = cameraRot;
        this.transform.localRotation = characterRot;
    }

    private void UpdateCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (cursorLock)
            {
                cursorLock = false;
            }
            else
            {
                cursorLock = true;
            }
        }
        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private Quaternion ClampRotation(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;

        float angleY = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;

        angleY = Mathf.Clamp(angleY, minY, maxY);

        q.x = Mathf.Tan(angleY * Mathf.Deg2Rad * 0.5f);

        return q;
    }
}
