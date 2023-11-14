using System;
using UnityEngine;

namespace MrPaganini.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstraper _bootstraperPrefab;

        public void Awake()
        {
            var bootstrapper = FindObjectOfType<GameBootstraper>();
            if (bootstrapper == null)
                Instantiate(_bootstraperPrefab);
        }
    }
}