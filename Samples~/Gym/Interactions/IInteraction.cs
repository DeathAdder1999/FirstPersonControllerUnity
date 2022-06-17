using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Horror.Interactions
{
    public interface IInteraction
    {
        void TriggerInteraction(GameObject caller);
    }
}
