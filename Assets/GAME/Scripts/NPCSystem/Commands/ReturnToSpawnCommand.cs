// Assets/NPCSystem/Commands/ReturnToSpawnCommand.cs
namespace NPCSystem
{
    public class ReturnToSpawnCommand : ICommand
    {
        private readonly NPCController _npc;

        public ReturnToSpawnCommand(NPCController npc) => _npc = npc;

        public void Execute() => _npc.ReturnToSpawn();
    }
}
