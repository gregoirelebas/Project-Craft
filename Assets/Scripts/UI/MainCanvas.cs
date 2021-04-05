using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
	public static MainCanvas Instance { get; set; } = null;

	[Header("Inventory")]
	[SerializeField] private InventoryDisplay playerDisplay = null;
	[SerializeField] private InventoryDisplay playerChestDisplay = null;
	[SerializeField] private InventoryDisplay chestDisplay = null;
	[SerializeField] private Image selectedIcon = null;

	[SerializeField] private TextMeshProUGUI cursorText = null;

	private Inventory playerInventory = null;
	private RectTransform selectedIconTransform = null;

	private void Awake()
	{
		cursorText.text = "";

		if (Instance == null)
		{
			Instance = this;
		}

		selectedIconTransform = selectedIcon.GetComponent<RectTransform>();

		DisplayPlayerInventory(false);
		HideChestInventory();
		SetSelectedIconSprite(null);
	}

	private void Start()
	{
		playerInventory = GameManager.Instance.GetPlayer().GetInventory();
	}

	private void OnEnable()
	{
		EventManager.Instance.StartListening(EventType.OnCursorEnter, SetTextOnCursor);
		EventManager.Instance.StartListening(EventType.OnCursorExit, SetTextOnCursor);
	}

	private void OnDisable()
	{
		EventManager.Instance.StopListening(EventType.OnCursorEnter, SetTextOnCursor);
		EventManager.Instance.StopListening(EventType.OnCursorExit, SetTextOnCursor);
	}

	/// <summary>
	/// Set the information text near the cursor.
	/// </summary>
	private void SetTextOnCursor(EventParameters parameters)
	{
		if (parameters != null)
		{
			cursorText.text = parameters.@string;
		}
		else
		{
			cursorText.text = "";
		}
	}

	public void SetSelectedIconPosition(Vector2 position)
	{
		if (selectedIcon.isActiveAndEnabled)
		{
			selectedIconTransform.anchoredPosition = position;
		}
	}

	public void SetSelectedIconSprite(Sprite sprite)
	{
		if (sprite == null)
		{
			selectedIcon.gameObject.SetActive(false);
		}
		else
		{
			selectedIcon.gameObject.SetActive(true);
			selectedIcon.sprite = sprite;
		}
	}

	public void DisplayPlayerInventory(bool display)
	{
		playerDisplay.ShowHidePanel(display);

		if (display)
		{
			playerDisplay.SetInventory(playerInventory);
			EventManager.Instance.TriggerEvent(EventType.OnMenuOpened);
		}
		else
		{
			EventManager.Instance.TriggerEvent(EventType.OnMenuClosed);
		}
	}

	public void DisplayChestInventory(Inventory chestInventory)
	{
		playerChestDisplay.ShowHidePanel(true);
		chestDisplay.ShowHidePanel(true);

		playerChestDisplay.SetInventory(playerInventory);
		chestDisplay.SetInventory(chestInventory);

		EventManager.Instance.TriggerEvent(EventType.OnMenuOpened);
	}

	public void HideChestInventory()
	{
		playerChestDisplay.ShowHidePanel(false);
		chestDisplay.ShowHidePanel(false);

		EventManager.Instance.TriggerEvent(EventType.OnMenuClosed);
	}
}
