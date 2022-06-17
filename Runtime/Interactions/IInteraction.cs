using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FirstPersonController.Interactions
{
    public interface IInteraction
    {
        void TriggerInteraction(GameObject caller);
    }
}
