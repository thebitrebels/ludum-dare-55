using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    private PlayerController _playerController;
    private Collider2D _collider;

    public CollectableEvent onCollected;
    
    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject == _playerController.gameObject)
        {
            onCollected.Invoke(this);
            Destroy(gameObject);
        }
    }
}
[System.Serializable]
public class CollectableEvent : UnityEvent<Collectable>
{
}
