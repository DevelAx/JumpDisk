using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMonoBehaviour : MonoBehaviour
{
	protected GameSettings Settings => Game.Settings;

	protected virtual void Awake()
	{
	}

	protected virtual void OnDestroy()
	{
	}

	/// <summary>
	/// Called only in Editor in "Edit" mode.
	/// </summary>
	protected virtual void OnEditorValidate() { }

#if UNITY_EDITOR

	private void OnValidate()
	{
		if (Application.isPlaying)
			return;

		OnEditorValidate();
	}

#endif
}
