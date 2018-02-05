using System;
using System.IO;
using Boo.Lang;
using TriLib;
using UnityEditor;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public GameObject Map;

    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        ImportMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool ImportMap()
    {
        var mapFilePath = Path.Combine(Environment.CurrentDirectory, "map.fbx");
        if (File.Exists(mapFilePath))
        {
            AssetLoader assetLoader = new AssetLoader();
            AssetLoaderOptions assetLoaderOptions = AssetLoaderOptions.CreateInstance();
            assetLoaderOptions.RotationAngles = new Vector3(0f, 0f, 0f);
            assetLoaderOptions.DontLoadAnimations = true;
            assetLoaderOptions.DontLoadMaterials = true;
            assetLoaderOptions.DontLoadCameras = true;
            assetLoaderOptions.GenerateMeshColliders = false;

            Map = assetLoader.LoadFromFile(mapFilePath, assetLoaderOptions);
            Map.transform.position = new Vector3(0f, 0f, 0f);
            Map.name = "Map";
            this.SetMap();
            return true;
        }

        return false;
    }

    public void SetMap()
    {
        Material material = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/Route.mat");
        List<Vector3> rotList = new List<Vector3>();
        foreach (Transform child in Map.transform.GetChild(0))
        {
            if (child.name.StartsWith("map"))
            {
                child.transform.GetComponentInChildren<MeshRenderer>().material = material;
            }
            else if (child.name.StartsWith("cam"))
            {
                Destroy(child.gameObject);
            }
            else if (child.name.StartsWith("rot"))
            {
                rotList.Add(child.transform.position);
                //Destroy(child.gameObject);
            }
            else if (child.name.StartsWith("rdr"))
            {
                Destroy(child.gameObject);
            }
        }
        Camera.main.FitToBounds(Map.transform,1);


        Color red = Color.red;
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.SetColors(red, red);
        lineRenderer.SetWidth(0.2F, 0.2F);

        //Change how mant points based on the mount of positions is the List
        lineRenderer.SetVertexCount(rotList.Count);

        for (int i = 0; i < rotList.Count; i++)
        {
            //Change the postion of the lines
            lineRenderer.SetPosition(i, rotList[i]);
        }
    }
}
