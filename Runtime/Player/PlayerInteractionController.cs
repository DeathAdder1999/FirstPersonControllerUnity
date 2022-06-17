using System;
using FirstPersonController.Interactions;
using UnityEngine;

namespace FirstPersonController.Controllers.Player
{
    public class PlayerInteractionController : MonoBehaviour
    {
        [SerializeField]
        GameObject m_InteractionText;
        [SerializeField]
        KeyCode m_InteractionKey;

        IInteraction m_CurrentInteraction;
        Action m_InteractionExecution;

        // Start is called before the first frame update
        void Start()
        {
            m_InteractionText.SetActive(false);
        }

        void FixedUpdate()
        {
            m_InteractionExecution?.Invoke();
            m_InteractionExecution = null;
        }

        void Update()
        {
            //TODO fixed update
            if (m_CurrentInteraction != null && Input.GetKeyDown(m_InteractionKey))
            {
                m_InteractionExecution = () => m_CurrentInteraction.TriggerInteraction(gameObject);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            var interaction = GetInteraction(other);

            if (interaction != null)
            {
                m_CurrentInteraction = interaction;
                m_InteractionText.SetActive(true);
            }
        }

        void OnTriggerExit(Collider other)
        {
            m_CurrentInteraction = null;
            m_InteractionText.SetActive(false);
        }

        IInteraction GetInteraction(Collider c)
        {
            return c.gameObject.GetComponent<IInteraction>();
        }
    }
}
