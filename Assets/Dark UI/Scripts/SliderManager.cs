﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Michsky.UI.Dark
{
    public class SliderManager : MonoBehaviour
    {
        [Header("TEXTS")]
        public TextMeshProUGUI valueText;

        [Header("SAVING")]
        public bool enableSaving = false;
        public string sliderTag = "Tag Text";
        public float defaultValue = 1;

        [Header("SETTINGS")]
        public bool usePercent = false;
        public bool showValue = true;
        public bool useRoundValue = false;

        private Slider mainSlider;
        float saveValue;

        void Start()
        {
            mainSlider = GetComponent<Slider>();

            if (showValue == false)
                valueText.enabled = false;

            if (enableSaving == true)
            {
                saveValue = DataManager.Instance.GetFloatDataPrefGame(sliderTag + "Volume");

                mainSlider.value = saveValue;

                mainSlider.onValueChanged.AddListener(delegate
                {
                    saveValue = mainSlider.value;
                    DataManager.Instance.SaveDataPrefGame(sliderTag + "Volume", saveValue);
                });
            }
        }

        void Update()
        {
            if (useRoundValue == true)
            {
                if (usePercent == true)
                    valueText.text = Mathf.Round(mainSlider.value * 1.0f).ToString() + "%";

                else
                    valueText.text = Mathf.Round(mainSlider.value * 1.0f).ToString();
            }

            else
            {
                if (usePercent == true)
                    valueText.text = mainSlider.value.ToString("F1") + "%";

                else
                    valueText.text = mainSlider.value.ToString("F1");
            }
        }
    }
}