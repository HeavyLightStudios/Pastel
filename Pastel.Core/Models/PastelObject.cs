namespace Pastel.Core.Models
{
    public abstract class PastelObject
    {
        public virtual void Update(float deltaTime)
        {
        }

        public virtual void Draw()
        {
        }

        public virtual void Dispose()
        {
        }
    }
}