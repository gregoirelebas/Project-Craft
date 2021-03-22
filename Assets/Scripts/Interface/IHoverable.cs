using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoverable
{
	/// <summary>
	/// Call when player cursor enter gameobject.
	/// </summary>
	void OnCursorEnter();

	/// <summary>
	/// Call when player cursor exit gameobject.
	/// </summary>
	void OnCursorExit();
}
