using System;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
   public Action<AnimalView> OnAnimalEat;
   public List<AnimalView> Animals = new();
   public List<BoxCollider> Obstacles;
   public int DeadPreys;
   public int DeadPredators;
}
