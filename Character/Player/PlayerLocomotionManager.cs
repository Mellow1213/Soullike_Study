using SG;
using UnityEngine;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    private PlayerManager player;
    public float verticalMovement;
    public float horizontalMovement;

    private Vector3 moveVec;
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float runSpeed = 6.0f;
    [SerializeField] private bool isRun = false;
    public float currentSpeed;
    [SerializeField] private float rotateSpeed = 15.0f;

    private Transform cameraT;
    
    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<PlayerManager>();
        
    }

    protected override void Update()
    {
        base.Update();
    }
    
    public void HandleAllMovement()
    {
        GroundMovement();
    }

    private void GroundMovement()
    {
        Vector2 inputVector = InputManager.instance.GetMove();
        Vector3 forward = PlayerCamera.instance.cameraObject.transform.forward * inputVector.y;
        Vector3 right = PlayerCamera.instance.cameraObject.transform.right * inputVector.x;
        moveVec = (forward + right).normalized;
        moveVec.y = 0;
        currentSpeed = isRun ? runSpeed : walkSpeed;
        _characterController.Move(moveVec * (currentSpeed * Time.deltaTime));
        if (moveVec != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveVec);
            Quaternion currentRotation =
                Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            transform.rotation = currentRotation;
        }
    }
}
