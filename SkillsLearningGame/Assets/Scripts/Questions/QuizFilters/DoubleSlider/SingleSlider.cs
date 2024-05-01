using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Slider))]
public class SingleSlider : MonoBehaviour, IBeginDragHandler, IPointerDownHandler
{

    [Header("References")]
    [SerializeField] private Label _label;

    // To add priority
    public event Action<SingleSlider> OnStartDrag;

    private Slider _slider;

    public bool IsEnabled
    {
        get { return _slider.interactable; }
        set { _slider.interactable = value; }

    }
    public float Value
    {
        get { return _slider.value; }
        set
        {
            _slider.value = value;
            _slider.onValueChanged.Invoke(_slider.value);

            UpdateLabel();
        }
    }
    public bool WholeNumbers
    {
        get { return _slider.wholeNumbers; }
        set { _slider.wholeNumbers = value; }
    }

    private void Awake()
    {
        if (!TryGetComponent<Slider>(out _slider))
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogError("Missing Slider Component");
#endif
        }
    }

    public void Setup(float value, float minValue, float maxValue, UnityAction<float> valueChanged)
    {
        _slider.minValue = minValue;
        _slider.maxValue = maxValue;

        _slider.value = value;
        _slider.onValueChanged.AddListener(Slider_OnValueChanged);
        _slider.onValueChanged.AddListener(valueChanged);
    }

    private void Slider_OnValueChanged(float arg0)
    {
        UpdateLabel();
    }

    protected virtual void UpdateLabel()
    {
        if (_label == null) { return; }
        _label.Text = Value.ToString();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnStartDrag?.Invoke(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnStartDrag?.Invoke(this);
    }
}