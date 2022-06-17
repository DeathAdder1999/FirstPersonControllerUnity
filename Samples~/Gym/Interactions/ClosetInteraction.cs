using FirstPersonController.Controllers.Player;
using UnityEngine;

namespace FirstPersonController.Interactions
{
    public class ClosetInteraction : MonoBehaviour, IInteraction
    {
        [SerializeField]
        Transform m_HideSpot;
        [SerializeField]
        Transform m_ExitSpot;

        public void TriggerInteraction(GameObject caller)
        {
            var stateController = caller.GetComponent<PlayerStateController>();

            if (stateController.IsHidden)
            {
                Exit(stateController);
                return;
            }
            
            Hide(stateController);
        }

        void Hide(PlayerStateController stateController)
        {
            stateController.IsHidden = true;
            stateController.gameObject.transform.SetPositionAndRotation(m_HideSpot.position, m_HideSpot.rotation);
        }

        void Exit(PlayerStateController stateController)
        {
            stateController.IsHidden = false;
            stateController.gameObject.transform.SetPositionAndRotation(m_ExitSpot.position, m_ExitSpot.rotation);
        }
    }
}
