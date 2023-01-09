using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [SerializeField] private GameObject bird;
    private Transform target_Transform;
    private Transform m_Transform;

    public AnimationCurve curve;
    public float duration = 1f;

    void Start()
    {
        target_Transform = bird.GetComponent<Transform>();
        m_Transform = GetComponent<Transform>();
    }

    void Update()
    {
        m_Transform.position = new Vector3(target_Transform.position.x , m_Transform.position.y, m_Transform.position.z);
    }

    //public void ShakeCamera()
    //{
    //    Vector3 startPosition = transform.position;
    //    float elapsedTime = 0f;

    //    while (elapsedTime < duration)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        float strength = curve.Evaluate(elapsedTime / duration);
    //        transform.position = startPosition + Random.insideUnitSphere * strength;
    //    }

    //    transform.position = startPosition;
    //}

    IEnumerator ShakeCamera()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.position = startPosition;
    }
}
