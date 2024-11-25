using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class AdjustControlPoints : MonoBehaviour
{
    private RamSpline ramSpline;

    private void OnEnable()
    {
        ramSpline = GetComponent<RamSpline>();
    }
    public void AdjustPointsToTerrain()
    {

        List<Vector4> controlPoints = ramSpline.controlPoints;
        for (int i = 0; i < controlPoints.Count; i++)
        {
            Vector4 point = controlPoints[i];
            float terrainHeight = Terrain.activeTerrain.SampleHeight(new Vector3(point.x, 0, point.z));
            controlPoints[i] = new Vector4(point.x, terrainHeight, point.z, point.w);
        }
        EditorUtility.SetDirty(ramSpline);
    }
}
[CustomEditor(typeof(AdjustControlPoints))]
public class AdjustControlPointsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AdjustControlPoints script = (AdjustControlPoints)target;

        if (GUILayout.Button("Snap points to terrain"))
        {
            script.AdjustPointsToTerrain();
        }
    }
}
