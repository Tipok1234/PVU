using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;
using Assets.Scripts.Models;

namespace Assets.Scripts.Managers
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager Instance => instance;
        public Transform ParticlesParent => _playerTransf;

        [SerializeField] private List<CreateParticle> _createParticles;
        [SerializeField] private List<AttackUnit> _attackEnemyUnits;
        [SerializeField] private List<Bullet> _bullets;
        [SerializeField] private List<DefenceUnit> _defenceUnits;
        [SerializeField] private List<ResourceModel> _resourceModel;
        [SerializeField] private Cart _cartPrefab;


        private Dictionary<ParticleType, List<ParticleSystem>> _particleDictionary;
        private Dictionary<AttackUnitType, List<AttackUnit>> _enemyUnitsDictionary;
        private Dictionary<BulletType, List<Bullet>> _bulletDictionary;
        private Dictionary<DefenceUnitType, List<DefenceUnit>> _defenceUnitsDictionary;
        private Dictionary<CurrencyType, List<ResourceModel>> _resourceModelDictionary;

        private List<Cart> _cartList;

        [SerializeField] private int _cartsCount = 6;
        [SerializeField] private int _countType;
        [SerializeField] private Transform _particlesParent;
        [SerializeField] private Transform _enemyParent;
        [SerializeField] private Transform _bulletParent;
        [SerializeField] private Transform _defenceUnitParent;
        [SerializeField] private Transform _resourceParent;
        [SerializeField] private Transform _cartParent;

        private Transform _playerTransf;
        private static PoolManager instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;

            _particleDictionary = new Dictionary<ParticleType, List<ParticleSystem>>();
            _enemyUnitsDictionary = new Dictionary<AttackUnitType, List<AttackUnit>>();
            _bulletDictionary = new Dictionary<BulletType, List<Bullet>>();
            _defenceUnitsDictionary = new Dictionary<DefenceUnitType, List<DefenceUnit>>();
            _resourceModelDictionary = new Dictionary<CurrencyType, List<ResourceModel>>();

            _cartList = new List<Cart>();

            for (int i = 0; i < _createParticles.Count; i++)
            {
                var particleSystems = new List<ParticleSystem>();

                for (int j = 0; j < _countType; j++)
                {
                    particleSystems.Add(Instantiate(_createParticles[i].ParticleSystem, _particlesParent));
                }
                _particleDictionary.Add(_createParticles[i].ParticleType, particleSystems);
            }

            for (int i = 0; i < _attackEnemyUnits.Count; i++)
            {
                var enemyUnits = new List<AttackUnit>();

                for (int j = 0; j < _countType; j++)
                {
                    enemyUnits.Add(Instantiate(_attackEnemyUnits[i], _enemyParent));
                }

                Debug.LogError(_attackEnemyUnits[i].name + " - " + _attackEnemyUnits[i].AttackUnitType);

                _enemyUnitsDictionary.Add(_attackEnemyUnits[i].AttackUnitType, enemyUnits);
            }

            for (int i = 0; i < _bullets.Count; i++)
            {
                var bullet = new List<Bullet>();

                for (int j = 0; j < _countType; j++)
                {
                    bullet.Add(Instantiate(_bullets[i], _bulletParent));
                }
                _bulletDictionary.Add(_bullets[i].BulletType, bullet);
            }

            for (int i = 0; i < _defenceUnits.Count; i++)
            {
                var defenceUnit = new List<DefenceUnit>();

                for (int j = 0; j < _countType; j++)
                {
                    defenceUnit.Add(Instantiate(_defenceUnits[i], _defenceUnitParent));
                }
                _defenceUnitsDictionary.Add(_defenceUnits[i].DefencUnitType, defenceUnit);
            }

            for (int i = 0; i < _resourceModel.Count; i++)
            {
                var resource = new List<ResourceModel>();

                for (int j = 0; j < _countType; j++)
                {
                    resource.Add(Instantiate(_resourceModel[i], _resourceParent));
                }
                _resourceModelDictionary.Add(_resourceModel[i].CurrencyType, resource);
            }

            for (int i = 0; i < _cartsCount; i++)
            {
                _cartList.Add(Instantiate(_cartPrefab, _cartParent));
            }
        }

        public Transform GetParticleByType(ParticleType particleType, Transform placeTransofrm)
        {
            if (_particleDictionary.TryGetValue(particleType, out var list))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].gameObject.activeSelf == false)
                    {
                        list[i].transform.position = placeTransofrm.position;
                        list[i].gameObject.SetActive(true);
                        return list[i].transform;
                    }
                }

                for (int i = 0; i < _createParticles.Count; i++)
                {
                    if (_createParticles[i].ParticleType == particleType)
                    {
                        list.Add(Instantiate(_createParticles[i].ParticleSystem, _particlesParent));
                        return list[list.Count - 1].transform;
                    }
                }
            }
            return null;
        }

        public AttackUnit GetEnemyUnitByType(AttackUnitType attackUnitType, Transform attackUnit)
        {
            if (_enemyUnitsDictionary.TryGetValue(attackUnitType, out var attackUnits))
            {
                for (int i = 0; i < attackUnits.Count; i++)
                {
                    if (attackUnits[i].gameObject.activeSelf == false)
                    {
                        attackUnits[i].transform.position = attackUnit.position;
                        attackUnits[i].gameObject.SetActive(true);
                        return attackUnits[i];
                    }
                }

                for (int i = 0; i < _attackEnemyUnits.Count; i++)
                {
                    if (_attackEnemyUnits[i].AttackUnitType == attackUnitType)
                    {
                        attackUnits[i].transform.position = attackUnit.position;
                        attackUnits[i].gameObject.SetActive(true);
                        attackUnits.Add(Instantiate(_attackEnemyUnits[i], _enemyParent));
                        return attackUnits[attackUnits.Count - 1];
                    }
                }
            }
            return null;
        }

        public Bullet GetBulletByType(BulletType bulletType, Transform bulletSpawn)
        {
            if (_bulletDictionary.TryGetValue(bulletType, out var bullets))
            {
                for (int i = 0; i < bullets.Count; i++)
                {
                    if (bullets[i].gameObject.activeSelf == false)
                    {
                        bullets[i].transform.position = bulletSpawn.position;
                        bullets[i].gameObject.SetActive(true);
                        return bullets[i];
                    }
                }

                for (int i = 0; i < _bullets.Count; i++)
                {
                    if (bullets[i].BulletType == bulletType)
                    {
                        bullets[i].transform.position = bulletSpawn.position;
                        bullets[i].gameObject.SetActive(true);
                        bullets.Add(Instantiate(_bullets[i], _bulletParent));
                        return bullets[bullets.Count - 1];
                    }
                }
            }
            return null;
        }

        public DefenceUnit GetDefenceUnitsByType(DefenceUnitType defenceUnitType, Transform defenceTransform)
        {
            if (_defenceUnitsDictionary.TryGetValue(defenceUnitType, out var defenceUnits))
            {
                for (int i = 0; i < defenceUnits.Count; i++)
                {
                    if (defenceUnits[i].gameObject.activeSelf == false)
                    {
                        defenceUnits[i].transform.position = _defenceUnitParent.position;
                        defenceUnits[i].gameObject.SetActive(true);
                        return defenceUnits[i];
                    }
                }

                for (int i = 0; i < defenceUnits.Count; i++)
                {
                    if (defenceUnits[i].DefencUnitType == defenceUnitType)
                    {
                        defenceUnits[i].transform.position = _defenceUnitParent.position;
                        defenceUnits[i].gameObject.SetActive(true);
                        defenceUnits.Add(Instantiate(_defenceUnits[i], _defenceUnitParent));
                        return defenceUnits[defenceUnits.Count - 1];
                    }
                }
            }
            return null;
        }

        public ResourceModel GetResourceModelByType(CurrencyType currencyType, Transform resourceTransform)
        {
            if (_resourceModelDictionary.TryGetValue(currencyType, out var resources))
            {
                for (int i = 0; i < resources.Count; i++)
                {
                    if (resources[i].gameObject.activeSelf == false)
                    {
                        resources[i].transform.position = resourceTransform.position;
                        resources[i].gameObject.SetActive(true);
                        return resources[i];
                    }
                }

                for (int i = 0; i < resources.Count; i++)
                {
                    if (resources[i].CurrencyType == currencyType)
                    {
                        resources[i].transform.position = resourceTransform.position;
                        resources[i].gameObject.SetActive(true);
                        resources.Add(Instantiate(_resourceModel[i], resourceTransform));
                        return resources[resources.Count - 1];
                    }
                }
            }
            return null;
        }

        public Transform GetCart(Vector3 cartPos)
        {
            for (int i = 0; i < _cartList.Count; i++)
            {
                if (_cartList[i].gameObject.activeSelf == false)
                {
                    _cartList[i].transform.position = cartPos;
                    _cartList[i].gameObject.SetActive(true);
                    return _cartList[i].transform;
                }
            }

            var newCart = Instantiate(_cartPrefab, _cartParent);
            newCart.gameObject.SetActive(true);
            _cartList.Add(newCart);

            return _cartList[_cartList.Count - 1].transform;
        }

        public void ReturnToPool(Transform objectTransform)
        {
            objectTransform.SetParent(_particlesParent);
        }

        public void ReturnUnitToPool(Transform unitTransform)
        {
            unitTransform.SetParent(_defenceUnitParent);
        }
    }

    [System.Serializable]
    public class CreateParticle
    {
        public ParticleType ParticleType => _particleType;
        public ParticleSystem ParticleSystem => _particleSystem;

        [SerializeField] private ParticleType _particleType;
        [SerializeField] private ParticleSystem _particleSystem;
    }
}
