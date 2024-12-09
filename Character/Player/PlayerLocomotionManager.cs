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
        Vector2 inputVector = InputManager.instance.GetMove();
        Vector3 forward = PlayerCamera.instance.transform.forward * inputVector.y;
        Vector3 right = PlayerCamera.instance.transform.right * inputVector.x;
        moveVec = (forward + right).normalized;
        Debug.Log("moveVec = " + moveVec);

        
        // todo - Grounded Movement
        // todo - Jump and Air Movement
    }
}
