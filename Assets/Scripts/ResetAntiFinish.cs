
using UnityEngine;

public class ResetAntiFinish : FinishTriggerComponent
{

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _engine.FixedTrigger(TriggerFinish.ResetAntiFinish);
        }
    }
}
