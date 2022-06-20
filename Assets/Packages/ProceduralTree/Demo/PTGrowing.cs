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
		ProceduralTree thisTree;
		public VisualEffect[] VF;
		public SizeBinder[] VFsize;

		[SerializeField] float foliageSize = 4;
		[SerializeField] int foliageDensityType; //index for VXF asset since I can not control particle capacity
		[SerializeField] float foliageY = 0;

		[Header("Breaking force")]
		[SerializeField] float mass = 1.5f;
		[SerializeField] float raduis = 0.05f;
		[SerializeField] float force = 0;
		[SerializeField] float maxForce = 5.5f;

		[SerializeField] float timeInterval = 5;
		public VisualEffectAsset[] VFXpresets;

		PTGarden mainObject;
		void OnEnable () {
			material = GetComponent<MeshRenderer>().material;
			thisTree = GetComponent<ProceduralTree>();
			material.SetFloat(kGrowingKey, 0f);
			foreach (VisualEffect vf in VF)
            {
				vf.playRate = 0;
			}			
		}

		void Start () {
			mainObject = FindObjectOfType<PTGarden>();
			mainObject.AddTreeToList(transform.position);

			transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
			switch(thisTree.TreeType)
            {
				case ProceduralTree.TreeTypes.LitOnGround:
					foliageSize = 8;
					foliageDensityType = 0;
					foliageY = 4;
					break;
				case ProceduralTree.TreeTypes.LitNoGround:
					foliageSize = 1f;
					foliageDensityType = 1;
					foliageY = -2;
					break;
				case ProceduralTree.TreeTypes.UnlitOnGround:
					foliageSize = 4;
					foliageDensityType = 2;
					foliageY = 0;
					break;
				case ProceduralTree.TreeTypes.UnlitNoGound:
					foliageSize = 0f;
					foliageDensityType = 3;
					foliageY = -2;
					break;
			}
			foreach (VisualEffect vf in VF)
			{
				vf.transform.localPosition = new Vector3(vf.transform.localPosition.x, foliageY, vf.transform.localPosition.z);
				vf.visualEffectAsset = VFXpresets[foliageDensityType];
			}

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

			createBreaker(5);
		}

		void  createBreaker(int  amount)
        {
			for(int i = 0; i  <=amount; i++)
            {
				var height = GetComponent<ProceduralTree>().length;

				var breaker = Instantiate(new GameObject(), transform.position + Vector3.up * height, Quaternion.identity);
				Vector3 direction = Vector3.up;
				var destroyer = breaker.AddComponent<SlowForce>();
				destroyer.Init(mass, raduis, force, maxForce, timeInterval, direction);
			}

		}

        private void Update()
        {
			if (transform.localScale.x <= 1)
			{
				transform.localScale += new Vector3(scalingSpeed * Time.deltaTime, scalingSpeed * Time.deltaTime, scalingSpeed * Time.deltaTime);
			}

			if(transform.localScale.x  >= 0.5f && VF[0].playRate < 100)
            {
				foreach (VisualEffect vf in VF)
				{
					vf.playRate += Time.deltaTime * FX_playRateSpeed;
				}
			}

			if (VF[0].playRate > 0.02 && VFsize[0].effectSize < foliageSize)
            {
				foreach (SizeBinder sb in VFsize)
				{
					sb.effectSize += Time.deltaTime * FX_scalingSpeed;
				}
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

