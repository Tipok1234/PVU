using System.Collections;
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

        private Dictionary<ParticleType, List<ParticleSystem>> _particleDictionary;
        private Dictionary<AttackUnitType, List<AttackUnit>> _enemyUnitsDictionary;

        [SerializeField] private int _countType;
        [SerializeField] private Transform _particlesParent;
        [SerializeField] private Transform _enemyParent;

        private Transform _playerTransf;
        private static PoolManager instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;


            _particleDictionary = new Dictionary<ParticleType, List<ParticleSystem>>();
            _enemyUnitsDictionary = new Dictionary<AttackUnitType, List<AttackUnit>>();

            for (int i = 0; i < _createParticles.Count; i++)
            {
                var particleSystems = new List<ParticleSystem>();

                for (int j = 0; j < _countType; j++)
                {
                    particleSystems.Add(Instantiate(_createParticles[i].ParticleSystem, _particlesParent));
                }      
                _particleDictionary.Add(_createParticles[i].ParticleType,particleSystems);
            }

            for (int i = 0; i < _attackEnemyUnits.Count; i++)
            {
                var enemyUnits = new List<AttackUnit>();

                for (int j = 0; j < _countType; j++)
                {
                    enemyUnits.Add(Instantiate(_attackEnemyUnits[i], _enemyParent));
                }

                _enemyUnitsDictionary.Add(_attackEnemyUnits[i].AttackUnitType, enemyUnits);
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
                        list.Add(Instantiate(_createParticles[i].ParticleSystem,_particlesParent));
                        return list[list.Count - 1].transform;
                    }
                }
            }
            return null;
        }

        public AttackUnit GetEnemyUnitByType(AttackUnitType attackUnitType, Transform attackUnit)
        {
            if(_enemyUnitsDictionary.TryGetValue(attackUnitType, out var attackUnits))
            {
                for (int i = 0; i < attackUnits.Count; i++)
                {
                    if (attackUnits[i].gameObject.activeSelf == false)
                    {
                        attackUnits[i].transform.position = attackUnit.position;
                        attackUnits[i].gameObject.SetActive(true);
                        Debug.LogError(attackUnits[i].HP);
                        return attackUnits[i];
                    }
                }

                for (int i = 0; i < _attackEnemyUnits.Count; i++)
                {
                    if (_attackEnemyUnits[i].AttackUnitType == attackUnitType)
                    {
                        attackUnits[i].transform.position = attackUnit.position;
                        attackUnits[i].gameObject.SetActive(true);
                        attackUnits.Add(Instantiate(_attackEnemyUnits[i], _particlesParent));
                        return attackUnits[attackUnits.Count - 1];
                    }
                }
            }
            return null;
        }

        public void ReturnToPool (Transform objectTransform)
        {
            objectTransform.SetParent(_particlesParent);
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
