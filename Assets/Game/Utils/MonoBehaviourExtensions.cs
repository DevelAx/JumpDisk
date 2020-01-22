using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonoBehaviourExtensions
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>() ?? go.AddComponent<T>();
        Debug.Assert(component, go.name);
        return component;
    }

    public static T GetOrAddComponent<T>(this Component component)
        where T : Component
    {
        return component.gameObject.GetOrAddComponent<T>();
    }

    public static T RequireComponent<T>(this GameObject go) where T : UnityEngine.Object
    {
        T component = go.GetComponent<T>();
        Debug.Assert(component);
        return component;
    }

    public static T RequireComponentInChildren<T>(this GameObject go) where T : UnityEngine.Object
    {
        T component = go.GetComponentInChildren<T>();
        Debug.Assert(component);
        return component;
    }

    public static T[] RequireComponentsInChildren<T>(this GameObject go) where T : UnityEngine.Object
    {
        T[] components = go.GetComponentsInChildren<T>();
        Debug.Assert(components.Length > 0);
        return components;
    }

    public static T RequireComponentInParent<T>(this GameObject go) where T : class
    {
        T component = go.GetComponentInParent<T>();
        Debug.Assert(component != null, go.name);
        return component;
    }

    public static T RequireComponent<T>(this MonoBehaviour mono) where T : class
    {
        T component = mono.GetComponent<T>();
        Debug.Assert(component != null, mono.name);
        return component;
    }

    public static T[] RequireComponents<T>(this MonoBehaviour mono, int componentsNumber = 0) where T : UnityEngine.Object
    {
        T[] components = mono.GetComponents<T>();
        Debug.AssertFormat(components.Length > 0, "No components of the '{0}' type are found!", typeof(T));
        Debug.AssertFormat(componentsNumber == 0 || components.Length == componentsNumber, "{0} components of the '{1}' type are found, but {2} required!",
            components.Length, typeof(T), componentsNumber);
        return components;
    }

    public static T[] RequireComponentsInChildren<T>(this MonoBehaviour mono, int componentsNumber = 0) where T : UnityEngine.Object
    {
        T[] components = mono.GetComponentsInChildren<T>();
        Debug.AssertFormat(components.Length > 0, "No components of the '{0}' type are found!", typeof(T));
        Debug.AssertFormat(componentsNumber == 0 || components.Length == componentsNumber, "{0} components of the '{1}' type are found, but {2} required!",
            components.Length, typeof(T), componentsNumber);
        return components;
    }

    public static T RequireComponentInChildren<T>(this MonoBehaviour mono) where T : UnityEngine.Object
    {
        T component = mono.GetComponentInChildren<T>();
        Debug.Assert(component, typeof(T).Name);
        return component;
    }

    public static T RequireComponentInParent<T>(this MonoBehaviour mono) where T : class
    {
        T component = mono.GetComponentInParent<T>();
        Debug.Assert(component != null, typeof(T).Name);
        return component;
    }

    #region Invoke

    public static void Invoke(this MonoBehaviour mono, float time, Action action)
    {
        mono.StartCoroutine(ExecuteAfterTime(action, time));
    }

    private static IEnumerator ExecuteAfterTime(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }

    #endregion Invoke

    #region Object Size

    public static Bounds Bounds(this GameObject obj)
    {
        Renderer renderer = obj.RequireComponent<Renderer>();
        return renderer.bounds;
    }

    #endregion
}
