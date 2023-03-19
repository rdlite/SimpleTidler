using UnityEngine;

public abstract class UIPanel : MonoBehaviour
{
    protected GlobalStateMachine _globalSM;
    protected Configs _configs;

    public void Init(Configs configs, GlobalStateMachine globalSM)
    {
        _globalSM = globalSM;
        _configs = configs;

        LocalInit();
    }

    protected abstract void LocalInit();

    public virtual void Enable()
    {
        gameObject.SetActive(true);
    }

    public virtual void Enable<TPayload>(TPayload payload)
    {
        gameObject.SetActive(true);
    }

    public virtual void Disable()
    {
        gameObject.SetActive(false);
    }
}

public class PayloadData
{
    public bool IsShowAnimation;

    public PayloadData(bool isShowAnimation)
    {
        IsShowAnimation = isShowAnimation;
    }
}