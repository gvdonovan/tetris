using RamQuest.Core.Model;

namespace RamQuest.Tetris.Model
{
    public class Command : Entity<int>
    {
        public int ModuleId { get; set; }
        public virtual Module Module { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string ContractType { get; set; }
        public string Assembly { get; set; }
        public string Namespace { get; set; }
        public string Type { get; set; }
    }
}
