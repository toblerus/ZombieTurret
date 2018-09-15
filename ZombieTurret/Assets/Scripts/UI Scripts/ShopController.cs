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

        public GameManager Manager;

        private ReactiveCommand BuyHealthUpgrade;
        private ReactiveCommand BuyDamageUpgrade;

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



        }
	
        // Update is called once per frame
        void Update () {
		
        }
    }
}
