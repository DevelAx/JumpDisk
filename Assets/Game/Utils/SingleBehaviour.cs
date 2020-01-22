using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingleBehaviour<T> : MyMonoBehaviour
	where T : MonoBehaviour
{
	public static T Instance { get; private set; }

	protected override void Awake()
	{
		base.Awake();
		string parentTypeName = typeof(T).Name;

		if (Instance)
		{
			Destroy(this);
			throw new GameException($"The '{parentTypeName}' is a singleton. It cannot be created more than once!");
		}

		Instance = this as T;
		Debug.Assert(Instance, parentTypeName);
	}
}
