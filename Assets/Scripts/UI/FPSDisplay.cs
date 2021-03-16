using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
	private TextMeshProUGUI fpsText = null;

	private int frameCount = 0;
	private float totalFPS = 0.0f;

	private void Awake()
	{
		fpsText = GetComponent<TextMeshProUGUI>();

		Application.targetFrameRate = 120;
	}

	private void Update()
	{
		totalFPS += 1 / Time.unscaledDeltaTime;
		frameCount++;

		fpsText.text = (totalFPS / frameCount).ToString("F0");
	}
}
