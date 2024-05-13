﻿using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class BaseUI : MonoBehaviour
    {
        [HideInInspector]
        public GameObject content;
        protected Canvas canvas;
        protected UIAnimator uiAnimator;
        protected UIAnimation uiAnimation;
        [HideInInspector]
        public CanvasGroup canvasGroup;
        public bool isActive { get; private set; }
        public int maxVisibleLayer = 2;

        public virtual void Awake()
        {
            content = transform.GetChild(0).gameObject;

            canvas = GetComponent<Canvas>();
            canvasGroup = content.GetComponent<CanvasGroup>();
            uiAnimator = GetComponent<UIAnimator>();
            uiAnimation = GetComponent<UIAnimation>();
        }

        public virtual void Disable()
        {
            if (canvas)
                canvas.enabled = false;//screws with the joysticks because apparently they scale (what?)// content.SetActive(false);
            isActive = false;
        }

        public virtual void Enable()
        {
            canvas.enabled = true;//screws with the joysticks because apparently they scale (what?)// content.SetActive(true);
            isActive = true;

            Invoke(nameof(ResetAllSliders), 0.2f);
        }

        private void ResetAllSliders()
        {
            var allSliders = GetComponentsInChildren<ScrollRect>();
            foreach (var slider in allSliders)
            {
                slider.normalizedPosition = slider.horizontal ? new Vector2(0f,0f) : new Vector2(0f,1f);
            }
        }

        public virtual void Show()
        {
            if (isActive)
                return;
            if (canvasGroup != null)
                canvasGroup.interactable = true;

            if (uiAnimator)
            {
                uiAnimator.StopHide();
                uiAnimator.StartShow();
                isActive = true;
            }
            else
            {
                Enable();
                isActive = true;
            }

            Redraw();
        }

        public virtual void Hide()
        {
            if (canvasGroup != null)
                canvasGroup.interactable = false;

            if (uiAnimator)
            {
                uiAnimator.StopShow();
                uiAnimator.StartHide();
                isActive = false;
            }
            else
            {
                Disable();
                isActive = false;
            }
        }

        public virtual void Redraw()
        {
        }

        public int GetSortingOrder()
        {
            return canvas.sortingOrder;
        }

        public void SetSortingOrder(int sortingOrder)
        {
            if (canvas)
                canvas.sortingOrder = sortingOrder;
        }

        public void ToggleCanvas(bool status)
        {
            canvas.enabled = status;
        }
    }
}