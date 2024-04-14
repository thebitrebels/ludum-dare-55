
using UnityEngine;

public class Pig : MonoBehaviour
{
    public float pushStrength;
    public float pushDelay;
    public Vector3 offset;

    private bool _isPlayerGrabbed;
    private PlayerController _playerController;
    private float _timerPushDelay;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player == null) return;
        _playerController = player;
        _playerController.transform.position = transform.position + offset;
        _isPlayerGrabbed = true;
    }

    private void Update()
    {
        if (_isPlayerGrabbed)
        {
            _playerController.enabled = false;
            _timerPushDelay += Time.deltaTime;
            _playerController.transform.position = transform.position + offset;
            // add vibration here for booth
        }

        if (_timerPushDelay < pushDelay) return;
        
        // Push player and reset
        _playerController.enabled = true;
        _isPlayerGrabbed = false;
        _timerPushDelay = 0f;
        _playerController.GetComponent<Rigidbody2D>().AddForce(Vector2.up * pushStrength, ForceMode2D.Impulse);
    }
}
