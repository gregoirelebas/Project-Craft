using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; } = null;

	private Player player = null;
	private MainCanvas mainCanvas = null;
	private float scaleRatio = 0.0f;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		InitOnSceneLoaded();
		SceneManager.sceneLoaded += InitOnSceneLoaded;

		LockCursor(null);

		EventManager.Instance.StartListening(EventType.OnMenuOpened, UnlockCursor);
		EventManager.Instance.StartListening(EventType.OnMenuClosed, LockCursor);
	}

	/// <summary>
	/// Find reference after a new scene was loaded.
	/// </summary>
	private void InitOnSceneLoaded(Scene scene = new Scene(), LoadSceneMode mode = LoadSceneMode.Single)
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

		mainCanvas = FindObjectOfType<MainCanvas>();

		CanvasScaler scaler = mainCanvas.GetComponent<CanvasScaler>();
		if (scaler.screenMatchMode == CanvasScaler.ScreenMatchMode.MatchWidthOrHeight)
		{
			float scaleX = scaler.referenceResolution.x / Screen.width;
			float scaleY = scaler.referenceResolution.y / Screen.height;

			scaleRatio = scaleX * (1.0f - scaler.matchWidthOrHeight) + scaleY * scaler.matchWidthOrHeight;
		}
	}

	/// <summary>
	/// Lock and hide the mouse cursor.
	/// </summary>
	private void LockCursor(EventParameters parameters)
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	/// <summary>
	/// Inlock and show the mouse cursor.
	/// </summary>
	private void UnlockCursor(EventParameters parameters)
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	/// <summary>
	/// Return the current player in scene.
	/// </summary>
	public Player GetPlayer()
	{
		return player;
	}

	/// <summary>
	/// Return the mouse position with canvas ratio and scale.
	/// </summary>
	public Vector2 GetMousePositionInCanvas(Vector2 mousePosition)
	{
		return mousePosition * scaleRatio;
	}
}
