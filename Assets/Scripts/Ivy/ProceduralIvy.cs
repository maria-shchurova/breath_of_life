using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProceduralIvy : MonoBehaviour {

    public Camera cam;
    [Space]
    public float recycleInterval = 30;
    [Space]
    public int branches = 3;
    public int maxPointsForBranch = 20;
    public float segmentLength = .002f;
    public float branchRadius = 0.02f;
    [Space]
    public Material branchMaterial;
    public Material[] leafMaterials;
    public Material[] flowerMaterials;
    [Space]
    public Blossom leafPrefab;
    public Blossom flowerPrefab;
    [Space]
    public bool wantBlossoms;
    int ivyCount = 0;

    [SerializeField] List<Vector3> branchesTips  =  new List<Vector3>();

    [Header("Breaking force")]
    [SerializeField] float mass = 1.5f;
    [SerializeField] float raduis = 0.05f;
    [SerializeField] float force = 0;
    [SerializeField] float maxForce = 5.5f;

    [SerializeField] float timeInterval = 5;
    [SerializeField] GameObject impactEffect;


    [SerializeField] float MaxDistanceBetweenSprouts;
    [SerializeField] bool isFirst =  true;
    [SerializeField] bool canGrow;

    public void AddNodeToList(IvyNode node)
    {
        var pos = node.getPosition();
        branchesTips.Add(pos); //adding the end of each branch  to calculate position to  it later
    }

    void Update() {

        if (Input.GetKeyUp(KeyCode.Space)) {
            // call this method when you are ready to group your meshes
            combineAndClear();
        }

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100)) {
                if(isFirst)
                {
                    Instantiate(impactEffect, hit.point, Quaternion.identity);
                    createIvy(hit);
                    isFirst = false;
                }
                else
                {

                    foreach(Vector3 tip in branchesTips.ToList())
                    {
                        if (Vector3.Distance(hit.point, tip) < MaxDistanceBetweenSprouts) //if  at least 1  of tips  is close  to  the hit point
                        {
                            canGrow = true;
                        }
                            
                    }
                    if(canGrow)
                    {
                        Instantiate(impactEffect, hit.point, Quaternion.identity);
                        createIvy(hit);
                        canGrow = false;
                    }
                }
            }
        }
    }

    Vector3 findTangentFromArbitraryNormal(Vector3 normal) {
        Vector3 t1 = Vector3.Cross(normal, Vector3.forward);
        Vector3 t2 = Vector3.Cross(normal, Vector3.up);
        if (t1.magnitude > t2.magnitude) {
            return t1;
        }
        return t2;
    }

    public void createIvy(RaycastHit hit) {
        var breaker = Instantiate(new GameObject(), hit.point + hit.normal, Quaternion.identity);
        Vector3 direction = hit.point - breaker.transform.position;
        var destroyer = breaker.AddComponent<SlowForce>();
        destroyer.Init(mass, raduis, force, maxForce, timeInterval, direction);

        Vector3 tangent = findTangentFromArbitraryNormal(hit.normal);
        GameObject ivy = new GameObject("Ivy " + ivyCount);

        ivy.transform.SetParent(transform);
        for (int i = 0; i < branches; i++)
        {
            Vector3 dir = Quaternion.AngleAxis(360 / branches * i + Random.Range(0, 360 / branches), hit.normal) * tangent;
            List<IvyNode> nodes = createBranch(maxPointsForBranch, hit.point, hit.normal, dir);
            GameObject branch = new GameObject("Branch " + i);
            Branch b = branch.AddComponent<Branch>();
            if (!wantBlossoms)
            {
                b.init(nodes, branchRadius, branchMaterial);
            }
            else
            {
                var leafMaterial = leafMaterials[Random.Range(0, leafMaterials.Length - 1)];
                var flowerMaterial = flowerMaterials[Random.Range(0, flowerMaterials.Length - 1)];

                b.init(nodes, branchRadius, branchMaterial, leafMaterial, leafPrefab, flowerMaterial, flowerPrefab, i == 0);
            }
            branch.transform.SetParent(ivy.transform);
        }

        ivyCount++;
       
    }

    Vector3 calculateTangent(Vector3 p0, Vector3 p1, Vector3 normal) {
        var heading = p1 - p0;
        var distance = heading.magnitude;
        var direction = heading / distance;
        return Vector3.Cross(normal, direction).normalized;
    }

    Vector3 applyCorrection(Vector3 p, Vector3 normal) {
        return p + normal * 0.01f;
    }

    bool isOccluded(Vector3 from, Vector3 to) {
        Ray ray = new Ray(from, (to - from) / (to - from).magnitude);
        return Physics.Raycast(ray, (to - from).magnitude);
    }

    bool isOccluded(Vector3 from, Vector3 to, Vector3 normal) {
        return isOccluded(applyCorrection(from, normal), applyCorrection(to, normal));
    }

    Vector3 calculateMiddlePoint(Vector3 p0, Vector3 p1, Vector3 normal) {
        Vector3 middle = (p0 + p1) / 2;
        var h = p0 - p1;
        var distance = h.magnitude;
        var dir = h / distance;
        return middle + normal * distance;
    }

    List<IvyNode> createBranch(int count, Vector3 pos, Vector3 normal, Vector3 dir) {

        if (count == maxPointsForBranch) {
            IvyNode rootNode = new IvyNode(pos, normal);
            return new List<IvyNode> { rootNode }.join(createBranch(count - 1, pos, normal, dir));
        } else if (count < maxPointsForBranch && count > 0) {

            if (count % 2 == 0) {
                dir = Quaternion.AngleAxis(Random.Range(-20.0f, 20.0f), normal) * dir;
            }

            RaycastHit hit;
            Ray ray = new Ray(pos, normal);
            Vector3 p1 = pos + normal * segmentLength;

            if (Physics.Raycast(ray, out hit, segmentLength)) {
                p1 = hit.point;
            }
            ray = new Ray(p1, dir);

            if (Physics.Raycast(ray, out hit, segmentLength)) {
                Vector3 p2 = hit.point;
                IvyNode p2Node = new IvyNode(p2, -dir);
                return new List<IvyNode> { p2Node }.join(createBranch(count - 1, p2, -dir, normal));
            } else {
                Vector3 p2 = p1 + dir * segmentLength;
                ray = new Ray(applyCorrection(p2, normal), -normal);
                if (Physics.Raycast(ray, out hit, segmentLength)) {
                    Vector3 p3 = hit.point;
                    IvyNode p3Node = new IvyNode(p3, normal);

                    if (isOccluded(p3, pos, normal)) {
                        Vector3 middle = calculateMiddlePoint(p3, pos, (normal + dir) / 2);

                        Vector3 m0 = (pos + middle) / 2;
                        Vector3 m1 = (p3 + middle) / 2;

                        IvyNode m0Node = new IvyNode(m0, normal);
                        IvyNode m1Node = new IvyNode(m1, normal);

                        return new List<IvyNode> { m0Node, m1Node, p3Node }.join(createBranch(count - 3, p3, normal, dir));
                    }

                    return new List<IvyNode> { p3Node }.join(createBranch(count - 1, p3, normal, dir));
                } else {
                    Vector3 p3 = p2 - normal * segmentLength;
                    ray = new Ray(applyCorrection(p3, normal), -normal);

                    if (Physics.Raycast(ray, out hit, segmentLength)) {
                        Vector3 p4 = hit.point;
                        IvyNode p4Node = new IvyNode(p4, normal);

                        if (isOccluded(p4, pos, normal)) {
                            Vector3 middle = calculateMiddlePoint(p4, pos, (normal + dir) / 2);
                            Vector3 m0 = (pos + middle) / 2;
                            Vector3 m1 = (p4 + middle) / 2;

                            IvyNode m0Node = new IvyNode(m0, normal);
                            IvyNode m1Node = new IvyNode(m1, normal);

                            return new List<IvyNode> { m0Node, m1Node, p4Node }.join(createBranch(count - 3, p4, normal, dir));
                        }

                        return new List<IvyNode> { p4Node }.join(createBranch(count - 1, p4, normal, dir));
                    } else {
                        Vector3 p4 = p3 - normal * segmentLength;
                        IvyNode p4Node = new IvyNode(p4, dir);

                        if (isOccluded(p4, pos, normal)) {
                            Vector3 middle = calculateMiddlePoint(p4, pos, (normal + dir) / 2);

                            Vector3 m0 = (pos + middle) / 2;
                            Vector3 m1 = (p4 + middle) / 2;

                            IvyNode m0Node = new IvyNode(m0, dir);
                            IvyNode m1Node = new IvyNode(m1, dir);

                            return new List<IvyNode> { m0Node, m1Node, p4Node }.join(createBranch(count - 3, p4, dir, -normal));
                        }
                        return new List<IvyNode> { p4Node }.join(createBranch(count - 1, p4, dir, -normal));
                    }
                }
            }

        }
        return null;
    }

    void combineAndClear() {
        MeshManager.instance.combineAll();
        foreach (Transform t in transform) {
            Destroy(t.gameObject);
        }
    }

}