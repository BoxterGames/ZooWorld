using UnityEngine;

[CreateAssetMenu(fileName = "AnimalConfig", menuName = "Config/AnimalConfig", order = 1)]
public class AnimalConfig : ScriptableObject
{
    public AnimalView[] Animals;
}
