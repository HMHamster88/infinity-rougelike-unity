using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public Collider2D wallsCollider;
    public float MaxInteractDistance = 1.5f;
    [SerializeField]
    private float speedMultiplier;
    public UnityEvent<MapObjectContainer> OnInteractWithContainer;

    private Rigidbody2D playerRigidBody;    
    private AttackController attackController;
    private MapBehaviour mapBehaviour;

    public void Attack(Vector3 mouseWorldPoint)
    {
        attackController.Attack(mouseWorldPoint);
    }

    public void InteractWithMapObjects(Vector3 mouseWorldPoint)
    {
        var player = this.gameObject;
        var playerPosition = player.transform.position;

        var lineHits = Physics2D.LinecastAll(playerPosition, mouseWorldPoint);

        var colliders = lineHits.Select(hit => hit.collider).ToList();

        if (!colliders.Contains(wallsCollider))
        {
            foreach (var collider in colliders)
            {
                var interactable = collider.gameObject.GetComponent<IInteractableMapObject>();
                if (interactable != null)
                {
                    var clickedObjectPosition = collider.gameObject.transform.position;
                    var distance = ((Vector2)playerPosition - (Vector2)clickedObjectPosition).magnitude;
                    if (distance < MaxInteractDistance)
                    {
                        interactable.Interact();
                        if (interactable as MapObjectContainer)
                        {
                            OnInteractWithContainer.Invoke(interactable as MapObjectContainer);
                        }
                    }
                }
            }
        }
        mapBehaviour.InteractWithMapObjects(playerPosition, mouseWorldPoint, MaxInteractDistance);
    }

    public void SetWalkDirection(Vector2 direction)
    {
        playerRigidBody.velocity = direction * speedMultiplier;
    }

    void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        attackController = GetComponent<AttackController>();
        mapBehaviour = wallsCollider.GetComponentInParent<MapBehaviour>();
    }

    public void SetStartPosition(Vector2 position)
    {
        playerRigidBody.position = position;
    }
}
