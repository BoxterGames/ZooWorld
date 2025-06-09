using UnityEngine;

[CreateAssetMenu(fileName = "LinearConfig", menuName = "Config/LinearConfig")]
public class LinearConfig : ScriptableObject
{
    public float LinearSpeed = 10;
    
    public float Frequency  => Random.Range(FrequencyMin, FrequencyMax);
    public float FrequencyMin = 0.5f;
    public float FrequencyMax = 1f;
    public float RotationAngle = 20;
    public float AvoidObstacleDistance = 0.4f;
    public float AvoidObstacleAngle = 40f;
}
