using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Michsky.UI.Dark
{
    public class PanelTabManager : MonoBehaviour
    {
        [Header("PANEL LIST")]
        public List<GameObject> panels = new List<GameObject>();

        [Header("BUTTON LIST")]
        public List<GameObject> buttons = new List<GameObject>();

        public GameObject currentPanel;
        public GameObject nextPanel;

        public GameObject currentButton;
        public GameObject nextButton;

        [Header("SETTINGS")]
        public int currentPanelIndex = 0;
        public int currentButtonlIndex = 0;

        public Animator currentPanelAnimator;
        public Animator nextPanelAnimator;

        public Animator currentButtonAnimator;
        public Animator nextButtonAnimator;

        public string panelFadeIn = "Panel In";
        public string panelFadeOut = "Panel Out";
        public string buttonFadeIn = "Hover to Pressed";
        public string buttonFadeOut = "Pressed to Normal";

        void OnEnable()
        {
            currentButton = buttons[currentPanelIndex];
            currentButtonAnimator = currentButton.GetComponent<Animator>();
            currentButtonAnimator.Play(buttonFadeIn);

            currentPanel = panels[currentPanelIndex];
            currentPanelAnimator = currentPanel.GetComponent<Animator>();
            currentPanelAnimator.Play(panelFadeIn);
        }

        public void OpenFirstTab()
        {
            currentPanel = panels[currentPanelIndex];
            currentPanelAnimator = currentPanel.GetComponent<Animator>();
            currentPanelAnimator.Play(panelFadeIn);

            currentButton = buttons[currentPanelIndex];
            currentButtonAnimator = currentButton.GetComponent<Animator>();
            currentButtonAnimator.Play(buttonFadeIn);
        }

        public void PanelAnim(int newPanel)
        {
            if (newPanel != currentPanelIndex)
            {
                currentPanel = panels[currentPanelIndex];
                currentPanelIndex = newPanel;
                nextPanel = panels[currentPanelIndex];

                currentPanelAnimator = currentPanel.GetComponent<Animator>();
                nextPanelAnimator = nextPanel.GetComponent<Animator>();
                currentPanelAnimator.Play(panelFadeOut);
                nextPanelAnimator.Play(panelFadeIn);

                currentButton = buttons[currentButtonlIndex];
                currentButtonlIndex = newPanel;
                nextButton = buttons[currentButtonlIndex];

                currentButtonAnimator = currentButton.GetComponent<Animator>();
                nextButtonAnimator = nextButton.GetComponent<Animator>();

                currentButtonAnimator.Play(buttonFadeOut);
                nextButtonAnimator.Play(buttonFadeIn);
            }
        }

        public void NextPage()
        {
            if (currentPanelIndex <= panels.Count - 2)
            {
                currentPanel = panels[currentPanelIndex];
                currentButton = buttons[currentButtonlIndex];
                nextButton = buttons[currentButtonlIndex + 1];

                currentPanelAnimator = currentPanel.GetComponent<Animator>();
                currentButtonAnimator = currentButton.GetComponent<Animator>();           
                currentButtonAnimator.Play(buttonFadeOut);
                currentPanelAnimator.Play(panelFadeOut);

                currentPanelIndex += 1;
                currentButtonlIndex += 1;
                nextPanel = panels[currentPanelIndex];

                nextPanelAnimator = nextPanel.GetComponent<Animator>();
                nextButtonAnimator = nextButton.GetComponent<Animator>();
                nextPanelAnimator.Play(panelFadeIn);
                nextButtonAnimator.Play(buttonFadeIn);
            }
        }

        public void PrevPage()
        {
            if (currentPanelIndex >= 1)
            {
                currentPanel = panels[currentPanelIndex];
                currentButton = buttons[currentButtonlIndex];
                nextButton = buttons[currentButtonlIndex - 1];

                currentPanelAnimator = currentPanel.GetComponent<Animator>();
                currentButtonAnimator = currentButton.GetComponent<Animator>();
                currentButtonAnimator.Play(buttonFadeOut);
                currentPanelAnimator.Play(panelFadeOut);

                currentPanelIndex -= 1;
                currentButtonlIndex -= 1;
                nextPanel = panels[currentPanelIndex];

                nextPanelAnimator = nextPanel.GetComponent<Animator>();
                nextButtonAnimator = nextButton.GetComponent<Animator>();
                nextPanelAnimator.Play(panelFadeIn);
                nextButtonAnimator.Play(buttonFadeIn);
            }
        }
    }
}