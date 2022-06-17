using UnityEngine;

namespace FirstPersonController.Interactions
{
    public class DoorInteraction : MonoBehaviour, IInteraction
    {
        [SerializeField]
        GameObject m_DoorClosed;
        [SerializeField]
        GameObject m_DoorOpenToward;
        [SerializeField]
        GameObject m_DoorOpenFrom;

        bool m_IsOpen;

        void Start()
        {
            CloseDoor();
        }

        public void TriggerInteraction(GameObject caller)
        {
            if (m_IsOpen)
            {
                CloseDoor();
                return;
            }

            OpenDoor(caller.transform.forward);
        }

        void OpenDoor(Vector3 direction)
        {
            m_DoorClosed.SetActive(false);
            m_IsOpen = true;

            if (direction.z < 0)
            {
                m_DoorOpenFrom.SetActive(true);
                return;
            }

            m_DoorOpenToward.SetActive(true);
        }

        void CloseDoor()
        {
            m_DoorClosed.SetActive(true);
            m_DoorOpenFrom.SetActive(false);
            m_DoorOpenToward.SetActive(false);

            m_IsOpen = false;
        }
    }
}
