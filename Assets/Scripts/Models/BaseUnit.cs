using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class BaseUnit : MonoBehaviour
    {
        [SerializeField] private int _hp;
        [SerializeField] private Collider _colliderUnit;
    }
}
