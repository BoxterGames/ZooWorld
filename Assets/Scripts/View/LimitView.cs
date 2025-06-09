using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LimitView : MonoBehaviour
{
    [Inject] private GameModel gameModel;
    [Inject] private SpawnConfig config;

    [SerializeField] private Image fillImage;
    [SerializeField] private TMP_Text counterText;

    private void Awake()
    {
        gameModel.OnAnimalEat += (_,_) => Redraw();
        gameModel.OnAnimalSpawn += Redraw;
    }

    private void OnEnable() => Redraw();

    private void Redraw()
    {
        counterText.text = $"{gameModel.Animals.Count}/{config.AnimalLimit}";
        fillImage.fillAmount = gameModel.Animals.Count / (float)config.AnimalLimit;
    }
}
