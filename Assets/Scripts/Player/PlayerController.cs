using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private GameObject gc;

    private int score = 0;

    private GroundChecker groundChecker;
    private Ray ray;
    private RaycastHit rayHit;
    private Vector2 sensitivity = new Vector2(3f, 3f);
    private Quaternion cameraRot, characterRot;
    private float minY = -90f;
    private float maxY = 90f;

    private float rayMaxDistance = 90f;

    private bool cursorLock = true;
    private void Start()
    {
        cameraRot = playerCamera.transform.localRotation;
        characterRot = this.transform.localRotation;
        groundChecker = gc.GetComponent<GroundChecker>();
        ray = new Ray(this.transform.position, new Vector3(playerCamera.transform.forward.x, 0, playerCamera.transform.forward.z));
    }

    private void Update()
    {
        ray.origin = this.transform.position;
        ray.direction = playerCamera.transform.forward;
        Debug.DrawRay(ray.origin, ray.direction * rayMaxDistance, Color.green, 0);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out rayHit, rayMaxDistance))
            {
                if (rayHit.collider.CompareTag("Target"))
                {
                    rayHit.collider.gameObject.GetComponent<Target>().Click();
                    float distance = Vector3.Distance(rayHit.transform.position, this.transform.position);
                    if(distance < 30f)
                    {
                        this.score++;
                    }
                    else if(distance < 50f)
                    {
                        this.score += 2;
                    }
                    else if(distance < 70f)
                    {
                        this.score += 3;
                    }
                    else
                    {
                        this.score += 4;
                    }
                }
            }
        }
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
        if (Input.GetAxis("Jump") != 0 & groundChecker.GetIsGrounded())
        {
            playerRB.velocity = new Vector3(playerRB.velocity.x, Input.GetAxis("Jump") * jumpSpeed, playerRB.velocity.z);
        }

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 comFoward = new Vector3(playerCamera.transform.forward.x, 0, playerCamera.transform.forward.z).normalized;
        Vector3 pos = comFoward * z + playerCamera.transform.right * x;
        transform.position += pos * speed * Time.deltaTime;
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
