using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Scripts.Managers
{
	public class GameManager : MonoBehaviour
	{
        [SerializeField] private Transform spawnPos;
        [SerializeField] private CanvasGroup blackScreen;
		public GameObject Player;

		public static GameManager instance;
        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.

            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
        }

        private void Update()
        {
            HandleRespawn();
        }

        private void HandleRespawn()
        {
            bool tooFar = Vector2.Distance(Player.transform.position, spawnPos.position) > 500f;

            print(Vector2.Distance(Player.transform.position, spawnPos.position));

            if (tooFar)
            {
                Respawn();
            }
        }

        private void Respawn()
        {
            Player.transform.position = spawnPos.position;
        }

        private IEnumerator RespawnEffect()
        {
            yield return FadeScreen(blackScreen, 1f, 0f, 1f);

            yield return new WaitForSeconds(1f);

            yield return FadeScreen(blackScreen, 0f, 1f, 1f);
        }

        private IEnumerator FadeScreen(CanvasGroup canvasGroup, float startAlpha, float targetAlpha, float duration)
        {
            canvasGroup.alpha = startAlpha;

            yield return canvasGroup.DOFade(targetAlpha, duration).WaitForCompletion();
        }
    }
}
