using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UnityEngine.UI
{
    /// <summary>
    /// A standard button that sends an event when clicked.
    /// </summary>
    [AddComponentMenu("UI/Button", 30)]
    public class MyButton : Selectable, IPointerClickHandler, ISubmitHandler
    {
        [Serializable]
        /// <summary>
        /// Function definition for a button click event.
        /// </summary>
        public class ButtonClickedEvent : UnityEvent { }

        // Event delegates triggered on Pointer Down.
        [FormerlySerializedAs("onPointerDown")]
        // Event delegates triggered on Pointer Up.
        [FormerlySerializedAs("onPointerUp")]
        // Event delegates triggered on click.
        [FormerlySerializedAs("onClick")]
        // Event delegates triggered on Pointer enter.
        [FormerlySerializedAs("onPointerEnter")]
        // Event delegates triggered on Pointer exit.
        [FormerlySerializedAs("onPointerExit")]

        [SerializeField]
        private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();

        [SerializeField]
        private ButtonClickedEvent m_onPointerUp = new ButtonClickedEvent();

        [SerializeField]
        private ButtonClickedEvent m_onPointerDown = new ButtonClickedEvent();

        [SerializeField]
        private ButtonClickedEvent m_onPointerEnter = new ButtonClickedEvent();

        [SerializeField]
        private ButtonClickedEvent m_onPointerExit = new ButtonClickedEvent();

        protected MyButton()
        { }

        public ButtonClickedEvent onPointerDown
        {
            get { return m_onPointerDown; }
            set { m_onPointerDown = value; }
        }

        public ButtonClickedEvent onPointerUp
        {
            get { return m_onPointerUp; }
            set { m_onPointerUp = value; }
        }

        public ButtonClickedEvent onClick
        {
            get { return m_OnClick; }
            set { m_OnClick = value; }
        }

        public ButtonClickedEvent onPointerEnter
        {
            get { return m_onPointerEnter; }
            set { m_onPointerEnter = value; }
        }

        public ButtonClickedEvent onPointerExit
        {
            get { return m_onPointerExit; }
            set { m_onPointerExit = value; }
        }

        private void Press()
        {
            if (!IsActive() || !IsInteractable())
                return;

            UISystemProfilerApi.AddMarker("MyButton.onClick", this);
            m_OnClick.Invoke();
        }

        private void PressDown()
        {
            if (!IsActive() || !IsInteractable())
                return;

            UISystemProfilerApi.AddMarker("MyButton.onPointerDown", this);
            m_onPointerDown.Invoke();
        }

        private void PressUp()
        {
            if (!IsActive() || !IsInteractable())
                return;

            UISystemProfilerApi.AddMarker("MyButton.onPointerUp", this);
            m_onPointerUp.Invoke();
        }

        private void PointerEnter()
        {
            if (!IsActive() || !IsInteractable())
                return;

            UISystemProfilerApi.AddMarker("MyButton.onPointerEnter", this);
            m_onPointerEnter.Invoke();
        }

        private void PointerExit()
        {
            if (!IsActive() || !IsInteractable())
                return;

            UISystemProfilerApi.AddMarker("MyButton.onPointerExit", this);
            m_onPointerExit.Invoke();
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            Press();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            if (eventData.button != PointerEventData.InputButton.Left)
                return;
            PressDown();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            if (eventData.button != PointerEventData.InputButton.Left)
                return;
            PressUp();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            if (eventData.button != PointerEventData.InputButton.Left)
                return;
            PointerExit();
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            if (eventData.button != PointerEventData.InputButton.Left)
                return;
            PointerEnter();
        }

        public virtual void OnSubmit(BaseEventData eventData)
        {
            Press();

            // if we get set disabled during the press
            // don't run the coroutine.
            if (!IsActive() || !IsInteractable())
                return;

            DoStateTransition(SelectionState.Pressed, false);
            StartCoroutine(OnFinishSubmit());
        }

        private IEnumerator OnFinishSubmit()
        {
            var fadeTime = colors.fadeDuration;
            var elapsedTime = 0f;

            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.unscaledDeltaTime;
                yield return null;
            }

            DoStateTransition(currentSelectionState, false);
        }
    }
}

public class SuperButton : MyButton { }


