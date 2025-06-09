using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TriInspector;
using UnityEngine;
using Zenject;

public class ObstacleController : MonoBehaviour
{
   [SerializeField] private List<BoxCollider> obstacleColliders;
   [Inject] private GameModel model;

   private void Awake()
   {
      model.Obstacles = obstacleColliders;
   }

   [Button]
   private void CollectColliders()
   {
      obstacleColliders = GetComponentsInChildren<BoxCollider>().ToList();
   }
}
