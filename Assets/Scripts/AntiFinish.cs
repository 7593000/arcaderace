
using UnityEngine;

public class AntiFinish : FinishTriggerComponent
{


    protected override void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            _engine.FixedTrigger(TriggerFinish.AntiFinish);
        }
    }
}
