using UnityEngine;
using Random = System.Random;

namespace Project.Scripts.Fractures
{
    public class FractureThis : MonoBehaviour
    {
        private Anchor anchor = Anchor.Bottom;
        public int chunks = 10;
        public float density = 50;
        public float internalStrength = 100;
            
        private Material insideMaterial;
        private Material outsideMaterial;

        private Random rng = new Random();

        private void Start()
        {         
            outsideMaterial = gameObject.GetComponent<MeshRenderer>().materials[0];
            Debug.LogWarning(gameObject.name + " : " + outsideMaterial);

            if(gameObject.GetComponent<MeshRenderer>().materials.Length > 1)
                insideMaterial = gameObject.GetComponent<MeshRenderer>().materials[1];

            if (insideMaterial == null)
                insideMaterial = outsideMaterial;

            FractureGameobject();
            gameObject.SetActive(false);
        }

        public ChunkGraphManager FractureGameobject()
        {
            var seed = rng.Next();
            return Fracture.FractureGameObject(
                gameObject,
                anchor,
                seed,
                chunks,
                insideMaterial,
                outsideMaterial,
                internalStrength,
                density
            );
        }
    }
}