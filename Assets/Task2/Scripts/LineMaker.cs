using UnityEngine;
using DG.Tweening;

public class LineMaker : MonoBehaviour
{
    public float tweenDuration = .4f;

    private Camera mainCamera;
    private Vector3 firstPoint;
    private Vector3 secondPoint;
    private LineRenderer lineRenderer;

    private Color activeColor = new Color(0.3294118f, 1f, 0.2235294f, 1f);
    private Color inactiveColor = new Color(0.3294118f, 1f, 0.2235294f, 0f);

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        mainCamera = Camera.main;

        //Sets the component to have 2 points
        lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {

        //Here we must set both points to the same location to give the effect of
        //"Dragging" out the line.
        if (Input.GetMouseButtonDown(0))
        {
            firstPoint = GetWorldPosition(Input.mousePosition);
            secondPoint = firstPoint;
            UpdateLineRenderer();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            secondPoint = GetWorldPosition(Input.mousePosition);
            UpdateLineRenderer();
            CutCircles();
        }

        //Similarly check for touch
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            //Touch down
            if(touch.phase == TouchPhase.Began)
            {
                firstPoint = GetWorldPosition(Input.mousePosition);
                secondPoint = firstPoint;
                UpdateLineRenderer();
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                secondPoint = GetWorldPosition(Input.mousePosition);
                UpdateLineRenderer();
                CutCircles();
            }
        }
    }

    //Shoots a raycast, and removes any intersecting circle;
    private void CutCircles()
    {
        // Shoot a raycast from firstPoint to secondPoint
        Vector2 direction = secondPoint - firstPoint;
        RaycastHit2D[] hits = Physics2D.RaycastAll(firstPoint, direction, direction.magnitude);


        // For each intersecting circle, play an animation and then destroy
        foreach (var hit in hits)
        {
            if (hit.collider != null)
            {
                hit.transform.DOScale(Vector3.zero, .2f).OnComplete(()=> Destroy(hit.collider.gameObject));
            }
        }

        // Fade out Line Renderer
        Color2 visible = new Color2(activeColor, activeColor);
        Color2 invisible = new Color2(inactiveColor, inactiveColor);
        lineRenderer.DOColor(visible, invisible, tweenDuration);
    }

    private void UpdateLineRenderer()
    {
        //Fade in Line Renderer
        Color2 visible = new Color2(activeColor, activeColor);
        Color2 invisible = new Color2(inactiveColor, inactiveColor);
        lineRenderer.DOColor(invisible, visible, tweenDuration);

        if (lineRenderer == null)
            return;

        lineRenderer.SetPosition(0, firstPoint);
        lineRenderer.SetPosition(1, secondPoint);
    }

    //Helper function for cleaner code
    private Vector3 GetWorldPosition(Vector3 screenPosition)
    {
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0;  // Adjust z for 2D
        return worldPosition;
    }
}
