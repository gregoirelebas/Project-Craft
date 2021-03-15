using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Utils : MonoBehaviour
{
	private static Utils instance = null;
	public static Utils Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameObject("@Utils").AddComponent<Utils>();
			}

			return instance;
		}
	}

	public Camera MainCamera { get; private set; } = null;

	private void Awake()
	{
		MainCamera = Camera.main;
	}

	public Vector2 GetMouseWorldPosition()
	{
		Vector2 mousePosition = Mouse.current.position.ReadValue();

		return MainCamera.ScreenToWorldPoint(mousePosition);
	}
}
