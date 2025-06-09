using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class SpawnController : MonoBehaviour
{
    [Inject] private GameModel model;
    [Inject] private SpawnConfig spawnConfig;
    [Inject] private ObjectPool<AnimalView> pool;

    private const float animalSize = 0.15f;
    private float nextSpawn;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (model.Animals.Count >= spawnConfig.AnimalLimit || Time.time < nextSpawn)
        {
            return;
        }

        nextSpawn = Time.time + spawnConfig.Frequency;

        var prefab = spawnConfig.Animals.GetRandom();
        var animal = pool.PopOrCreate(prefab, x=>x.Type == prefab.Type);
        animal.transform.parent = transform;
        animal.transform.position = mainCamera.GetPointInCameraView(transform.position.y + animalSize);
        animal.transform.rotation = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up);
        animal.Setup(Guid.NewGuid());
        model.Animals.Add(animal);
    }
}
