using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class VisionConeVisualiser : MonoBehaviour
{
    [Header("Link to VisionCone")]
    public VisionCone visionCone;

    [Header("Visual Settings")]
    public int segments = 24; // Number of segments along the arc
    public Color coneColor = new Color(0f, 1f, 1f, 0.25f);

    private Mesh mesh;
    [SerializeField] private Material _material;

    [SerializeField] private bool _is3D = false;

    void Awake()
    {
        if (visionCone == null)
            visionCone = GetComponentInParent<VisionCone>();

        if (_is3D)
            GenerateMesh();
        else
            GenerateMesh2D();
    }

#if UNITY_EDITOR
    [ContextMenu("Rebuild 2D Vision Cone")]
    void RebuildButton()
    {
        if(_is3D)
            GenerateMesh();
        else
            GenerateMesh2D();
    }
#endif
    void GenerateMesh()
    {
        if (visionCone == null) return;

        if (mesh != null)
            DestroyImmediate(mesh);

        mesh = new Mesh();
        mesh.name = "VisionCone3DMesh";

        int verticalSegments = segments / 2; // You can expose this if you want
        int horizontalSegments = segments;

        Vector3 apex = Vector3.zero;
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Color> colors = new List<Color>();

        vertices.Add(apex);
        colors.Add(coneColor);

        // Generate vertices for each horizontal & vertical subdivision
        for (int i = 0; i <= horizontalSegments; i++)
        {
            float hT = (float)i / horizontalSegments;
            float hAngle = Mathf.Lerp(-visionCone.horizontalFOV * 0.5f, visionCone.horizontalFOV * 0.5f, hT);

            for (int j = 0; j <= verticalSegments; j++)
            {
                float vT = (float)j / verticalSegments;
                float vAngle = Mathf.Lerp(-visionCone.verticalFOV * 0.5f, visionCone.verticalFOV * 0.5f, vT);

                Vector3 dir = Quaternion.Euler(vAngle, hAngle, 0) * Vector3.forward;
                vertices.Add(dir * visionCone.viewDistance);
                colors.Add(coneColor);
            }
        }

        // Build triangles
        for (int i = 0; i < horizontalSegments; i++)
        {
            for (int j = 0; j < verticalSegments; j++)
            {
                int current = 1 + j + i * (verticalSegments + 1);
                int next = current + verticalSegments + 1;

                // Triangle 1
                triangles.Add(0); // apex
                triangles.Add(current);
                triangles.Add(current + 1);

                // Triangle 2
                triangles.Add(0); // apex
                triangles.Add(current + 1);
                triangles.Add(next + 1);
            }
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.colors = colors.ToArray();
        mesh.RecalculateNormals();

        MeshFilter mf = GetComponent<MeshFilter>();
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mf.sharedMesh = mesh;

        if (mr.sharedMaterial == null && _material != null)
            mr.sharedMaterial = _material;
    }
    void GenerateMesh2D()
    {
        if (visionCone == null) return;

        if (mesh != null)
            DestroyImmediate(mesh);

        mesh = new Mesh();
        mesh.name = "VisionCone2DMesh";

        Vector3[] vertices = new Vector3[segments + 2]; // apex + points along arc
        int[] triangles = new int[segments * 3];
        Color[] colors = new Color[vertices.Length];

        Vector3 apex = Vector3.zero;
        vertices[0] = apex;
        colors[0] = coneColor;

        // Generate points along the horizontal FOV arc
        for (int i = 0; i <= segments; i++)
        {
            float t = (float)i / segments;
            float angle = Mathf.Lerp(-visionCone.horizontalFOV * 0.5f, visionCone.horizontalFOV * 0.5f, t);
            Vector3 dir = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            vertices[i + 1] = dir * visionCone.viewDistance;
            colors[i + 1] = coneColor;
        }

        // Build triangles
        for (int i = 0; i < segments; i++)
        {
            triangles[i * 3] = 0;           // apex
            triangles[i * 3 + 1] = i + 1;   // current vertex
            triangles[i * 3 + 2] = i + 2;   // next vertex
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.RecalculateNormals();

        MeshFilter mf = GetComponent<MeshFilter>();
        MeshRenderer mr = GetComponent<MeshRenderer>();

        mf.sharedMesh = mesh;

        if (mr.sharedMaterial == null)
        {
            Material mat = _material;

            mr.sharedMaterial = mat;
        }
    }
}