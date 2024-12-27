using SG;
using UnityEngine;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    private PlayerManager player;
    public float verticalMovement;
    public float horizontalMovement;
    public float moveAmount;

    [SerializeField] private float walkSpeed = 1.0f;
    [SerializeField] private float runSpeed = 6.0f;
    [SerializeField] private bool isSprint = false;
    public float currentSpeed;
    [SerializeField] private float rotateSpeed = 15.0f;
    private Vector3 moveDirection;

    private Transform cameraT;
    
    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<PlayerManager>();
        
    }

    protected override void Update()
    {
        base.Update();
        if (player.IsOwner) // 내 플레이어면 값을 서버로 보내고 아니면 값을 서버에서 가져온다
        {
            player._characterNetworkManager.animatorVerticalParameter.Value = verticalMovement;
            player._characterNetworkManager.animatorHorizontalParameter.Value = horizontalMovement;
            player._characterNetworkManager.networkMoveAmount.Value = moveAmount;
            player._characterNetworkManager.networkSprintState.Value = isSprint;
        }
        else
        {
            verticalMovement = player._characterNetworkManager.animatorVerticalParameter.Value;
            horizontalMovement = player._characterNetworkManager.animatorHorizontalParameter.Value;
            moveAmount = player._characterNetworkManager.networkMoveAmount.Value;
            isSprint = player._characterNetworkManager.networkSprintState.Value;
            player._playerAnimationManager.UpdateAllAnimation(0, moveAmount * (isSprint ? 2 : 1));
        }
    }
    
    public void HandleAllMovement()
    {
        GroundMovement();
    }

    private void GroundMovement()
    {
        // Get Input
        Vector2 inputVector = InputManager.instance.GetMove();
        isSprint = InputManager.instance.GetSprint();
        Debug.Log(isSprint);
        
        // Calculate base vector by Player Camera
        Vector3 forward = PlayerCamera.instance.cameraObject.transform.forward * inputVector.y;
        Vector3 right = PlayerCamera.instance.cameraObject.transform.right * inputVector.x;
        moveDirection = (forward + right).normalized;
        moveDirection.y = 0;
        
        // Decide current speed
        moveAmount = moveDirection.magnitude;
        currentSpeed = isSprint ? runSpeed : walkSpeed;
        verticalMovement = inputVector.magnitude;
        horizontalMovement = inputVector.magnitude;
        
        // Do Move
        _characterController.Move(moveDirection * (currentSpeed * Time.deltaTime));
        player._playerAnimationManager.UpdateAllAnimation(0, moveAmount * (isSprint ? 2 : 1));
        
        // Do Rotate
        // When input is zero, Do not perform Rotation.
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            Quaternion currentRotation =
                Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            transform.rotation = currentRotation;
        }
    }
}