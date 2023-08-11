using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopupController : MonoBehaviour
{
    [Header("References")]
    public Image backgroundFade;
    public GameObject popUpWindow;
    
    [Header("Labels")]
    public Text nameLabel;
    public Text addressLabel;
    public Text pointsLabel;

    [Header("Animation Settings")]
    public float tweenDuration = .5f;
    public float tweenOvershoot = 1.5f;

    //This value needs to be updated before showing popup
    public ClientData clientData;

    public void Start()
    {
        ShowPopup();
    }

    public void ShowPopup()
    {
        if (clientData == null) return;

        //Temporarily set the background color opacity to 0, then fade upto .9f
        Color c = backgroundFade.color;
        c.a = 0f;
        backgroundFade.color = c;
        backgroundFade.DOFade(.9f, tweenDuration);

        //Temporarily set scale to 0, then tween up to 1
        popUpWindow.transform.localScale = Vector3.zero;
        popUpWindow.transform.DOScale(Vector3.one, tweenDuration).SetEase(Ease.OutBounce, tweenOvershoot);

        nameLabel.text = "Name : "+clientData.name;
        addressLabel.text = "Address : "+clientData.address;
        pointsLabel.text = "Points : "+clientData.points;

    }

    public void ClosePopup()
    {
        backgroundFade.DOFade(0f, tweenDuration);
        popUpWindow.transform.DOScale(Vector3.zero, tweenDuration).SetEase(Ease.InBack, tweenOvershoot).OnComplete(() =>
        {
            if(gameObject!=null)
            Destroy(gameObject);
        });
    }
}
