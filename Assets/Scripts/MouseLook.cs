using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
	[SerializeField] private Transform playerBody = null;

	[SerializeField] private float mouseSensivity = 100.0f;

	private float xRotation = 0.0f;

	private void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void Update()
	{
		float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

		playerBody.Rotate(Vector3.up * mouseX);
		transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
	}
}
