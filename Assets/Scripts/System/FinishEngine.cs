using System;
using System.Collections.Generic;
using UnityEngine;

public class FinishEngine : MonoBehaviour
{
    public static event Action<bool> OnTimerWork;

    private Timer _timer;
    [ SerializeField]
    private List<FinishTriggerComponent> _triggers;
 
    [SerializeField]
    private bool _isAntiFinishActive = false;

    private Score _score; //todo -> Temp

    private void OnValidate()
    {
        _score = FindFirstObjectByType<Score>();
        _timer = FindFirstObjectByType<Timer>();
    }
    private void Start()
    {
        foreach (var trigger in _triggers)
        {
            trigger.InstallEngine(this);
        }
    }


    public void FixedTrigger(TriggerFinish trigger)
    {

        switch (trigger)
        {
            case TriggerFinish.Finish:
                if (_isAntiFinishActive) break;
                Finish();
                break;
            case TriggerFinish.AntiFinish:
                _isAntiFinishActive = true;

                break;
            case TriggerFinish.ResetAntiFinish:
                _isAntiFinishActive = false;

                break;
        }
    }

    private void Finish()
    {
        Debug.Log("Finish");

        OnTimerWork?.Invoke(false);
        string timerResult = _timer.GetTimeIndicators ;
        _score.InstallScoreTable( timerResult );
    }






}
