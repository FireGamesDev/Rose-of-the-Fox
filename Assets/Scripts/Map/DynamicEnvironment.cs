using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Map
{
    public class DynamicEnvironment : MonoBehaviour
    {
        private Animator _anim;

        private void Start()
        {
            _anim = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _anim.SetTrigger("move");
            }
        }
    }
}
