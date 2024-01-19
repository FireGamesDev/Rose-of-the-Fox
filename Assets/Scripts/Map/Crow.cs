using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Map
{
	public class Crow : MonoBehaviour
	{
		[SerializeField] private Animator _anim;
		[SerializeField] private SpriteRenderer spriteRenderer;

        private bool flewAway = false;

        private IEnumerator Start()
        {
            spriteRenderer.flipX = Random.Range(0, 2) == 0 ? true : false;

            while (true)
            {
                yield return new WaitForSeconds(2);

                if (flewAway) yield break;

                if (Random.Range(0, 20) == 16)
                {
                    FlyAway();
                }

                switch (Random.Range(0, 5))
                {
                    case 0:
                        _anim.SetTrigger("eat");
                        break;
                    case 1:
                        _anim.SetTrigger("eat");
                        break;
                    case 2:
                        _anim.SetTrigger("jump");
                        break;
                    case 3:
                        spriteRenderer.flipX = !spriteRenderer.flipX;
                        break;
                    default:
                        break;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                FlyAway();
            }
        }

        private void FlyAway()
        {
            if (flewAway) return;

            flewAway = true;
            _anim.SetTrigger("fly");
            spriteRenderer.sortingLayerName = "Foreground";
            Destroy(gameObject, 3);
        }
    }
}
