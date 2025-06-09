using System;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
   public Action<AnimalView, AnimalView> OnAnimalEat;
   public Action OnAnimalSpawn;
   
   public List<AnimalView> Animals = new();
   public int DeadPreys;
   public int DeadPredators;
}
