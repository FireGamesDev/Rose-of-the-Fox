using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Managers
{
	public class EyeManager : MonoBehaviour
	{
        private GameObject player;
        [SerializeField] private Animator _anim;
        [SerializeField] private GameObject eye;

        private void Start()
        {
            player = GameManager.instance.Player;

            eye.transform.localScale *= Random.Range(1f, 2f);

            StartCoroutine(SetRed());

            InvokeRepeating(nameof(Blink), 2f, 1f);
        }

        private void Update()
        {
            if (Vector2.Distance(player.transform.position, transform.position) > 5)
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

        private IEnumerator SetRed()
        {
            yield return new WaitForSeconds(5.5f);
            eye.GetComponent<SpriteRenderer>().color = Color.red;
            _anim.SetTrigger("Grow");
        }
    }
}
