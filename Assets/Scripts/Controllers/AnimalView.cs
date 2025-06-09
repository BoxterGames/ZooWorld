using System;
using UnityEngine;
using Zenject;

public class AnimalView : MonoBehaviour
{
    public Guid Id { get; private set; }
    public AnimalType Type;

    private FoodChainController foodChainController;

    public void Setup(Guid id)
    {
        Id = id;
        foodChainController ??= ProjectContext.Instance.Container.Resolve<FoodChainController>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.transform.CompareTag("Animal"))
        {
            return;
        }
        
        foodChainController.CollideAnimals(this, other.gameObject);
    }
}
