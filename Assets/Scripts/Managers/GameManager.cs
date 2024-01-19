using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Managers
{
	public class GameManager : MonoBehaviour
	{
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
	}
}
