using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static scr_Models;
using static scr_Bullet;


public class scr_CharacterController : MonoBehaviour
{
    // Variables

    private CharacterController characterController;
    private DefaultInput defaultInput;
    private Vector2 input_Movement;
    [HideInInspector]
    public Vector2 input_View;

    private Vector3 newCameraRotation;
    private Vector3 newCharacterRotation;

    [Header("References")]
    public Transform cameraHolder;
    public Transform feetTransform;

    [Header("Settings")]
    public PlayerSettingsModel playerSettings;
    public float viewClampYMin = -70;
    public float viewClampYMax = 80;
    public LayerMask playerMask;
    public LayerMask groundMask;


    [Header("Gravity")]
    public float gravityAmount;
    public float gravityMin;
    private float playerGravity;

    public Vector3 jumpingForce;
    private Vector3 jumpingForceVelocity;

    [Header("Stance")]
    public PlayerStance playerStance;
    public float playerStanceSmoothing;

    public CharacterStance playerStandStance;
    public CharacterStance playerCrouchStance;
    public CharacterStance playerProneStance;

    private float stanceCheckErrorMargin = 0.05f;

    private float cameraHeight;
    private float cameraHeightVelocity;

    private Vector3 stanceCapsuleCenter;
    private Vector3 stanceCapsuleCenterVelocity;

    private float stanceCapsuleHeight;
    private float stanceCapsuleHeightVelocity;

    [HideInInspector]
    public bool isSprinting;

    private Vector3 newMovementSpeed;
    private Vector3 newMovementSpeedVelocity;

    [Header("Weapon")]
    public scr_WeaponController currentWeapon;
    public float weaponAnimationSpeed;
    [HideInInspector]
    public bool isGrounded;
    [HideInInspector]
    public bool isFalling;

    [Header("Aiming In")]
    public bool isAimingIn;

    [Header("Shooting")]
    public float fireRate;

    public GameObject bullet;
    public GameObject bulletSpawn;

    private Transform _bullet;
    public AudioSource m_ShootingSound;
    public ParticleSystem muzzleFlash;

    #region - Awake -

    public void Awake()
    {
        // Set cursor to not be visible
        Cursor.visible = false;
        // Lock cursor in scren 
        Cursor.lockState = CursorLockMode.Locked;

        defaultInput = new DefaultInput();

        defaultInput.Character.Movement.performed += e => input_Movement = e.ReadValue<Vector2>();
        defaultInput.Character.View.performed += e => input_View = e.ReadValue<Vector2>();
        defaultInput.Character.Jump.performed += e => Jump();
        defaultInput.Character.Crouch.performed += e => Crouch();
        defaultInput.Character.Prone.performed += e => Prone();
        defaultInput.Character.Sprint.performed += e => ToggleSprint();
        defaultInput.Character.SprintReleased.performed += e => StopSprint();

        defaultInput.Weapon.Fire2Pressed.performed += e => AimingInPressed();
        defaultInput.Weapon.Fire2Released.performed += e => AimingInReleased();

        defaultInput.Weapon.Fire1Pressed.performed += e => Fire();

        defaultInput.Enable();

        m_ShootingSound = GetComponent<AudioSource>();

        newCameraRotation = cameraHolder.localRotation.eulerAngles;
        newCharacterRotation = transform.localRotation.eulerAngles;

        characterController = GetComponent<CharacterController>();

        cameraHeight = cameraHolder.localPosition.y;

        if (currentWeapon)
        {
            currentWeapon.Initialize(this);
        }
    }

    #endregion

    #region - Update -

    private void Update()
    {
        SetIsGrounded();
        SetIsFalling();
        CalculateView();
        CalculateMovement();
        CalculateJump();
        CalculateStance();
        CalculateAimingIn();
        IsFiring();
    }

    #endregion

    #region - Aiming In -

    private void AimingInPressed()
    {
        isAimingIn = true;
    }

    private void AimingInReleased()
    {
        isAimingIn = false;
    }

