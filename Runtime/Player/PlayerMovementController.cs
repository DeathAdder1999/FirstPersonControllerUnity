using System;
using System.Collections;
using System.Collections.Generic;
using FirstPersonController.Settings;
using Unity.VisualScripting;
using UnityEngine;

namespace FirstPersonController.Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        const string k_MouseX = "Mouse X";
        const string k_MouseY = "Mouse Y";
        const string k_Horizontal = "Horizontal";
        const string k_Vertical = "Vertical";
        const float k_MaxMouseChange = 9f;

        [SerializeField]
        Transform m_CameraTransform;
        [SerializeField]
        Transform m_GroundCheck;
        [SerializeField]
        LayerMask m_GroundLayer;

        CharacterController m_CharacterController;
        PlayerStateController m_PlayerState;
        PlayerSettings m_Settings;
        float m_RotationX = 0f;
        float m_RotationY = 0f;
        Vector3 m_Velocity;

        void Awake()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_Settings = ScriptableObject.CreateInstance<PlayerSettings>();
            m_PlayerState = GetComponent<PlayerStateController>();
        }

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            HandleRotation();
            HandleMovement();
        }

        float GetDeltaMouse(string mouseAxis)
        {
            return Input.GetAxis(mouseAxis) * m_Settings.MouseSensitivity * Time.deltaTime;
        }

        void HandleRotation()
        {
            var mouseX = GetDeltaMouse(k_MouseX);
            var mouseY = GetDeltaMouse(k_MouseY);

            HandleCameraRotation(mouseX, mouseY);
            HandleBodyRotation(mouseX);
        }

        void HandleBodyRotation(float mouseX)
        {
            if (m_PlayerState.IsHidden)
            {
                return;
            }
            
            transform.Rotate(Vector3.up * mouseX);
        }

        void HandleCameraRotation(float mouseX, float mouseY)
        {
            //TODO fix this bug
            if (Mathf.Abs(mouseY) < k_MaxMouseChange)
            {
                m_RotationX -= mouseY;
                m_RotationX = Math.Clamp(m_RotationX, -90, 90);
            }
            
            if (m_PlayerState.IsHidden && Mathf.Abs(mouseX) < k_MaxMouseChange)
            {
                m_RotationY += mouseX;
                m_RotationY = Math.Clamp(m_RotationY, -90, 90);
            }
            
            m_CameraTransform.localRotation = Quaternion.Euler(m_RotationX, m_RotationY, 0);
        }

        void HandleMovement()
        {
            if (m_PlayerState.IsHidden)
            {
                return;
            }
            
            var horizontalX = Input.GetAxis(k_Horizontal);
            var verticalZ = Input.GetAxis(k_Vertical);
            var speed = m_Settings.WalkSpeed;
            var isGrounded = Physics.CheckSphere(m_GroundCheck.position, m_Settings.GroundDistance, m_GroundLayer);

            if (isGrounded && m_Velocity.y < 0)
            {
                m_Velocity.y = -2f;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = m_Settings.RunSpeed;
            }

            //g * t^2
            m_Velocity.y += -Physics.gravity.magnitude * Time.deltaTime;
            var moveDirection = transform.right * horizontalX + transform.forward * verticalZ + m_Velocity;
            m_CharacterController.Move(moveDirection * speed * Time.deltaTime);
        }
    }
}
