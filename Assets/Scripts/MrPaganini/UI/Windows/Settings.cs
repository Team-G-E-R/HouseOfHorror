using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using GameResources.SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MrPaganini.UI.Windows
{
    public class Settings : Window
    {
        [SerializeField] private ResolutionSettings _resolutionSettings;
        [SerializeField] private AudioSettings _audioSettings;
    }
    
}