    private void CalculateAimingIn()
    {
        // if there is no current weapon, returns nothing
        if (!currentWeapon)
        {
            return;
        }

        currentWeapon.isAimingIn = isAimingIn;
    }

    #endregion

    #region - Fire Weapon -

    // Creates the bullet when function Fire is called.

    public void Fire()
    {
        _bullet = Instantiate(bullet.transform, bulletSpawn.transform.position, Quaternion.identity);
        m_ShootingSound.Play();
        muzzleFlash.Play();
    }

    // Checks to see if player is pressing the fire button. 

    public void IsFiring()
    { 
        defaultInput.Weapon.Fire1Pressed.performed += e => Fire();
    }

    #endregion

    #region - IsFalling / IsGrounded -

    // If anything within groundMask is colliding with isGroundedSphere, player is grounded.

    private void SetIsGrounded()
    {
        isGrounded = Physics.CheckSphere(feetTransform.position, playerSettings.isGroundedRadius, groundMask);
    }

    // If player is not grounded and magnitude is greater than or equal to falling speed, the player is falling.

    private void SetIsFalling()
    {
        isFalling = (!isGrounded && characterController.velocity.magnitude >= playerSettings.isFallingSpeed);
    }

    #endregion

    #region - View / Movement -

    // PLAYER VIEW

    private void CalculateView()
    {

        newCharacterRotation.y += playerSettings.ViewXSensitivity * (playerSettings.ViewXInverted ? -input_View.x : input_View.x) * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(newCharacterRotation);

        newCameraRotation.x += playerSettings.ViewYSensitivity * (playerSettings.ViewYInverted ? input_View.y : -input_View.y) * Time.deltaTime;
        newCameraRotation.x = Mathf.Clamp(newCameraRotation.x, viewClampYMin, viewClampYMax);

        cameraHolder.localRotation = Quaternion.Euler(newCameraRotation);

    }

    // PLAYER MOVEMENT

    private void CalculateMovement()
    {
        // Is the player holding the shift to sprint

        if (input_Movement.y <= 0.2f)
        {
            isSprinting = false;
        }

        var verticalSpeed = playerSettings.WalkingForwardSpeed;
        var horizontalSpeed = playerSettings.WalkingStrafeSpeed;

        if (isSprinting)
        {
            verticalSpeed = playerSettings.RunningForwardSpeed;
            horizontalSpeed = playerSettings.RunningStrafeSpeed;
        }

        // Movement Speed Effectors i.e. crouch, prone, falling

        if (!isGrounded)
        {
            playerSettings.SpeedEffector = playerSettings.FallingSpeedEffector;
        }
        else if (playerStance == PlayerStance.Crouch)
        {
            playerSettings.SpeedEffector = playerSettings.CrouchSpeedEffector;
        }
        else if (playerStance == PlayerStance.Prone)
        {
            playerSettings.SpeedEffector = playerSettings.ProneSpeedEffector;
        }
        else
        {
            playerSettings.SpeedEffector = 1;
        }

        // Determines how fast the walking animation plays relative to the players walking speed.

        weaponAnimationSpeed = characterController.velocity.magnitude / (playerSettings.WalkingForwardSpeed * playerSettings.SpeedEffector);

        if(weaponAnimationSpeed > 1)
        {
            weaponAnimationSpeed = 1;
        }

        // Determines walking speed.

        verticalSpeed *= playerSettings.SpeedEffector;
        horizontalSpeed *= playerSettings.SpeedEffector;

        // Makes movement relative to player position and direction

        newMovementSpeed = Vector3.SmoothDamp(newMovementSpeed, new Vector3(horizontalSpeed * input_Movement.x * Time.deltaTime, 0, verticalSpeed * input_Movement.y * Time.deltaTime), ref newMovementSpeedVelocity, isGrounded ? playerSettings.MovementSmoothing : playerSettings.FallingSmoothing );
        var movementSpeed = transform.TransformDirection(newMovementSpeed);

        // Sets player gravity

        if(playerGravity > gravityMin)
        {
            playerGravity -= gravityAmount * Time.deltaTime;
        }        
        
        if (playerGravity < -0.1f && isGrounded)
        {
            playerGravity = -0.1f;
        }

        // Calculates players position on z axis to determine movement speed if in air

        movementSpeed.y += playerGravity;
        movementSpeed += jumpingForce * Time.deltaTime;

        characterController.Move(movementSpeed);
    }

