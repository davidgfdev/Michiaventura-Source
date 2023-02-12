using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_climasystem : MonoBehaviour
{
    public enum clima
    {
        clear,
        rain,
        storm
    }

    [SerializeField]
    clima LevelClima;

    [SerializeField] float lightningRate;
    [SerializeField] float lightningRange;
    [SerializeField] ParticleSystem rain;
    [SerializeField] GameObject lightningStrike;
    bool storm;

    private void Start()
    {
        UpdateClima();
    }

    public void UpdateClima()
    {
        switch (LevelClima)
        {
            case clima.clear:
                rain.Stop();
                storm = false;
                break;
            case clima.rain:
                rain.Play();
                storm = false;
                break;
            case clima.storm:
                rain.Play();
                storm = true;
                StartCoroutine(StormThunders());
                break;
        }
    }

    IEnumerator StormThunders()
    {
        while (storm)
        {
            float calculatedRange = lightningRange / GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity.sqrMagnitude;
            calculatedRange = Mathf.Clamp(calculatedRange, 7, lightningRange);
            Vector3 lightningPos = calcStrikePosition(calculatedRange);
            Instantiate(lightningStrike, lightningPos, lightningStrike.transform.rotation);
            yield return new WaitForSeconds(lightningRate);
        }
    }

    Vector2 calcStrikePosition(float range)
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        float minX = player.position.x - range;
        float maxX = player.position.x + range;

        float XPos = Random.Range(minX, maxX);
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(XPos, player.position.y + lightningRange), Vector2.down);

        Vector2 finalPos = new Vector2(hit.point.x, hit.point.y);

        return finalPos;
    }

    public clima getCurrentClima()
    {
        return LevelClima;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GameObject.FindGameObjectWithTag("Player").transform.position, lightningRange);
    }

    public void alternateStorm(){
        LevelClima = storm ? clima.clear : clima.storm;
        UpdateClima();
    }
}
