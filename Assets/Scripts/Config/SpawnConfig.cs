using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnConfig", menuName = "Config/SpawnConfig")]
public class SpawnConfig : ScriptableObject
{
   public AnimalView[] Animals;
   public int AnimalLimit;
   public float Frequency  => Random.Range(FrequencyMin, FrequencyMax);
   public float FrequencyMin = 1f;
   public float FrequencyMax = 2.5f;
}
