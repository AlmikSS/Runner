using System.Collections;
using TMPro;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _coinsText;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravityForce;
    [SerializeField] private float _angularSpeed;
    [SerializeField] private AudioSource _picupCoin;
    [SerializeField] private Vector3 _leftLinePos;
    [SerializeField] private Vector3 _midleLinePos;
    [SerializeField] private Vector3 _rightLinePos;

    private Vector3 _direction;
    private CharacterController _controller;
    public int Score;
    private int _coins;
    private int _currentLine = 1;
    
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        StartCoroutine(AddScore());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            if (_currentLine > 0)
                _currentLine--;

        if (Input.GetKeyDown(KeyCode.D))
            if (_currentLine < 2)
                _currentLine++;

        if (Input.GetKey(KeyCode.Space))
            Jump();

        _midleLinePos.z = transform.position.z;
        _rightLinePos.z = transform.position.z;
        _leftLinePos.z = transform.position.z;
        
        _midleLinePos.y = transform.position.y;
        _rightLinePos.y = transform.position.y;
        _leftLinePos.y = transform.position.y;
        
        if (_currentLine == 1)
            transform.position = Vector3.Lerp(transform.position, _midleLinePos, _angularSpeed);
        if (_currentLine == 0)
            transform.position = Vector3.Lerp(transform.position, _leftLinePos, _angularSpeed);
        if (_currentLine == 2)
            transform.position = Vector3.Lerp(transform.position, _rightLinePos, _angularSpeed);
    }

    private void FixedUpdate()
    {
        _direction.z = _speed;
        _direction.y += _gravityForce * Time.fixedDeltaTime;
        _controller.Move(_direction * Time.fixedDeltaTime);
    }

    private IEnumerator AddScore()
    {
        Score++;
        _scoreText.text = Score.ToString();
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(AddScore());
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Obstacle")
        {
            Debug.Log(hit.gameObject.transform.parent.name);
            Debug.Log(hit.gameObject.name);
            GameManager.Lost(Score);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WinTrigger"))
            GameManager.Win();
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            _picupCoin.Play();
            _coins++;
            _coinsText.text = _coins.ToString();
        }
    }

    private void Jump()
    {
        if (_controller.isGrounded)
        {
            _direction.y = _jumpForce;
        }
    }
}