using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Boo.Lang;
using TriLib;
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
        var mapFilePath = "map.fbx";
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
        Material material = Resources.Load<Material>("Route");

        int MapSize = 0;

        //Map calcuter
        foreach (Transform child in Map.transform.GetChild(0))
        {
            if (!child.name.StartsWith("map")) continue;
            MapSize++;
            child.transform.GetComponentInChildren<MeshRenderer>().material = material;
        }

        Route[][] RoutesByFloor = new Route[MapSize][];
        string[] args;
        Route[] route;
        //Route calcuter
        foreach (Transform child in Map.transform.GetChild(0))
        {
            if (child.name.StartsWith("rdr"))
            {
                Destroy(child.gameObject);
                continue;
            }
            if (!child.name.StartsWith("rot")) continue;
            args = child.name.Split('_');
            route = RoutesByFloor[int.Parse(args[2]) - 1] = RoutesByFloor[int.Parse(args[2]) - 1] ?? new Route[1000];

            if (args[1] == args[4])
                if (route.ElementAt(int.Parse(args[1])) == null)
                    route[int.Parse(args[1])] = new Route(child.localPosition);
                else
                    route[int.Parse(args[1])].position = child.localPosition;
            else
            {
               
                if (route.ElementAt(int.Parse(args[1])) == null)
                    route[int.Parse(args[1])] = new Route(child.localPosition);
                route[int.Parse(args[1])].prev.Add(int.Parse(args[4]));

                if (route.ElementAt(int.Parse(args[4])) == null)
                    route[int.Parse(args[4])] = new Route(Vector3.zero);
                route[int.Parse(args[4])].next.Add(int.Parse(args[1]));

            }

            Destroy(child.gameObject);
        }

        FitToBounds(Camera.main, Map.transform, 1.2f);
    }

    public static void FitToBounds(Camera camera, Transform transform, float distance)
    {
        var bounds = transform.EncapsulateBounds();
        var boundRadius = bounds.extents.magnitude;
        var finalDistnace = (boundRadius / (2.0f * Mathf.Tan(0.5f * camera.fieldOfView * Mathf.Deg2Rad))) * distance;
        camera.farClipPlane = finalDistnace * 2f;
        camera.transform.position = new Vector3(bounds.center.x, bounds.center.y + 100, bounds.center.z + finalDistnace);
        camera.transform.LookAt(transform.position + bounds.extents);
    }

}
