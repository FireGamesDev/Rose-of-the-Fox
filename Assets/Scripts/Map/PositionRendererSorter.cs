using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Map
{
	public class PositionRendererSorter : MonoBehaviour
	{
		[SerializeField] private int sortingOrderBase = 5000;
		[SerializeField] private float offset = 0;
		[SerializeField] private bool runOnlyOnce = false;

		private float timer = 0f;
		private float timerMax = .1f;

		private SpriteRenderer myRenderer;

        private void Awake()
        {
			myRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        private void LateUpdate()
        {
			timer -= Time.deltaTime;
			if (timer <= 0f)
            {
				timer = timerMax;

				myRenderer.sortingOrder = (int)(sortingOrderBase - transform.position.y - offset);
				if (runOnlyOnce) { Destroy(this); }
			}
		}
    }
}
