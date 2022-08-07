using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdJump : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsuleCollider;
    AudioSource audioSource;
    public AudioClip audioDie;

    public float jumpPower;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<AudioSource>().Play();
            rb.velocity = Vector2.up * jumpPower;// (0,3)
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    //Sprite Alpha
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //Sprite Flip Y
        spriteRenderer.flipY = true;
        //Collider Disable
        capsuleCollider.enabled = false;
        //Die Effect Jump
        rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        {

        }

        if (Score.score > Score.bestScore)
        {
            Score.bestScore = Score.score;
        }
        PlaySound("DIE");
        StartCoroutine(loadScene());// 코루틴을 이용해서 1초후에 게임오버 씬을 불러옴
        IEnumerator loadScene()
        {
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene("GameOverScene");
        }
    }
    void PlaySound(string action)
    {
        switch (action)
        {
            case "DIE":
                audioSource.clip = audioDie;
                break;
        }
        audioSource.Play();
    }
}
