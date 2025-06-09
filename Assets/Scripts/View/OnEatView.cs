using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using Zenject;

public class OnEatView : MonoBehaviour
{
    private class AnimatedPhrase
    {
        public AnimalView AnimalView;
        public Transform Phrase;
        public float AnimationTimeEnd;
    }

    [SerializeField] private Transform phrasePrefab;
    [SerializeField] private float offset = 1f;
    [Inject] private GameModel gameModel;
    
    private ObjectPool<Transform> phrasePool = new();
    private List<AnimatedPhrase> phrases = new();
    private Camera mainCamera;
    
    private void Awake()
    {
        mainCamera = Camera.main;
        gameModel.OnAnimalEat += SpawnView;
    }

    private void LateUpdate()
    {
        for (int i = phrases.Count - 1; i >= 0; i--)
        {
            if (Time.time > phrases[i].AnimationTimeEnd)
            {
                phrases[i].Phrase.DOKill();
                phrasePool.Add(phrases[i].Phrase);
                phrases.Remove(phrases[i]);
                continue;
            }

            var phrase = phrases[i].Phrase.transform;
            var worldPos = phrases[i].AnimalView.transform.position;
            phrase.position = mainCamera.WorldToScreenPoint(worldPos) + Vector3.down * offset;
        }
    }

    private void SpawnView(AnimalView winner, AnimalView loser)
    {
        var activePhrase = phrases.FirstOrDefault(x => x.AnimalView == winner);
        activePhrase ??= CreatePhrase(winner);
        activePhrase.Phrase.DOKill();
        activePhrase.AnimationTimeEnd = Time.time + 2f;
        activePhrase.Phrase.DOScale(0, 0.5f)
            .From(1)
            .SetDelay(1.5f)
            .Play();

        var loserPhrase = phrases.FirstOrDefault(x => x.AnimalView == loser);

        if (loserPhrase != null)
        {
            loserPhrase.Phrase.DOKill();
            phrasePool.Add(loserPhrase.Phrase);
            phrases.Remove(loserPhrase);
        }
    }

    private AnimatedPhrase CreatePhrase(AnimalView winner)
    {
        var phrase = new AnimatedPhrase()
        {
            AnimalView = winner,
            Phrase = phrasePool.PopOrCreate(phrasePrefab, transform)
        };
            
        phrases.Add(phrase);
        return phrase;
    }
}
