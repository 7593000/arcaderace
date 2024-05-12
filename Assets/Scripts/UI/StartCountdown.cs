using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class StartCountdown : MonoBehaviour
{
    public static event Action<bool> OnStartRace;
    private TMP_Text _text;
    private Animation _animation;
    [SerializeField, Tooltip("Начала отсчета")]
    private int _countDown = 3;
    [SerializeField, Tooltip("пауза для отсчета")]
    private float _timer = 1;

    private void OnValidate()
    {
        _text = GetComponent<TMP_Text>();
        _animation = GetComponent<Animation>();
    }

    private IEnumerator GetCount()
    {
        yield return new WaitForSeconds(_timer);
        _countDown--;

        if (_countDown < 1)
        {
            _text.text = "START";
            OnStartRace?.Invoke(true);
            yield return new WaitForSeconds(_timer);
            _text.text = string.Empty;
            yield break;
        }
        _animation.Play();
        _text.text = _countDown.ToString();
    }

    public void OnAnimationEvent()
    {
        StartCoroutine(GetCount());

    }

    private void Start()
    {
        if (_animation == null)
            Debug.LogWarning("A component is missing Animation");
        if (_text == null)
            Debug.LogWarning("A component is missing  TMP_Text");
        _text.text = _countDown.ToString();
        _animation.Play();
    }

}
