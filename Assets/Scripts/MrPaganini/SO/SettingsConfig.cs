using System;
using MrPaganini.Infrastruct.Settings;
using UnityEngine;

namespace GameResources.SO
{
    [CreateAssetMenu(fileName = "New SettingsConfig", menuName = "Settings/New Settings")]
    public class SettingsConfig : ScriptableObject
    {
        [SerializeField] public ScreenSettings ScreenSettings;

        public float Volume;
    }
}