using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashHit : MonoBehaviour
{
    // Class used for give an effect when someone got hit by something

    [SerializeField] private Material _whiteFlashMat;
    [SerializeField] private float _restoreDefaultMatTime = .1f;

    private Material _defaultMat;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        // In this class, when the game start, we are defining that the default material is the one that already is defined in our prefab.

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMat = _spriteRenderer.material;
    }

    public float GetRestoreMatTime()
    {
        return _restoreDefaultMatTime;
    }

    public IEnumerator FlashRoutine()
    {
        /* In this class we are stating a coroutine that:
         * # 1 -  Changes the Object Material to the one defined in the _whiteFlashMat variable
         * # 2 - This change is set to the time define at the variable _restoreDefaultMatTime
         * # 3 - The Object material is set back to normal (once we already defined it one the awake class.)
         */

        _spriteRenderer.material = _whiteFlashMat;
        yield return new WaitForSeconds(_restoreDefaultMatTime);
        _spriteRenderer.material = _defaultMat;
    }

}
