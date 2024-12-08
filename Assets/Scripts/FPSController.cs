using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class FPSController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 20f;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;

    public bool canMove = true;

    // Sonidos
    public AudioClip[] footstepSounds; // Array de sonidos de pasos
    private AudioSource audioSource;
    private float stepInterval = 0.5f; // Intervalo entre sonidos de pasos
    private float stepTimer = 0f; // Temporizador para controlar el intervalo

    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Movimiento en X y Z
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;

        moveDirection.x = forward.x * curSpeedX + right.x * curSpeedY;
        moveDirection.z = forward.z * curSpeedX + right.z * curSpeedY;

        // Movimiento en el eje vertical (gravedad y salto)
        if (characterController.isGrounded)
        {
            moveDirection.y = -2f;

            // Manejo del salto
            if (Input.GetButton("Jump") && canMove)
            {
                moveDirection.y = jumpPower;
            }

            // Reproducir sonidos de pasos si el jugador se mueve
            if (moveDirection.x != 0 || moveDirection.z != 0)
            {
                stepTimer += Time.deltaTime;
                if (stepTimer >= stepInterval)
                {
                    PlayFootstepSound();
                    stepTimer = 0f;
                }
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Movimiento del CharacterController
        characterController.Move(moveDirection * Time.deltaTime);

        // Rotación de la cámara (Mouse Look)
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    void PlayFootstepSound()
    {
        if (footstepSounds.Length > 0)
        {
            int index = Random.Range(0, footstepSounds.Length); // Seleccionar un sonido aleatorio
            audioSource.clip = footstepSounds[index];
            audioSource.Play();
        }
    }
}
