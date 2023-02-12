using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_lightningstrike : MonoBehaviour
{
    [SerializeField] float preStrikeTime;
    [SerializeField] float alphaPreStrike;

    [SerializeField] float StrikeTime;

    [SerializeField] Sprite[] sprites;
    [SerializeField] GameObject audioFX;

    BoxCollider2D trigger;
    SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        trigger = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    void Start()
    {
        _spriteRenderer.sprite = sprites[0];
        trigger.enabled = false;
        GameObject[] strikes = GameObject.FindGameObjectsWithTag("Lightning");
        foreach (GameObject strike in strikes)
        {
            print(strike.name);
            if (strike != this.gameObject)
            {
                Destroy(gameObject);
            }
        }
        StartCoroutine(Lightning());
    }

    IEnumerator Lightning()
    {
        yield return new WaitForSeconds(preStrikeTime);
        trigger.enabled = true;
        _spriteRenderer.sprite = sprites[1];
        Instantiate(audioFX, transform.position, transform.rotation);
        yield return new WaitForSeconds(StrikeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("LevelController").GetComponent<scr_levelstate>().OnPlayerHurt();
        }
    }
}
