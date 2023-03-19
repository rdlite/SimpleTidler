using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelState : IState
{
    private UIRoot _uiRoot;
    private GlobalStateMachine _globalSM;
    private Configs _configs;
    private ICoroutineRunner _coroutineRunner;

    public LoadLevelState(
        GlobalStateMachine globalStateMachine, ICoroutineRunner coroutineRunner, Configs configs,
        UIRoot uiRoot)
    {
        _uiRoot = uiRoot;
        _globalSM = globalStateMachine;
        _coroutineRunner = coroutineRunner;
        _configs = configs;
    }

    public void Enter()
    {
        _uiRoot.DeactivatePanels();
        LoadScene(1, InitScene);
    }

    private void LoadScene(int id, Action callback)
    {
        _coroutineRunner.StartCoroutine(SceneLoadingAsync(id, callback));
    }

    private IEnumerator SceneLoadingAsync(int id, Action callback)
    {
        if (SceneManager.GetSceneByBuildIndex(id).isLoaded)
        {
            SceneManager.UnloadSceneAsync(id);
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(id, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(id));
        callback?.Invoke();
    }

    private void InitScene()
    {
        MoneyFlowController moneyFlowController = new MoneyFlowController(_uiRoot);
        UpgradesController upgradesController = new UpgradesController(
            moneyFlowController, _uiRoot, _configs);
        _globalSM.StatesData.MoneyFlowController = moneyFlowController;
        _globalSM.StatesData.UpgradesController = upgradesController;
        moneyFlowController.Init();
        BulletFactory bulletFactory = new BulletFactory(_configs, upgradesController);

        PlayerTower playerTower = UnityEngine.Object.Instantiate(_configs.PrefabsContainer.PlayerTower);
        playerTower.transform.position = Vector3.zero;
        playerTower.transform.rotation = Quaternion.identity;
        playerTower.Init(
            _configs, bulletFactory, upgradesController,
            _uiRoot, _globalSM);
        _globalSM.StatesData.PlayerTower = playerTower;

        _globalSM.StatesData.Curtain.SetActive(false, false, null);
        _globalSM.Enter<BattleState>();
    }

    public void Exit() { }
}