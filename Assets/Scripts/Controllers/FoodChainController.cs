using System.Linq;
using UnityEngine;
using Zenject;

public class FoodChainController
{
    [Inject] private GameModel model;
    [Inject] private ObjectPool<AnimalView> pool;
    
    public void CollideAnimals(AnimalView firstAnimal, GameObject otherAnimal)
    {
        var secondAnimal = model.Animals.FirstOrDefault(x => x.gameObject == otherAnimal);

        if (secondAnimal == null || firstAnimal.Id.CompareTo(secondAnimal.Id) > 0)
        {
            return;
        }

        var firstAnimalType = firstAnimal.Type.GetFoodChainType();
        var secondAnimalType = secondAnimal.Type.GetFoodChainType();

        if (firstAnimalType is FoodChainType.Prey && secondAnimalType is FoodChainType.Prey)
        {
            return;
        }

        if (firstAnimalType is FoodChainType.Predator && secondAnimalType is FoodChainType.Predator)
        {
            var isFirstWin = Random.Range(0f, 1f) < 0.5f;
            var winner = isFirstWin ? firstAnimal : secondAnimal;
            var loser = isFirstWin ? secondAnimal : firstAnimal;
            model.DeadPredators++;
            Eat(winner, loser);
            return;
        }

        var predator = firstAnimalType is FoodChainType.Predator ? firstAnimal : secondAnimal;
        var prey = firstAnimalType is FoodChainType.Predator ? secondAnimal : firstAnimal;
        model.DeadPreys++;
        Eat(predator, prey);
    }

    private void Eat(AnimalView winner, AnimalView loser)
    {
        model.Animals.Remove(loser);
        model.OnAnimalEat?.Invoke(winner);
        pool.Add(loser);
    }
}
