using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public walkshake cameraShakeScript;

    public Camera playerCamera;
    public float walkSpeed = 15f;
    public float runSpeed = 30f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;

    public AudioSource footstepAudioSource;
    public AudioClip walkClip;
    public AudioClip runClip;
    public float stepInterval = 0.01f;
    private float nextStepTime = 0f;


    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    private bool canMove = true;
    private Vector3 lastPosition;
    private float distanceTraveled = 0f;
    public float shakeDistanceThreshold = 5f;
    public static PlayerController instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        lastPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);


        if (canMove && (curSpeedX != 0 || curSpeedY != 0))
        {
            if (Time.time >= nextStepTime && characterController.velocity.magnitude > 0.1f)
            {
                nextStepTime = Time.time + stepInterval;
                PlayFootstepSound(isRunning);
            }
        }

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.R) && canMove)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;
        }
        else
        {
            characterController.height = defaultHeight;
            walkSpeed = 15f;
            runSpeed = 30f;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        distanceTraveled = Vector3.Distance(lastPosition, transform.position);

        if (cameraShakeScript != null)
        {
            cameraShakeScript.SetSpeed(characterController.velocity.magnitude);
        }

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    void PlayFootstepSound(bool isRunning)
    {
        if (footstepAudioSource != null)
        {
            if (isRunning && runClip != null)
            {
                footstepAudioSource.clip = runClip;
            }
            else if (!isRunning && walkClip != null)
            {
                footstepAudioSource.clip = walkClip;
            }

            if (!footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Play();
            }
        }
    }
}
