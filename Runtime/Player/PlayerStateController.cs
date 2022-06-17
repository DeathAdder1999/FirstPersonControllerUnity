using System;
using UnityEngine;

namespace FirstPersonController.Controllers.Player
{
    public class PlayerStateController : MonoBehaviour
    {
        public bool IsAlive { get; set; }
        public bool IsHidden { get; set; }

        void Awake()
        {
            IsAlive = true;
        }
    }
}
