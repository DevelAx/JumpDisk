using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMonoBehaviour : MonoBehaviour
{
	protected GameSettings Settings => Game.Settings;

	protected virtual void Awake()
	{
		SubscribeToEvents();
	}

	protected virtual void OnDestroy()
	{
		UnsubscribeFromEvents();
	}

	protected virtual void SubscribeToEvents() { }

	protected virtual void UnsubscribeFromEvents() { }

	/// <summary>
	/// Called only in Editor in "Edit" mode.
	/// </summary>
	protected virtual void OnEditorValidate() { }

	/// <summary>
	/// Called only in 
	/// </summary>
	protected virtual void OnEditorDrawGizmos() { }

#if UNITY_EDITOR

	private void OnValidate()
	{
		if (Application.isPlaying)
			return;

		OnEditorValidate();
	}

	private void OnDrawGizmos()
	{
		OnEditorDrawGizmos();
	}

#endif
}
