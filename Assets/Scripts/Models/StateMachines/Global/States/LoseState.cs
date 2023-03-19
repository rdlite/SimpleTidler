using System.Collections;
using UnityEngine;

public class LoseState : IState
{
    private UIRoot _uiRoot;
    private GlobalStateMachine _globalSM;
    private Configs _configs;
    private ICoroutineRunner _coroutineRunner;

    public LoseState(
        GlobalStateMachine globalSM, Configs configs, ICoroutineRunner coroutineRunner,
        UIRoot uiRoot)
    {
        _uiRoot = uiRoot;
        _globalSM = globalSM;
        _configs = configs;
        _coroutineRunner = coroutineRunner;
    }

    public void Enter()
    {
        _coroutineRunner.StartCoroutine(LosingRoutine());
    }

    private IEnumerator LosingRoutine()
    {
        _uiRoot.DeactivatePanels();
        _globalSM.StatesData.PlayerTower.DeactivateRadiusVisual();

        Vector3 startTowerPos = _globalSM.StatesData.PlayerTower.transform.position;
        float t = 0f;

        while (t <= 1f)
        {
            t += .1f;

            _globalSM.StatesData.PlayerTower.transform.position = startTowerPos;

            yield return new WaitForSeconds(.05f);

            _globalSM.StatesData.PlayerTower.transform.position += Random.insideUnitSphere * .1f;

            yield return new WaitForSeconds(.05f);
        }

        Object.Instantiate(_configs.PrefabsContainer.PlayerDeathFX, _globalSM.StatesData.PlayerTower.transform.position, _configs.PrefabsContainer.PlayerDeathFX.transform.rotation);
        Object.Destroy(_globalSM.StatesData.PlayerTower.gameObject);

        yield return new WaitForSeconds(1f);

        _uiRoot.EnablePanel<LosePanel>();
    }

    public void Exit() { }
}
