using UnityEngine;

namespace FirstPersonController.Settings
{
    [CreateAssetMenu]
    public class PlayerSettings : ScriptableObject
    {
        [SerializeField]
        float m_MouseSensitivity = 100f;
        [SerializeField]
        float m_WalkSpeed = 1f;
        [SerializeField]
        float m_RunSpeed = 5f;
        [SerializeField]
        float m_GroundDistance = 0.4f;

        public float MouseSensitivity => m_MouseSensitivity;
        public float WalkSpeed => m_WalkSpeed;
        public float RunSpeed => m_RunSpeed;
        public float GroundDistance => m_GroundDistance;
    }
}
