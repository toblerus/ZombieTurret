using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraEffects : MonoBehaviour
{
    public ShaderEffect_BleedingColors EffectBleedingColors;
    // Use this for initialization

    private float rnd2;

    void Start() { 
    //{
    //    Observable.Interval(TimeSpan.FromSeconds(rnd2)).Subscribe(x =>
    //    {
    //        var rnd = Random.Range(0.3f, 0.8f);

    //        rnd2 = Random.Range(1, 2f);

    //        EffectBleedingColors.shift = rnd;


    //    }).AddTo(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }
}