using Enemy;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UniRx;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    public int MaxHealth;
    public int BaseHealth = 100;

    public int Health;

    //public int Cash;
    public ReactiveProperty<int> CashReactive;
    public int HealthUpgradeCost = 20;
    public int HealthPerUpgrade = 20;
    public int NumberOfHealthUpgrades = 0;

    public int BaseDamage = 1;
    public int Damage = 1;
    public int DamagePerUpgrade = 1;
    public int NumberOfDamageUpgrades = 0;
    public int DamageUpgradeCost = 20;

    public int TurretLevel = 1;
    public int MaxTurretLevel = 3;
    public int TurretUpgradeCost = 100;

    public int HealCost = 30;

    public int StartingCash = 100;

    public ReactiveProperty<int> HealthUpgradeCostReactive;
    public ReactiveProperty<int> NumberOfHealthUpgradesReactive;
    public ReactiveProperty<int> DamageUpgradeCostReactive;
    public ReactiveProperty<int> NumberOfDamageUpgradesReactive;
    public ReactiveProperty<int> TurretUpgradeCostReactive;
    public ReactiveProperty<int> TurretLevelReactive;


    public int GetHealth()
    {
        return Health;
    }

    public int GetCash()
    {
        return CashReactive.Value;
    }

    void Awake()
    {
        HealthUpgradeCostReactive = new ReactiveProperty<int>(HealthUpgradeCost);
        NumberOfHealthUpgradesReactive = new ReactiveProperty<int>(NumberOfHealthUpgrades);
        DamageUpgradeCostReactive = new ReactiveProperty<int>(DamageUpgradeCost);
        NumberOfDamageUpgradesReactive = new ReactiveProperty<int>(NumberOfDamageUpgrades);
        TurretUpgradeCostReactive = new ReactiveProperty<int>(TurretUpgradeCost);
        TurretLevelReactive = new ReactiveProperty<int>(TurretLevel);

        CashReactive = new ReactiveProperty<int>(StartingCash);
        MaxHealth = BaseHealth;
        Health = MaxHealth;

        Damage = BaseDamage;
        MessageBroker.Default.Receive<PlayerLifeUpdatedEvent>().Subscribe(evt => { Health = evt.Life; })
            .AddTo(gameObject);
        MessageBroker.Default.Receive<EnemyDiedEvent>().Subscribe(evt => { CashReactive.Value += evt.Gold; })
            .AddTo(gameObject);
    }

    public void OnUpgradeHealth()
    {
        NumberOfHealthUpgrades++;
        CashReactive.Value -= HealthUpgradeCost * NumberOfHealthUpgrades;
        MaxHealth = BaseHealth + (HealthPerUpgrade * NumberOfHealthUpgrades);
        Health = MaxHealth;
        HealthUpgradeCostReactive.Value = CurrentHealthUpgradeCost();
        NumberOfHealthUpgradesReactive.Value = NumberOfHealthUpgrades;
    }

    public void OnUpgradeDamage()
    {
        NumberOfDamageUpgrades++;
        CashReactive.Value -= DamageUpgradeCost * NumberOfDamageUpgrades;
        Damage = BaseDamage +  (DamagePerUpgrade * TurretLevel * NumberOfDamageUpgrades);
        DamageUpgradeCostReactive.Value = CurrentDamageUpgradeCost();
        NumberOfDamageUpgradesReactive.Value = NumberOfDamageUpgrades;
    }

    public void OnTurretUpgrade()
    {
        CashReactive.Value -= TurretUpgradeCost * TurretLevel;
        TurretLevel++;
        Damage = (BaseDamage + (DamagePerUpgrade * NumberOfDamageUpgrades)) * TurretLevel;
        TurretUpgradeCostReactive.Value = CurrentTurretUpgradeCost();
        TurretLevelReactive.Value = TurretLevel;
    }

    public void OnHeal()
    {
        CashReactive.Value -= HealCost;
        Health = MaxHealth;
    }

    public int CurrentHealthUpgradeCost()
    {
        return HealthUpgradeCost * (NumberOfHealthUpgrades + 1);
    }

    public int CurrentDamageUpgradeCost()
    {
        return DamageUpgradeCost * (NumberOfDamageUpgrades + 1);
    }

    public int CurrentTurretUpgradeCost()
    {
        return TurretLevel * TurretUpgradeCost;
    }
}