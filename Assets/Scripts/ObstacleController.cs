using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private float GapHeight;
    private float GapMidpoint;
    private float TotalHeight;
    private SpriteRenderer UpColumn;
    private SpriteRenderer DownColumn;

    private GameplayManager gameplayManager;
    private UIController UIController;

    private AudioSource m_audioSource;

    void Start()
    {
        TotalHeight = GetComponent<BoxCollider2D>().size.y;
        DownColumn = transform.Find("DownColumn").gameObject.GetComponent<SpriteRenderer>();
        UpColumn = transform.Find("UpColumn").gameObject.GetComponent<SpriteRenderer>();
        UpdateObstacleParams();
        gameplayManager = FindObjectOfType<GameplayManager>();
        UIController = FindObjectOfType<UIController>();

        m_audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
    }

    private void UpdateObstacleParams()
    {
        GapMidpoint = Random.Range(3.4f, TotalHeight - 3.4f);
        GapHeight = Random.Range(2.5f, 4.5f);

        float MidPoint = Mathf.Clamp(GapMidpoint, 0, TotalHeight);
        float FullGapHeight = Mathf.Clamp(GapHeight, 0, TotalHeight);
        float DownHeight = MidPoint - FullGapHeight / 2;
        float UpHeight = TotalHeight - MidPoint - FullGapHeight / 2;

        UpColumn.size = new Vector2(UpColumn.size.x, UpHeight);
        DownColumn.size = new Vector2(DownColumn.size.x, DownHeight);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bird"))
        {
            gameplayManager.Score += 1;
            UIController.UpdateScoreText();
            m_audioSource.Play();
        }
    }
}
