
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GameInput
{
    public abstract class JoystickHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image _joystickBackground;
        [SerializeField] private Image _joystick;
        [SerializeField] private Image _joystickArea;

        // private Vector2 _joystickBackgroundStartPosition;
        protected Vector2 _inputVector;

        [SerializeField] private Color _inactiveColor;
        [SerializeField] private Color _activeColor;

        public bool IsActive = false;


        private void Start()
        {
            IsActive = false;
            ClickEffect();

            // _joystickBackgroundStartPosition = _joystickBackground.rectTransform.anchoredPosition;
        }


        private void ClickEffect()
        {
            _joystick.color = IsActive ? _activeColor : _inactiveColor;
            _joystickBackground.color = IsActive ? _activeColor : _inactiveColor;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsActive = true;
            ClickEffect();

            Vector2 joystickBackgroundPosition;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickArea.rectTransform, eventData.position, null, out joystickBackgroundPosition))
            {
                _joystickBackground.rectTransform.anchoredPosition = new Vector2(joystickBackgroundPosition.x, joystickBackgroundPosition.y);
            }
        }


        public void OnDragOld(PointerEventData eventData)
        {
            Vector2 joystickPosition;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackground.rectTransform, eventData.position, null, out joystickPosition))
            {
                joystickPosition.x = joystickPosition.x * 2 / _joystickBackground.rectTransform.sizeDelta.x;
                joystickPosition.y = joystickPosition.y * 2 / _joystickBackground.rectTransform.sizeDelta.y;

                _inputVector = new Vector2(joystickPosition.x, joystickPosition.y);

                _inputVector = (_inputVector.magnitude > 1f) ? _inputVector.normalized : _inputVector;
                _joystick.rectTransform.anchoredPosition = new Vector2(_inputVector.x * (_joystickBackground.rectTransform.sizeDelta.x / 2), _inputVector.y * (_joystickBackground.rectTransform.sizeDelta.y / 2));
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 joystickPosition;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackground.rectTransform, eventData.position, null, out joystickPosition))
            {
                // Учитываем скорость движения пальца
                Vector2 deltaMovement = eventData.delta / _joystickBackground.rectTransform.sizeDelta;

                _inputVector += deltaMovement;
                _inputVector = (_inputVector.magnitude > 1f) ? _inputVector.normalized : _inputVector;

                _joystick.rectTransform.anchoredPosition = new Vector2(_inputVector.x * (_joystickBackground.rectTransform.sizeDelta.x / 2), _inputVector.y * (_joystickBackground.rectTransform.sizeDelta.y / 2));
            }
        }



        public void OnPointerUp(PointerEventData eventData)
        {
            _joystick.rectTransform.anchoredPosition = Vector2.zero;
            _inputVector = Vector2.zero;

            // _joystickBackground.rectTransform.anchoredPosition = _joystickBackgroundStartPosition;

            IsActive = false;
            ClickEffect();
        }
    }

}
