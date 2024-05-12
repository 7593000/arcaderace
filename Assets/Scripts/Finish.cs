using UnityEngine;

public class Finish : FinishTriggerComponent
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _engine.FixedTrigger(TriggerFinish.Finish);
        }

    }
}
