using System;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.UIElements;
using Random = Unity.Mathematics.Random;

namespace DefaultNamespace
{
    public class ECSManager : MonoBehaviour
    {
        public GameObject voxelPrefab;
        private EntityManager manager;
        private const int spawnedObjectsCount = 36000;
        private BlobAssetStore blob;

        private void Start()
        {
            Random rd = new Random(120);
            blob = new BlobAssetStore();
            manager = World.DefaultGameObjectInjectionWorld.EntityManager;
            GameObjectConversionSettings settings;
            settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, blob);
            var prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(voxelPrefab, settings);
            for (int i = 0; i < spawnedObjectsCount; i++)
            {
                var instance = manager.Instantiate(prefab);
                var position = transform.TransformPoint(new float3(rd.NextFloat(-10, 10), rd.NextFloat(0, 20f),
                    rd.NextFloat(-10, 10)));
                manager.SetComponentData(instance, new Translation() {Value = position});
                manager.SetComponentData(instance, new Rotation {Value = new quaternion(0, 1, 0, 0)});
            }
        }
    }
}