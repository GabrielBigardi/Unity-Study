using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public static class ListExtensionMethods
{
    /// <summary>
    /// Grab a random element from a list.
    /// </summary>
    /// <param name="list">The list to grab the element from</param>
    /// <returns>Element of type T from the list</returns>
	public static T RandomElement<T>(this List<T> list) => list[Random.Range(0, list.Count)];

    /// <summary>
    /// Makes a shuffled copy of a list.
    /// </summary>
    /// <param name="list">The list to make a shuffled copy</param>
    /// <returns>List of type T</returns>
	public static List<T> Shuffled<T>(this List<T> list) => list.OrderBy(x => Guid.NewGuid()).ToList();
}
