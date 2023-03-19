using UnityEngine;
using UnityEngine.EventSystems;

public class Bootstrap : MonoBehaviour, ICoroutineRunner
{
    [SerializeField] private Curtain _curtain;
    [SerializeField] private Configs _configs;
    [SerializeField] private UIRoot _uiRoot;
    [SerializeField] private CameraController _camera;
    [SerializeField] private EventSystem _eventSystem;

    private GlobalStateMachine _globalSM;

    private void Awake()
    {
        _uiRoot = Instantiate(_uiRoot);
        _camera = Instantiate(_camera);
        _curtain = Instantiate(_curtain);
        Instantiate(_eventSystem);

        _curtain.Init(this);

        _globalSM = new GlobalStateMachine(
            _camera, _uiRoot, this,
            _configs, _curtain);
        _globalSM.Enter<StartGameState>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = Time.timeScale == 5f ? 1f : 5f;
        }

        _globalSM.UpdateState();
    }

    private void FixedUpdate()
    {
        _globalSM.FixedUpdateState();
    }

    private void LateUpdate()
    {
        _globalSM.LateUpdateState();
    }
}