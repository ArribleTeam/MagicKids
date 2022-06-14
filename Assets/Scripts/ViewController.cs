using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class ViewController : MonoBehaviour
{
    [SerializeField] float scoreTweenDuration = 2f;
    [SerializeField] float scoreScale = 1.25f;
    [SerializeField] float arrowFadeDuration = .5f;

    [Space, Header("Refs")]
    [SerializeField] GameObject star;
    [SerializeField] TMP_Text score1, score2;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] Image leftArrow1, leftArrow2, rightArrow1, rightArrow2, leftGradient, rightGradient;


    int score;
    Tween scoreValueTween, scoreSizeTween, starTweenRotation, leftArrowTween, rightArrowTween;

    private void OnEnable()
    {
        scrollRect.horizontalScrollbar.onValueChanged.AddListener(CheckArrows);
    }
    private void Awake()
    {
        GamePanel.ScoreChanged.AddListener(OnScoreChanged);
    }

    private void OnScoreChanged(int score, bool useAnimation)
    {
        if (useAnimation)
            UpdateScore(score);
        else
        {
            this.score += score;
            score1.text = this.score.ToString();
            score2.text = this.score.ToString();
        }
    }

    private void UpdateScore(int deltaScore)
    {
        scoreValueTween = DOTween.To(() => score, x => score = x, score + deltaScore, scoreTweenDuration)
            .OnUpdate(() =>
            {
                score1.text = score.ToString();
                score2.text = score.ToString();
            }).SetEase(Ease.OutQuad);

        scoreSizeTween = score1.transform.DOScale(scoreScale, scoreTweenDuration / 2f)
        .SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.OutBack);

        starTweenRotation = star.transform
            .DORotate(new Vector3(0, 0, 360),
            scoreTweenDuration,
            RotateMode.FastBeyond360)
            .SetEase(Ease.Linear);
    }
    private void CheckArrows(float value)
    {
        if (value < 0.05f)
        {
            if (leftArrow1.color.a != 0)
                leftArrowTween = DOTween.Sequence(leftArrowTween)
                    .Insert(0, leftArrow1.DOFade(0, arrowFadeDuration))
                    .Insert(0, leftArrow2.DOFade(0, arrowFadeDuration))
                    .Insert(0, leftGradient.DOFade(0, arrowFadeDuration));
        }
        else
        {
            if (leftArrow1.color.a != 1)
                leftArrowTween = DOTween.Sequence(leftArrowTween)
                .Insert(0, leftArrow1.DOFade(1, arrowFadeDuration))
                .Insert(0, leftArrow2.DOFade(1, arrowFadeDuration))
                .Insert(0, leftGradient.DOFade(1, arrowFadeDuration));
        }

        if (value > .95f)
        {
            if (rightArrow1.color.a != 0)
                rightArrowTween = DOTween.Sequence(rightArrowTween)
                    .Insert(0, rightArrow1.DOFade(0, arrowFadeDuration))
                    .Insert(0, rightArrow2.DOFade(0, arrowFadeDuration))
                    .Insert(0, rightGradient.DOFade(0, arrowFadeDuration));
        }
        else
        {
            if (rightArrow1.color.a != 1)
                rightArrowTween = DOTween.Sequence(rightArrowTween)
                .Insert(0, rightArrow1.DOFade(1, arrowFadeDuration))
                .Insert(0, rightArrow2.DOFade(1, arrowFadeDuration))
                .Insert(0, rightGradient.DOFade(1, arrowFadeDuration));
        }
    }

    public void ClearSaves()
    {
        SaveController.ResetScores();
    }
}

