using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace DefaultNamespace
{
    public class MoveSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var jobHandle = Entities.WithName("MoveSystem").ForEach((ref Translation position, ref Rotation rotation) =>
            {
                position.Value.y += (0.001f * math.up()).y;
                
                if (position.Value.y > 20) position.Value.y = 0;
            }).Schedule(inputDeps);
            return jobHandle;
        }
    }
}