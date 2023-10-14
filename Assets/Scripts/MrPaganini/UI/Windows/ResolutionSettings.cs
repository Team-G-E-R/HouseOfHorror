using System.Collections.Generic;
using GameResources.SO;
using TMPro;
using UnityEngine;

namespace MrPaganini.UI.Windows
{
    public class ResolutionSettings : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _resolutionDropdown;

        private Resolution[] _resolutions;
        private List<Resolution> _filteredResolutions;

        private float _currentRefreshRate;
        private int _currentResolutionIndex = 0;
        private SettingsConfig _settingsConfig;

        public void Start()
        {
            _settingsConfig = AllServices.Singleton.Single<ISettingsService>().SettingsConfig;  
            StartSettings();
        }

        public void SetResolution(int resolutionIndex)
        {
            Debug.Log(resolutionIndex);
            Resolution resolution = _filteredResolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, true);
            Debug.Log("SetResolution " + Screen.currentResolution.width + " "+ Screen.currentResolution.height);
            _settingsConfig.ScreenSettings.SetResolution(resolution.width, resolution.height,
                (int)(_currentRefreshRate));
        }

        private void StartSettings()
        { 
            _resolutions = Screen.resolutions;
            _filteredResolutions = new List<Resolution>();

            _resolutionDropdown.ClearOptions();
            _currentRefreshRate = Screen.currentResolution.refreshRate;

            FilteredResolution();
            SetResolutionsInDropdown();
        }

        private void FilteredResolution()
        {
            foreach (var resolution in _resolutions)
            {
                if (resolution.refreshRate == _currentRefreshRate)
                    _filteredResolutions.Add(resolution);
            }
        }

        private void SetResolutionsInDropdown()
        {
            List<string> options = new List<string>();
            for (var i = 0; i < _filteredResolutions.Count; i++)
            {
                var filteredResolution = _filteredResolutions[i];
                string resolutionOption = filteredResolution.width + "x" +
                                          filteredResolution.height + " " +
                                          filteredResolution.refreshRate + " Hz";
                options.Add(resolutionOption);
                if (filteredResolution.width == Screen.width &&
                    filteredResolution.height == Screen.height)
                {
                    _currentResolutionIndex = i;
                }
            }

            _resolutionDropdown.AddOptions(options);
            _resolutionDropdown.value = _currentResolutionIndex;
            _resolutionDropdown.RefreshShownValue();
        }
    }
}