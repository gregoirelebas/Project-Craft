using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
	[SerializeField] private Transform playerCamera = null;
	[SerializeField] private float mouseSensivity = 100.0f;

	private Vector2 mouseInput = Vector2.zero;
	private float xRotation = 0.0f;

	private void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void Update()
	{
		mouseInput *= Time.deltaTime * mouseSensivity;

		xRotation -= mouseInput.y;
		xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

		transform.Rotate(Vector3.up * mouseInput.x);
		playerCamera.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
	}

	public void OnLook(InputValue inputValue)
	{
		mouseInput = inputValue.Get<Vector2>();
	}
}
