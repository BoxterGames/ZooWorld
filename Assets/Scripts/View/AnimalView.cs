using System;
using UnityEngine;
using Zenject;

public class AnimalView : MonoBehaviour
{
    public Guid Id { get; private set; }
    public AnimalType Type => type;
  
    [SerializeField] private AnimalType type;

    private FoodChainController foodChainController;
    
    public void Setup(Guid id)
    {
        Id = id;
        foodChainController ??= ProjectContext.Instance.Container.Resolve<FoodChainController>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Animal"))
        {
            foodChainController.CollideAnimals(this, other.gameObject);
        }
        
        if (other.transform.CompareTag("Obstacle"))
        {
            transform.rotation *= Quaternion.AngleAxis(180, Vector3.up);
        }
    }
}
