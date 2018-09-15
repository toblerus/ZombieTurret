using System;
using UnityEngine;
using UniRx;


namespace Enemy
{
    public class Knight : AbstractEnemy
    {
        protected override void Attack()
        {
            Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(x =>
            {
                DoDamage();
                //Debug.Log("DOING DAMAGE");
            }).AddTo(gameObject);
        }
    }
}