using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class SpawnController : MonoBehaviour
{
    [Inject] private AnimalModel model;
    [Inject] private AnimalConfig animalConfig;
    [Inject] private SpawnConfig spawnConfig;
    [Inject] private ObjectPool<AnimalView> pool;

    private float nextSpawn;

    private void Update()
    {
        if (model.Animals.Count > spawnConfig.AnimalLimit || Time.time < nextSpawn)
        {
            return;
        }

        nextSpawn = Time.time + spawnConfig.Frequency;

        var prefab = animalConfig.Animals.GetRandom();
        var animal = pool.PopOrCreate(prefab, x=>x.Type == prefab.Type);
        animal.transform.position = transform.position + Vector3.up * 0.15f;
        animal.transform.rotation = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up);
        animal.Setup(Guid.NewGuid());
        model.Animals.Add(animal);
    }
}
