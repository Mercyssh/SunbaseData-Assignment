using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ListItem : MonoBehaviour
{
    [Header("References")]
    public Text LabelObject;
    public Text PointsObject;

    [Header("Animation Settings")]
    public float fadeInDuration = .5f;
    public float popOutDuration = .5f;
    public float delay = .2f;

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

    //This function is called when instantiating,
    //Each item has incrementally more delay, makes the animation to look more polished
    public void SetDelay(float d)
    {
        delay = d;
    }
}
