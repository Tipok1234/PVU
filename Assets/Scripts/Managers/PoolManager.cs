using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;

namespace Assets.Scripts.Managers
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager Instance => instance;
        public Transform ParticlesParent => _playerTransf;

        [SerializeField] private List<CreateParticle> _createParticles;
        private Dictionary<ParticleType, List<ParticleSystem>> _particleDictionary;

        [SerializeField] private int _countType;
        [SerializeField] private Transform _particlesParent;

        private Transform _playerTransf;
        private static PoolManager instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;


            _particleDictionary = new Dictionary<ParticleType, List<ParticleSystem>>();

            for (int i = 0; i < _createParticles.Count; i++)
            {
                var particleSystems = new List<ParticleSystem>();

                for (int j = 0; j < _countType; j++)
                {
                    particleSystems.Add(Instantiate(_createParticles[i].ParticleSystem, _particlesParent));
                }      
                _particleDictionary.Add(_createParticles[i].ParticleType,particleSystems);
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
