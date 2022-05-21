using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace ProceduralModeling {

	[RequireComponent (typeof(MeshRenderer))]
	public class PTGrowing : MonoBehaviour {

		Material material;
		public float timeToFill;
		public float scalingSpeed;

		public float FX_playRateSpeed;
		public float FX_scalingSpeed;

		const string kGrowingKey = "_T";
		public VisualEffect VF;
		public SizeBinder VFsize;
		void OnEnable () {
			material = GetComponent<MeshRenderer>().material;
			material.SetFloat(kGrowingKey, 0f);

			VF.playRate = 0;
		}

		void Start () {
			transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

			StartCoroutine(IGrowing(timeToFill));
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

			if(transform.localScale.x  >= 0.5f && VF.playRate < 100)
            {
				VF.playRate += Time.deltaTime * FX_playRateSpeed;
			}

			if (VF.playRate > 0.02 && VFsize.effectSize < 4)
				VFsize.effectSize += Time.deltaTime * FX_scalingSpeed;
		}

		void OnDestroy() {
			if(material != null) {
				Destroy(material);
				material = null;
			}
		}

	}
		
}

