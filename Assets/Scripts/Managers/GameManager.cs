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
        [SerializeField] private GameObject sfxPlayer;
        [SerializeField] private AudioClip respawnSFX;
        [SerializeField] private AudioSource mainAmbience;
		public GameObject Player;

		public static GameManager instance;

        private Coroutine routine;

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

            MakeThemeLouderAsThePlayerGetsFurther();
        }

        private void MakeThemeLouderAsThePlayerGetsFurther()
        {
            float distance = Vector2.Distance(Player.transform.position, spawnPos.position);

            mainAmbience.volume = Mathf.Lerp(0f, 1f, distance / 50);
        }

        private void HandleRespawn()
        {
            bool tooFar = Vector2.Distance(Player.transform.position, spawnPos.position) > 35f;

            if (tooFar)
            {
                if (routine == null)
                {
                    routine = StartCoroutine(RespawnEffect());
                }         
            }
        }

        private void Respawn()
        {
            Player.transform.position = spawnPos.position;
        }

        private IEnumerator RespawnEffect()
        {
            PlayRespawnSFX();
            yield return FadeScreen(blackScreen, 0f, .9f, .2f);
            
            for (int i = 0; i < 4; i++)
            {
                yield return FadeScreen(blackScreen, .9f, .6f, .2f);
                yield return FadeScreen(blackScreen, .6f, .9f, .2f);
            }
            
            yield return FadeScreen(blackScreen, 0.9f, 1f, .2f);

            Respawn();

            yield return FadeScreen(blackScreen, 1f, 0f, 2f);

            routine = null;
        }

        private void PlayRespawnSFX()
        {
            Instantiate(sfxPlayer, transform).GetComponent<SFXPlayer>().PlaySFXWithVolume(respawnSFX, 0.5f);
        }

        private IEnumerator FadeScreen(CanvasGroup canvasGroup, float startAlpha, float targetAlpha, float duration)
        {
            canvasGroup.alpha = startAlpha;

            yield return canvasGroup.DOFade(targetAlpha, duration).WaitForCompletion();
        }
    }
}
