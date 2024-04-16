using System;
using System.Security.Cryptography.X509Certificates;
using Unity.Properties;
using UnityEngine;
using UnityEngine.Localization;

public class MapObjectContainer : MonoBehaviour, IInteractableMapObject, INamed
{
    public AudioClip openSound;
    public AudioClip closeSound;

    public Sprite closedSprite;
    public Sprite openedSprite;

    public LocalizedString Name;

    private SpriteRenderer spriteRenderer;

    public Action<MapObjectContainer> beforeOpen;

    [DontCreateProperty]
    private bool isOpen = false;

    [CreateProperty]
    public bool IsOpen { 
        get 
        { 
            return isOpen; 
        }
        set
        { 
            isOpen = value;
            setSprite();
        }
    }

    [CreateProperty]
    public ItemsBag ItemsBag { 
        get 
        {
            return gameObject.GetComponent<ItemsBag>();
        } 
    }

    string INamed.Name => Name.GetLocalizedString();

    public void Open()
    {
        if (beforeOpen != null)
        {
            beforeOpen(this);
        }
        IsOpen = true;
        if (openSound != null)
        {
            AudioSource.PlayClipAtPoint(openSound, this.transform.position);
        }
    }

    public void Close()
    {
        IsOpen = false;
        if (closeSound != null)
        {
            AudioSource.PlayClipAtPoint(closeSound, this.transform.position);
        }
    }

    public void Interact()
    {
        Open();
    }

    private void setSprite()
    {
        if (IsOpen)
        {
            if (openedSprite != null)
            {
                spriteRenderer.sprite = openedSprite;
            }
        }
        else
        {
            if (closedSprite != null) 
            { 
                spriteRenderer.sprite = closedSprite; 
            }
        }
    }

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
