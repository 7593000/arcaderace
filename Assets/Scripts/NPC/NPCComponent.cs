 
using UnityEngine;
 
public class NPCComponent : CarComponent
{
    private StartCountdown _countDown;
    private NPCMoveComponent _move;

    private void OnEnable()
    {
        //FinishEngine.OnTimerWork += OnStatusMovement;
        StartCountdown.OnStartRace += OnStatusMovement;
    }
    private void OnDisable()
    {
        //FinishEngine.OnTimerWork -= OnStatusMovement;
        StartCountdown.OnStartRace -= OnStatusMovement;
    }

    protected override void OnValidate()
    {
     base.OnValidate();
    _move??= GetComponentInChildren<NPCMoveComponent>();
    }
 
    protected override void Start()
    {
        base.Start();
        _movement = new Movement(this);
    }
    private void OnStatusMovement( bool status )
    {

        _movement.OnStarRace( status );
    }

    public void UseBrake(float speedLimit)
    {
          _move.BrakeCoroutine(speedLimit);
    }

    protected override void GetInput()
    {
        
    }
}
 
