using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip step1Clip;
    [SerializeField] private AudioClip step4Clip;
    [SerializeField] private Renderer[] alphaParts;
    [SerializeField] private int alphaTransition1DurationInFrames;
    [SerializeField] private int alphaTransition2DurationInFrames;

    private void Awake()
    {
       ApplyAlpha(0f);
       PlayAlphaTransition1();
    }

    public void PlayStep4Clip()
    {
        audioSource.clip = step4Clip;
        audioSource.Play();
    }
    public void PlayStep1Clip()
    {
        audioSource.clip = step1Clip;
        audioSource.Play();
    }
    private void PlayAlphaTransition1()
    {
        DOTween.To(()=>0f, ApplyAlpha, 1f, alphaTransition1DurationInFrames/30f);
    }
    public void PlayAlphaTransition2()
    {
        DOTween.To(()=>1f, ApplyAlpha, 0f, alphaTransition2DurationInFrames/30f);
    }

    private void ApplyAlpha(float alpha)
    {
        foreach (var mesh in alphaParts)
        {
            Debug.Log("setting alpha");
            mesh.material.SetFloat("_alpha", alpha);
        }
    }
}
