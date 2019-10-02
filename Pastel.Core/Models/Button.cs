namespace Pastel.Core.Models
{
    public class Button
    {
        public string Name { get; }
        public bool Pressed { get; set; }

        public Button(string name)
        {
            Name = name;
        }
    }
}