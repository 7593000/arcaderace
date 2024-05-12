using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TMP_Text _timer;
    private float _elapsedTime = 0f;
    private bool _restartTimer = false;
    private bool _workTimer = false;
    private string _formattedTime = string.Empty;
    private string _timerString = string.Empty;
    public string GetTimeIndicators => _timerString;
    private void OnValidate()
    {
        _timer = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        FinishEngine.OnTimerWork += StatusTimer;
        StartCountdown.OnStartRace += StatusTimer;
    }
    private void OnDisable()

    {
        StartCountdown.OnStartRace -= StatusTimer;
        FinishEngine.OnTimerWork -= StatusTimer;

    }
    private void StatusTimer(bool status)
    {
        _workTimer = status;
    }
    private readonly string _timeFormat = "{0:00}:{1:00}:<color=red>{2:00}</color>";

    private string FormatTime(float time, ref string _timerstring)
    {

        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 100) % 100);




        _timerstring = $"{CheckLenght(minutes)}{CheckLenght(seconds)}{CheckLenght(milliseconds)}";

        return string.Format(_timeFormat, minutes, seconds, milliseconds);

    }

    /// <summary>
    /// Добавить вперед 0 
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    private string CheckLenght(int number)
    {
        string temp = number.ToString();

        if (number < 10)
        {
            return "0" + number;
        }

        return temp;
    }


    void Update()
    {
        if (_restartTimer) _elapsedTime = 0;
        if (_workTimer == false) return;
        _elapsedTime += Time.deltaTime;

        _formattedTime = FormatTime(_elapsedTime, ref _timerString);
        _timer.text = _formattedTime;
    }


}
