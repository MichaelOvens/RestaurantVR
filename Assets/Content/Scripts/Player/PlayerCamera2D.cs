using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Player
{
    public class PlayerCamera2D : MonoBehaviour
    {
        [Header("Look Rotation")]
        [SerializeField] private float _lookSpeed;
        [SerializeField] private InputAction _lookUp;
        [SerializeField] private InputAction _lookDown;
        [SerializeField] private InputAction _lookLeft;
        [SerializeField] private InputAction _lookRight;

        private void Awake()
        {
            _lookUp.Enable();
            _lookDown.Enable();
            _lookRight.Enable();
            _lookLeft.Enable();
        }

        private void Update()
        {
            if (_lookUp.IsPressed())
            {
                transform.Rotate(Vector3.left * _lookSpeed * Time.deltaTime, Space.Self);
            }
            if (_lookDown.IsPressed())
            {
                transform.Rotate(Vector3.right * _lookSpeed * Time.deltaTime, Space.Self);
            }
            if (_lookRight.IsPressed())
            {
                transform.Rotate(Vector3.up * _lookSpeed * Time.deltaTime, Space.World);
            }
            if (_lookLeft.IsPressed())
            {
                transform.Rotate(Vector3.down * _lookSpeed * Time.deltaTime, Space.World);
            }
        }
    }
}
