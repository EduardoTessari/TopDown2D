using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparentDetection : MonoBehaviour
{

    //this class is used to add the effect of transparencry when we enter somewhere that would block the player's vision

    [SerializeField] private float _transparencyAmount = 0.8f;
    [SerializeField] private float _fadeTime = .04f;


    private SpriteRenderer _spriteRenderer;
    private Tilemap _tileMap;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _tileMap = GetComponent<Tilemap>();
    }

    // this action confers if the player object is colliding with a sprite or a tilemap and start the coroutine that changes the asset transparecy.
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerControler>())
        {
            if(_spriteRenderer)
            {
                StartCoroutine(FadeRoutine(_spriteRenderer, _fadeTime, _spriteRenderer.color.a, _transparencyAmount));
            }
            if (_tileMap)
            {
                StartCoroutine(FadeRoutine(_tileMap, _fadeTime, _tileMap.color.a, _transparencyAmount));
            }

        }
    }

    // this action confers if the player object is over colliding with a sprite or a tilemap and start the coroutine that changes the asset transparecy.
    private void OnTriggerExit2D (Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerControler>())
        {
            if (_spriteRenderer)
            {
                StartCoroutine(FadeRoutine(_spriteRenderer, _fadeTime, _spriteRenderer.color.a, 1f));
            }
            if (_tileMap)
            {
                StartCoroutine(FadeRoutine(_tileMap, _fadeTime, _tileMap.color.a, 1f));
            }

        }
    }

    // this action start the couroutine to change the transparency to a more transparente one for those assets classified as sprite.
    private IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float targetTransparency)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }
    }

    // this action start the couroutine to change the transparency to a more transparente one for those assets classified as Tilme Map.

    private IEnumerator FadeRoutine(Tilemap tilemap, float fadeTime, float startValue, float targetTransparency)
        {
            float elapsedTime = 0;
            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
                tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
                yield return null;
            }
        }
}
