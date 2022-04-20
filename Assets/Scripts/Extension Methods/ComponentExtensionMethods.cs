using UnityEngine;

public static class ComponentExtensionMethods
{
	/// <summary>
	/// Try to get the component, if hasn't, then add it.
	/// </summary>
	/// <param name="gameObject">The gameobject to grab the component from</param>
	/// <returns>Previously or newly attached component</returns>
	public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component => gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();

	/// <summary>
	/// Checks whether a game object has a component of type T attached.
	/// </summary>
	/// <param name="gameObject">Game object.</param>
	/// <returns>True when component is attached.</returns>
	public static bool HasComponent<T>(this GameObject gameObject) where T : Component => gameObject.GetComponent<T>() != null;
}
