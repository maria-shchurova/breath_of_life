using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralModeling {

	[RequireComponent (typeof(MeshRenderer))]
	public class PTGrowing : MonoBehaviour {

		Material material;
		public float fillingSpeed;
		public float scalingSpeed;

		const string kGrowingKey = "_T";

		void OnEnable () {
			material = GetComponent<MeshRenderer>().material;
			material.SetFloat(kGrowingKey, 0f);

		}

		void Start () {
			transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

			StartCoroutine(IGrowing(fillingSpeed));
		}

		IEnumerator IGrowing(float duration) {
			yield return 0;
			var time = 0f;
			while(time < duration) { 
				yield return 0;
				material.SetFloat(kGrowingKey, time / duration);
				time += Time.deltaTime;
			}
			material.SetFloat(kGrowingKey, 1f);
			gameObject.AddComponent<MeshCollider>();
		}

        private void Update()
        {
			if (transform.localScale.x <= 1)
			{
				transform.localScale += new Vector3(scalingSpeed * Time.deltaTime, scalingSpeed * Time.deltaTime, scalingSpeed * Time.deltaTime);
			}

		}

		void OnDestroy() {
			if(material != null) {
				Destroy(material);
				material = null;
			}
		}

	}
		
}

