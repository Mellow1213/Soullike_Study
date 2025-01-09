using SG;
using UnityEngine;
using Unity.Netcode;

namespace SG
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        private PlayerManager player;
        public float verticalMovement;
        public float horizontalMovement;
        public float moveAmount;

        [SerializeField] private float walkSpeed = 1.0f;
        [SerializeField] private float runSpeed = 6.0f;
        [SerializeField] private bool isSprint = false;
        [SerializeField] private bool isAction = false;
        public float currentSpeed;
        [SerializeField] private float rotateSpeed = 15.0f;
        private Vector3 moveDirection;

        private Transform cameraT;

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }

        void Start()
        {
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
                player._characterNetworkManager.isDoingAction.Value = isAction;
            }
            else
            {
                verticalMovement = player._characterNetworkManager.animatorVerticalParameter.Value;
                horizontalMovement = player._characterNetworkManager.animatorHorizontalParameter.Value;
                moveAmount = player._characterNetworkManager.networkMoveAmount.Value;
                isSprint = player._characterNetworkManager.networkSprintState.Value;
                player._playerAnimationManager.UpdateAllAnimation(0, moveAmount);
                isAction = player._characterNetworkManager.isDoingAction.Value;
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

            // Calculate base vector by Player Camera
            Vector3 forward = PlayerCamera.instance.cameraObject.transform.forward * inputVector.y;
            Vector3 right = PlayerCamera.instance.cameraObject.transform.right * inputVector.x;
            forward.y = 0;
            right.y = 0;
            moveDirection =
                (forward + right)
                .normalized; // y를 0으로 만들고 정규화. 정규화 후 y=0으로 순서가 바뀌면 정규화 때 y값 때문에 moveDirection.magnitude가 이론 값보다 더 작아짐.

            // Decide current speed
            currentSpeed = isSprint ? runSpeed : walkSpeed;
            if (isAction)
                currentSpeed = 0;
            if (moveDirection == Vector3.zero)
                currentSpeed = 0;
            verticalMovement = inputVector.magnitude;
            horizontalMovement = inputVector.magnitude;
            Vector3 finalMoveVector = moveDirection * currentSpeed;
//            Debug.Log("벡터의 길이 = " + finalMoveVector.magnitude);
//            Debug.Log("이동값 = " + currentSpeed);
            moveAmount = currentSpeed;

            // Do Move
            _characterController.Move(finalMoveVector * Time.deltaTime);

            // Do Rotate
            // When input is zero, Do not perform Rotation.
            if (moveDirection != Vector3.zero && !isAction)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                Quaternion currentRotation =
                    Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
                transform.rotation = currentRotation;
            }

            // Update Animation
            player._playerAnimationManager.UpdateAllAnimation(0, moveAmount);
        }

        public void AttemptToTryDodge()
        {
            if (isAction)
                return;
            isAction = true;
            if (InputManager.instance.GetMove() == Vector2.zero)
            {
                player._playerAnimationManager.UpdateRollAnimation("Backflip", true);
            }
            else
            {
                player._playerAnimationManager.UpdateRollAnimation("roll_front", true);
            }
        }
        
        public void StopAction()
        {
            isAction = false;
        }
    }
}