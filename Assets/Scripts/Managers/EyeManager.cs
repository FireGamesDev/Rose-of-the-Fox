using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Scripts.Managers
{
	public class EyeManager : MonoBehaviour
	{
        private GameObject player;
        [SerializeField] private Animator _anim;
        [SerializeField] private GameObject eye;

        private SpriteRenderer eyeSprite;

        private float minScale = 1f;
        private float maxScale = 4.2f;
        private float minDistance = 2f;
        private float maxDistance = 17.5f;
        private float blinkInterval = 1f;

        private float scaleVariety;

        private void Start()
        {
            player = GameManager.instance.Player;

            eyeSprite = eye.GetComponent<SpriteRenderer>();

            scaleVariety = Random.Range(1f, 2f);

            transform.localScale = Vector3.one * scaleVariety;

            InvokeRepeating(nameof(Blink), 2f, blinkInterval);
        }

        private void Update()
        {
            float distance = Vector2.Distance(player.transform.position, transform.position);

            float scale = Mathf.Lerp(minScale, maxScale, 1 - Mathf.InverseLerp(minDistance, maxDistance, distance)) * scaleVariety;
            transform.localScale = Vector3.one * scale;

            float lerpValue = Mathf.InverseLerp(minDistance, maxDistance, distance);

            Color lerpedColor = Color.Lerp(Color.red, Color.yellow, lerpValue);
            eyeSprite.color = lerpedColor;

            if (distance > 5)
            {
                Vector3 dir = player.transform.position - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                angle -= 90;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        private void Blink()
        {
            if (Random.Range(0, 4) == 2)
            {
                _anim.SetTrigger("Blink");
            }
        }
    }
}
