using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMoveSystem : MonoBehaviour
{
    [SerializeField] private GameObject _player;


    public float Path = 1.5f;
    public float TimeToMove = 1f;

    public Vector3 PlayerPosition => _player.transform.position;
    private Sequence dgSeq;
    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        PauseManager.onPauseStateUpdated += DoPause;
    }

    private void OnDisable()
    {
        PauseManager.onPauseStateUpdated += DoPause;
    }

    public void DoPause(bool state)
    {
        dgSeq.TogglePause();
    }
    public void Init()
    {
        transform.position = new Vector3(Path, transform.position.y, transform.position.z);
        StartMoving();
        
    }

    public void StartMoving()
    {
        dgSeq = DOTween.Sequence()
            .Append(_player.transform.DOMoveX(-Path, TimeToMove))
            .Append(_player.transform.DOMoveX(Path, TimeToMove))
            .SetLoops(-1);

        if (SceneManager.GetActiveScene().name == "Gameplay") dgSeq.TogglePause();
    }
}
