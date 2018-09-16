using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI_Scripts
{
    public class ShopController : MonoBehaviour
    {
        public Button Healthupgrade;
        public Button DamageUpgraderade;
        public Button Heal;
        public Button UpgradeTurret;

        public TextMeshProUGUI HealthUpgradeCost;
        public TextMeshProUGUI NumberOfHealthUpgrades;
        public TextMeshProUGUI DamageUpgradeCost;
        public TextMeshProUGUI NumberOfDamageUpgrades;
        public TextMeshProUGUI HealCost;
        public TextMeshProUGUI TurretUpgradeCost;
        public TextMeshProUGUI NumberOfTurretUpgrades;

        public GameManager Manager;

        private ReactiveCommand BuyHealthUpgrade;
        private ReactiveCommand BuyDamageUpgrade;

        private ReactiveCommand BuyHeal;
        private ReactiveCommand BuyTurretUpgrade;

        // Use this for initialization
        void Start () {
            Manager = FindObjectOfType<GameManager>();

            BuyHealthUpgrade = new ReactiveCommand(Manager.CashReactive.Select(cash => cash >= Manager.CurrentHealthUpgradeCost()));
            BuyHealthUpgrade.BindTo(Healthupgrade);
            Healthupgrade.OnClickAsObservable().Subscribe(_ =>
            {
                Manager.OnUpgradeHealth();
            }).AddTo(gameObject);

            BuyDamageUpgrade = new ReactiveCommand(Manager.CashReactive.Select(cash => cash >= Manager.CurrentDamageUpgradeCost()));
            BuyDamageUpgrade.BindTo(DamageUpgraderade);
            DamageUpgraderade.OnClickAsObservable().Subscribe(_ =>
            {
                Manager.OnUpgradeDamage();
            }).AddTo(gameObject);

            BuyHeal = new ReactiveCommand(Manager.CashReactive.Select(cash => cash >= Manager.HealCost));
            BuyHeal.BindTo(Heal);
            Heal.OnClickAsObservable().Subscribe(_ =>
            {
                Manager.OnHeal();
            }).AddTo(gameObject);

            BuyTurretUpgrade = new ReactiveCommand(Manager.CashReactive.Select(cash => cash >= Manager.TurretUpgradeCostReactive.Value));
            BuyTurretUpgrade.BindTo(UpgradeTurret);
            UpgradeTurret.OnClickAsObservable().Subscribe(_ =>
            {
                Manager.OnTurretUpgrade();
            }).AddTo(gameObject);

            Manager.HealthUpgradeCostReactive.Subscribe(val => HealthUpgradeCost.text = val.ToString())
                .AddTo(gameObject);
            Manager.NumberOfHealthUpgradesReactive.Subscribe(val => NumberOfHealthUpgrades.text = val.ToString())
                .AddTo(gameObject);
            Manager.DamageUpgradeCostReactive.Subscribe(val => DamageUpgradeCost.text = val.ToString())
                .AddTo(gameObject);
            Manager.NumberOfDamageUpgradesReactive.Subscribe(val => NumberOfDamageUpgrades.text = val.ToString())
                .AddTo(gameObject);
            Manager.TurretLevelReactive.Subscribe(val => NumberOfTurretUpgrades.text = val.ToString())
                .AddTo(gameObject);
            Manager.TurretUpgradeCostReactive.Subscribe(val => TurretUpgradeCost.text = val.ToString())
                .AddTo(gameObject);
            HealCost.text = Manager.HealCost.ToString();
        }
	
        // Update is called once per frame
        void Update () {
		
        }
    }
}
