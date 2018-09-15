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

        public Text HealthUpgradeCost;
        public Text NumberOfHealthUpgrades;
        public Text DamageUpgradeCost;
        public Text NumberOfDamageUpgrades;
        public Text HealCost;
        public Text TurretUpgradeCost;
        public Text NumberOfTurretUpgrades;

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

            BuyTurretUpgrade = new ReactiveCommand(Manager.CashReactive.Select(cash => cash >= Manager.CurrentTurretUpgradeCost()));
            BuyTurretUpgrade.BindTo(UpgradeTurret);
            UpgradeTurret.OnClickAsObservable().Subscribe(_ =>
            {
                Manager.OnTurretUpgrade();
            }).AddTo(gameObject);

        }
	
        // Update is called once per frame
        void Update () {
		
        }
    }
}
