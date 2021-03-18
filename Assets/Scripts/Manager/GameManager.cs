using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; } = null;

	private Player player = null;

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

		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}

	public Player GetPlayer()
	{
		return player;
	}
}
