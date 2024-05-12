using UnityEngine;

public abstract class FinishTriggerComponent : MonoBehaviour
{
    protected FinishEngine _engine;

    public virtual void InstallEngine(FinishEngine engine)
    {
        _engine = engine;
    }

    protected abstract void OnTriggerEnter(Collider other);

}
