using UnityEngine;
using Unity.Netcode;

namespace SG
{
    public class CharacterNetworkManager : NetworkBehaviour
    {
        [Header("Position")] 
        public NetworkVariable<Vector3> networkPosition = new NetworkVariable<Vector3>(Vector3.zero, 
            NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<Quaternion> networkRotation = new NetworkVariable<Quaternion>(Quaternion.identity,
            NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        public Vector3 networkPositionVelocity;
        public float networkPositionSmoothTime = 0.1f;
        public float networkRotationSmoothTime = 0.1f;

        [Header("Animation")] 
        public NetworkVariable<float> animatorHorizontalParameter =
            new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> animatorVerticalParameter =
            new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> networkMoveAmount =
            new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        

        [Header("Action")] 
        public NetworkVariable<bool> networkSprintState = new NetworkVariable<bool>(false,
            NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    }
}