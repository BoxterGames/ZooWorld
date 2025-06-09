public static class EnumExtensions
{
    public static FoodChainType GetFoodChainType(this AnimalType type) =>
        type is AnimalType.Frog ? 
            FoodChainType.Prey : 
            FoodChainType.Predator;
}
