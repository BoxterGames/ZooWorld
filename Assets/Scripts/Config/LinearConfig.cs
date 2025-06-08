using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "LinearConfig", menuName = "Config/LinearConfig")]
public class LinearConfig : ScriptableObject
{
    public float LinearSpeed = 10;
    
    public float Frequency  => Random.Range(FrequencyMin, FrequencyMax);
    public float FrequencyMin = 0.5f;
    public float FrequencyMax = 1f;
    [FormerlySerializedAs("RotationSpeed")] public float RotationAngle = 15;
}
