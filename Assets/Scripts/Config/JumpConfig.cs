using UnityEngine;

[CreateAssetMenu(fileName = "JumpConfig", menuName = "Config/JumpConfig")]
public class JumpConfig : ScriptableObject
{
    public float Force => Random.Range(ForceMin, ForceMax);
    public float Frequency  => Random.Range(FrequencyMin, FrequencyMax);

    public float ForceMin = 50;
    public float ForceMax = 150;
    public float FrequencyMin = 0.5f;
    public float FrequencyMax = 1f;
    public float AngleLimit = 45;
    public float AvoidObstacleDistance = 0.4f;
    public float AvoidObstacleAngle = 40f;
}
