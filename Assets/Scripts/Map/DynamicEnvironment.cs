using Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Map
{
    public class DynamicEnvironment : MonoBehaviour
    {
        private Animator _anim;
        [SerializeField] private GameObject sfxPlayer;
        [SerializeField] private AudioClip sfx;

        private void Start()
        {
            _anim = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _anim.SetTrigger("move");

                PlaySFX();
            }
        }

        private void PlaySFX()
        {
            Instantiate(sfxPlayer, transform).GetComponent<SFXPlayer>().PlaySFX(sfx);
        }
    }
}
