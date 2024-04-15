using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    private PlayerController _playerController;
    private Collider2D _collider;

    public string key;
    public CollectableEvent onCollected;
    private bool _collected;
    
    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (_collected) return;
        if (collider2D.gameObject == _playerController.gameObject)
        {
            onCollected.Invoke(this);
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            _collected = true;
            StartCoroutine(DestroyWithDelay());
        }
    }
    
    IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(4.5f);
        Destroy(gameObject);
    }
}
[System.Serializable]
public class CollectableEvent : UnityEvent<Collectable>
{
}
