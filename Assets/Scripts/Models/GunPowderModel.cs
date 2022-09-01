using UnityEngine;

namespace Assets.Scripts.Models
{
    public class GunPowderModel : MonoBehaviour
    {
        public int SoftCurrency => _softCurrency;

        [SerializeField] private int _softCurrency;

        public void Setup()
        {
            Destroy(gameObject, 3.0f);
        }

    }
}
