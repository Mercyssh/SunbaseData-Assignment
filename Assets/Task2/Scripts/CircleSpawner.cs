using UnityEngine;
using DG.Tweening;

public class CircleSpawner : MonoBehaviour
{

    [Header("Spawning")]
    public GameObject circlePrefab;
    public int countRangeMin=5;
    public int countRangeMax=10;

    [Header("Animations")]
    public float tweenDuration = .3f;
    public float tweenOverShoot = 1.5f;
    public float tweenDelay = .2f;

    private void Start()
    {
        SpawnCircles();
    }

    public void SpawnCircles()
    {
        int count = Random.Range(countRangeMin, countRangeMax);

        for(var i=0; i<count; i++)
        {
            GameObject circle = Instantiate(circlePrefab);

            // Random screen position
            Vector3 randomScreenPosition = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 0);

            // Convert screen position to world position
            Vector3 randomWorldPosition = Camera.main.ScreenToWorldPoint(randomScreenPosition);

            // Set z to 0 to make sure it's not positioned away from the camera
            randomWorldPosition.z = 0; 

            // Set the position of the circle
            circle.transform.position = randomWorldPosition;

            // Animate the circles
            circle.transform.localScale = Vector3.zero;
            circle.transform.DOScale(Vector3.one, .2f).SetEase(Ease.InElastic, tweenOverShoot).SetDelay(i * tweenDelay);
        }
    }
}
