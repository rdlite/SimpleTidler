using System;
using System.Collections.Generic;

public class GlobalStateMachine : UpdateStateMachine
{
    public BetweenStatesData StatesData;

    public GlobalStateMachine(
        CameraController camera, UIRoot uiRoot, ICoroutineRunner coroutineRunner,
        Configs configs, Curtain curtain)
    {
        StatesData = new BetweenStatesData();
        StatesData.Camera = camera;
        StatesData.Curtain = curtain;

        uiRoot.Init(configs, this);

        _states = new Dictionary<Type, IExitableState>()
        {
            [typeof(StartGameState)] = new StartGameState(
                this),
            [typeof(LoadLevelState)] = new LoadLevelState(
                this, coroutineRunner, configs,
                uiRoot),
            [typeof(BattleState)] = new BattleState(
                this, configs, uiRoot),
            [typeof(LoseState)] = new LoseState(
                this, configs, coroutineRunner,
                uiRoot),
        };
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
}

public class BetweenStatesData
{
    public CameraController Camera;
    public MoneyFlowController MoneyFlowController;
    public UpgradesController UpgradesController;
    public PlayerTower PlayerTower;
    public Curtain Curtain;
}