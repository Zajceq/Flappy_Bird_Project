using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdController : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    public float Force = 300.0f;
    public float Velocity = 5.0f;
    public Quaternion TargetUpRotation;
    public Quaternion TargetDownRotation;
    public float TargetRotationAmount = 30.0f;
    public float RotationSpeed = 50.0f;

    private AudioSource m_audioSource;
    private MainCameraController m_camera;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        TargetUpRotation = Quaternion.Euler(0, 0, TargetRotationAmount);
        TargetDownRotation = Quaternion.Euler(0, 0, -TargetRotationAmount);

        m_audioSource = GetComponent<AudioSource>();
        m_camera = FindObjectOfType<MainCameraController>();
    }

    void Update()
    {
        Fly();
        SetRotation();
    }

    private void FixedUpdate()
    {
        m_rigidbody.velocity = new Vector2(Velocity, m_rigidbody.velocity.y); //set the constant right movement for the bird equal Velocity
    }

    private void Fly()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_rigidbody.AddForce(Vector2.up * Force);
            m_audioSource.Play();
        }
    }

    private void SetRotation()
    {
        if (m_rigidbody.velocity.y > 0)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetUpRotation, Time.deltaTime * RotationSpeed);
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetDownRotation, Time.deltaTime * RotationSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Obstacle") || collision.collider.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            m_camera.StartCoroutine("ShakeCamera");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            StartCoroutine(RestartScene());
        }
    }

    IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
