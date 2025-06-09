using System;
using System.Collections.Generic;

public class AnimalModel
{
   public Action<AnimalView> OnAnimalEat;
   public List<AnimalView> Animals = new();
   public int DeadPreys;
   public int DeadPredators;
}
