public class StartGameState : IState
{
    private GlobalStateMachine _globalSM;

    public StartGameState(GlobalStateMachine globalSM)
    {
        _globalSM = globalSM;
    }

    public void Enter()
    {
        _globalSM.StatesData.Curtain.SetActive(true, true, null);
        _globalSM.Enter<LoadLevelState>();
	}

    public void Exit() { }
}
