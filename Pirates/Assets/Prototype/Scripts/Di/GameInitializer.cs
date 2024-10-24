using Scellecs.Morpeh;

namespace Prototype.Scripts.Di
{
    public class GameInitializer : IInitializer
    {
        public World World { get; set; }

        public void Dispose()
        {
            // TODO release managed resources here
        }

        public void OnAwake()
        {
            
        }
    }
}