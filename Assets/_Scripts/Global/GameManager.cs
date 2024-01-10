using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _lostMenu;
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private GameObject _gameplayMenu;
    [SerializeField] private TMP_Text _lostText;
    [SerializeField] private TMP_Text _recordText;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private GameObject _player;
    [SerializeField] private LevelGenerator _generator;
    private int _record = 0;
    private static GameManager _instance;

    private void Start()
    {
        Restart();
        _instance = this;
        _gameplayMenu.SetActive(true);
        _lostMenu.SetActive(false);
        _winMenu.SetActive(false);
    }

    public static void Lost(int score)
    {    
        _instance._gameplayMenu.SetActive(false);
        _instance._lostMenu.SetActive(true);
        _instance._player.GetComponent<PlayerController>().enabled = false;
        _instance._player.GetComponent<CharacterController>().enabled = false;
        if (score > _instance._record)
            _instance._record = score;
        _instance._recordText.text = $"Highest: {_instance._record}";
        _instance._lostText.text = $"Score: {score}";
    }

    public static void Win()
    {
        _instance._gameplayMenu.SetActive(false);
        _instance._winMenu.SetActive(true);
        _instance._player.GetComponent<PlayerController>().enabled = false;
        _instance._player.GetComponent<CharacterController>().enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_lostMenu.activeSelf || _winMenu.activeSelf)
            {
                Restart();
            }
        }
    }

    public void Restart()
    {
        _generator.Regenarate();
        _gameplayMenu.SetActive(true);
        _lostMenu.SetActive(false);
        _winMenu.SetActive(false);
        _player.transform.position = _startPosition.position;
        _player.GetComponent<CharacterController>().enabled = true;
        _player.GetComponent<PlayerController>().enabled = true;
        _player.GetComponent<PlayerController>().Score = 0;
    }
}