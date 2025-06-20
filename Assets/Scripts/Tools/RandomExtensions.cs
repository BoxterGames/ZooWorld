using UnityEngine;

public static class RandomExtensions
{
    public static T GetRandom<T>(this T[] array)
    {
        return array.Length == 0 ? default : array[Random.Range(0, array.Length)];
    }
}
