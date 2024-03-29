using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ListItem : MonoBehaviour
{
    [Header("References")]
    public Text labelObject;
    public Text pointsObject;
    public GameObject popupPrefab;

    [Header("Animation Settings")]
    public float fadeInDuration = .5f;
    public float popOutDuration = .5f;
    public float delay = .2f;

    [HideInInspector]
    public ClientData clientData;

    private void Start()
    {
        FadeIn();
    }

    void FadeIn()
    {
        if (TryGetComponent(out CanvasGroup canvasGroup))
        {
            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1, .5f).SetDelay(delay);
        }

    }

    //Call when destroying listItem
    public void PopOut()
    {
        if (TryGetComponent(out RectTransform rectTransform))
        {
            //We want the width to remain the same, but the height to transition to 0
            
            Vector2 target = GetComponent<RectTransform>().sizeDelta;
            target.y = 0;

            rectTransform.DOSizeDelta(target, popOutDuration).OnComplete(()=> { 
                if(gameObject!=null)
                    Destroy(gameObject); 
            });
        }
    }

    public void CreatePopUp()
    {
        //Parent.parent refers to the Canvas Object
        GameObject popUp = Instantiate(popupPrefab, transform.parent.parent);
        popUp.GetComponent<PopupController>().clientData = clientData;
    }

    //This function is called when instantiating,
    //Each item has incrementally more delay, makes the animation to look more polished
    public void SetDelay(float d)
    {
        delay = d;
    }
}
