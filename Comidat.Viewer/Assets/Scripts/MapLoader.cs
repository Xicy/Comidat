using System.IO;
using System.Linq;
using TriLib;
using UnityEngine;


public class MapLoader : MonoBehaviour
{
    public GameObject Map;
    public Transform Routes;

    void Start()
    {
        ImportMap();
        Camera.main.FitToBounds(Map.transform, 1.2f);
    }

    public bool ImportMap()
    {
        const string mapFilePath = "map.fbx";
        if (!File.Exists(mapFilePath)) return false;

        var assetLoader = new AssetLoader();
        var assetLoaderOptions = AssetLoaderOptions.CreateInstance();
        assetLoaderOptions.RotationAngles = new Vector3(0f, 0f, 0f);
        assetLoaderOptions.DontLoadAnimations = true;
        assetLoaderOptions.DontLoadMaterials = true;
        assetLoaderOptions.DontLoadCameras = true;
        assetLoaderOptions.GenerateMeshColliders = true;

        Map = assetLoader.LoadFromFile(mapFilePath, assetLoaderOptions);
        Map.transform.position = new Vector3(0f, 0f, 0f);
        Map.name = "Map";
        this.SetMap();
        return true;
    }

    public void SetMap()
    {
        var material = Resources.Load<Material>("Route");
        Vector3[][] pathsByGroup = new Vector3[500][];
        int[] itterator = new int[500];
        int groupMaks = 0;
        foreach (Transform child in Map.transform.GetChild(0))
        {
            if (child.name.StartsWith("map"))
            {
                child.transform.GetComponentInChildren<MeshRenderer>().material = material;
                child.transform.GetComponentInChildren<MeshRenderer>().gameObject.layer = 9;
            }
            else if (child.name.StartsWith("rot"))
            {
                var args = child.name.Split('_');
                var group = int.Parse(args[3]);
                groupMaks = groupMaks < group ? group : groupMaks;
                if (pathsByGroup[group] == null)
                    pathsByGroup[group] = new Vector3[500];
                var path = pathsByGroup[group];
                path[itterator[group]++] = child.localPosition;

                Destroy(child.gameObject);
            }
            else
                Destroy(child.gameObject);
        }
        for (; groupMaks > 0; groupMaks--)
        {
            var obj = new GameObject("Group_" + groupMaks, typeof(LineRenderer), typeof(ClosestPointLineRenderer));
            obj.transform.parent = Routes;
            var line = obj.GetComponent<LineRenderer>();
            line.widthMultiplier = 0;
            line.positionCount = itterator[groupMaks];
            line.SetPositions(pathsByGroup[groupMaks]);
        }
        Routes.gameObject.GetComponent<ClosestPointGroup>().UpdateFunction();
    }
}