    #endregion

    #region - Jump -

    // JUMP SMOOTHING

    private void CalculateJump()
    {
        jumpingForce = Vector3.SmoothDamp(jumpingForce, Vector3.zero, ref jumpingForceVelocity, playerSettings.JumpingFalloff);
    }

    // JUMP FEATURE

    private void Jump()
    {
        // If player is prone or NOT grounded, the space will return nothing.

        if (!isGrounded || playerStance == PlayerStance.Prone)
        {
            return;
        }

        // If player is in crouch, playe will NOT jump but stand.

        if (playerStance == PlayerStance.Crouch)
        {
            // If player collides with other game object, player will not stand from crouch or prone.

            if (StanceCheck(playerStandStance.StanceCollider.height))
            {
                return;
            }

            playerStance = PlayerStance.Stand;
            return;
        }

        // Jump

        jumpingForce = Vector3.up * playerSettings.JumpingHeight;
        playerGravity = 0;
        currentWeapon.TriggerJump();
    }

    #endregion

    #region - Stance -

    // STANCE FEATURE

    private void CalculateStance()
    {
        // Calculates which stance the player is currently in for player view.

        var currentStance = playerStandStance;

        if (playerStance == PlayerStance.Crouch)
        {
            currentStance = playerCrouchStance;
        }
        else if (playerStance == PlayerStance.Prone)
        {
            currentStance = playerProneStance;
        }

        cameraHeight = Mathf.SmoothDamp(cameraHolder.localPosition.y, currentStance.CameraHeight, ref cameraHeightVelocity, playerStanceSmoothing);
        cameraHolder.localPosition = new Vector3(cameraHolder.localPosition.x, cameraHeight, cameraHolder.localPosition.z);

        characterController.height = Mathf.SmoothDamp(characterController.height, currentStance.StanceCollider.height, ref stanceCapsuleHeightVelocity, playerStanceSmoothing);
        characterController.center = Vector3.SmoothDamp(characterController.center, currentStance.StanceCollider.center, ref stanceCapsuleCenterVelocity, playerStanceSmoothing);
    }

    // CROUCH FEATURE

    private void Crouch()
    {
        if (playerStance == PlayerStance.Crouch)
        {
            if (StanceCheck(playerStandStance.StanceCollider.height))
            {
                return;
            }

            playerStance = PlayerStance.Stand;
            return;
        }

        if (StanceCheck(playerCrouchStance.StanceCollider.height))
        {
            return;
        }

        playerStance = PlayerStance.Crouch;
    }

    // PRONE FEATURE

    private void Prone()
    {
        playerStance = PlayerStance.Prone;
    }

    // Check to see if stance change will collide with something above. 

    private bool StanceCheck(float stanceCheckHeight)
    {
        var start = new Vector3(feetTransform.position.x, feetTransform.position.y + characterController.radius + stanceCheckErrorMargin, feetTransform.position.z);
        var end = new Vector3(feetTransform.position.x, feetTransform.position.y - characterController.radius - stanceCheckErrorMargin + stanceCheckHeight, feetTransform.position.z);

        return Physics.CheckCapsule(start, end, characterController.radius, playerMask);
    }

    #endregion

    #region - Sprinting -

    // TOGGLE SPRINT

    private void ToggleSprint()
    {
        if (input_Movement.y <= 0.2f)
        {
            isSprinting = false;
            return;
        }

        isSprinting = !isSprinting;
    }

    // STOP SPRINTING

    private void StopSprint()
    {
        if (playerSettings.SprintingHold)
        {
            isSprinting = false;
        }
    }

    #endregion

    #region - Gizmos -

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(feetTransform.position, playerSettings.isGroundedRadius);
    }

    #endregion
}
