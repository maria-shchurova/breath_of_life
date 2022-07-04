using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Rendering;

namespace ProceduralModeling {

	public class PTGarden : MonoBehaviour {

		[SerializeField] Camera cam;
		public List<GameObject> prefabs;
		[SerializeField] List<Vector3> TreesOnScene  = new List<Vector3>();
		[SerializeField] float MinimalDistanceBetweenTrees;
		[SerializeField] float clickingCooldown; //to  avoid spamming
		float timeSinceClick; //to  avoid spamming
		bool readyToPlant = true; //to  avoid spamming
		bool CanGrow;
		bool isFirst = true;
		GameObject HintOnTheScene;
		[SerializeField] Vector2 scaleRange = new Vector2(1f, 1.2f);

		const string SHADER_PATH = "Hidden/Internal-Colored";

		Material lineMaterial = null;
		MeshCollider col = null;
		Vector3[] vertices;
		int[] triangles;

		bool hit;
		Vector3 point;
		Vector3 normal;
		Quaternion rotation;

		[Header("Tutorial")]
		[SerializeField] GameObject PlantArea;

		void Update () {
			timeSinceClick += Time.deltaTime;
			if (timeSinceClick >= clickingCooldown)
            {
				readyToPlant = true;
			}

			var mouse = Input.mousePosition;
			var ray = cam.ScreenPointToRay(mouse);
			RaycastHit info;
			hit = col.Raycast(ray, out info, float.MaxValue);
			if(hit) {
				point = info.point;
				var t = info.triangleIndex * 3;
				var a = triangles[t];
				var b = triangles[t + 1];
				var c = triangles[t + 2];
				var va = vertices[a];
				var vb = vertices[b];
				var vc = vertices[c];
				normal = transform.TransformDirection(Vector3.Cross(vb - va, vc - va));
				rotation = Quaternion.LookRotation(normal);
			}

			if(Input.GetMouseButtonUp(0) && hit && readyToPlant) {

				if (TreesOnScene.Count > 3)
				{
					if(HintOnTheScene)
					Destroy(HintOnTheScene);// after the 3rd tree hint disapperas
				}

				if (isFirst)
                {
					timeSinceClick = 0;
					readyToPlant = false;
					var go = Instantiate(prefabs[Random.Range(0, prefabs.Count)]) as GameObject;
					if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name ==  "Intro")
                    {
						var hint = Instantiate(PlantArea, point + Vector3.up, PlantArea.transform.rotation);
						HintOnTheScene = hint;
					}
					go.transform.position = point;
					go.transform.localScale = Vector3.one * Random.Range(scaleRange.x, scaleRange.y);
					go.transform.localRotation = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up);
					//check hit area conditions

					//
					var tree = go.GetComponent<ProceduralTree>();
					tree.isOnGround = CompareTag("Ground");
					tree.Data.randomSeed = Random.Range(0, 300);
					isFirst = false;
				}
				else
                {
					foreach (Vector3 t in TreesOnScene.ToList())
					{
						if (Vector3.Distance(point, t) > MinimalDistanceBetweenTrees) //if  at least 1  of tips  is close  to  the hit point
						{
							CanGrow = true;
						}
					}

					if (CanGrow)
					{
						timeSinceClick = 0;
						readyToPlant = false;
						var go = Instantiate(prefabs[Random.Range(0, prefabs.Count)]) as GameObject;
						go.transform.position = point;
						go.transform.localScale = Vector3.one * Random.Range(scaleRange.x, scaleRange.y);
						go.transform.localRotation = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up);
						//check hit area conditions

						//
						var tree = go.GetComponent<ProceduralTree>();
						tree.isOnGround = CompareTag("Ground");
						tree.Data.randomSeed = Random.Range(0, 300);

						CanGrow = false;
					}
				}

			}

		}

		const int resolution = 16;
		const float pi2 = Mathf.PI * 2f;
		const float radius = 0.5f;
		Color color = new Color(0.6f, 0.75f, 1f);


		public void AddTreeToList(Vector3 tree)
        {
			TreesOnScene.Add(tree);
        }
		void OnRenderObject () {
			if(!hit) return;

			CheckInit();

			lineMaterial.SetPass(0);
			lineMaterial.SetInt("_ZTest", (int)CompareFunction.Always);

			GL.PushMatrix();
			GL.Begin(GL.LINES);
			GL.Color(color);

			for(int i = 0; i < resolution; i++) {
				var cur = (float)i / resolution * pi2;
				var next = (float)(i + 1) / resolution * pi2;
				var p1 = rotation * new Vector3(Mathf.Cos(cur), Mathf.Sin(cur), 0f);
				var p2 = rotation * new Vector3(Mathf.Cos(next), Mathf.Sin(next), 0f);
				GL.Vertex(point + p1 * radius);
				GL.Vertex(point + p2 * radius);
			}

			GL.End();
			GL.PopMatrix();
		}

		void OnEnable () {
			col = GetComponent<MeshCollider>();
			var mesh = GetComponent<MeshFilter>().sharedMesh;
			vertices = mesh.vertices;
			triangles = mesh.triangles;
		}

		void CheckInit () {
			if(lineMaterial == null) {
				Shader shader = Shader.Find(SHADER_PATH);
				if (shader == null) return;
				lineMaterial = new Material(shader);
				lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
		}

	}
	
}

