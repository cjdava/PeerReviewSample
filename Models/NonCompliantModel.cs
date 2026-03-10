namespace PeerReviewSample.Models
{
    public class NonCompliantModel
    {
        public List<string> Items = new List<string>(); // Initialized

        public string Name; // No null check in constructor

        public NonCompliantModel(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name must not be null or empty.", nameof(name));
            Name = name;
        }

        public void AddItem(string item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            Items.Add(item);
        }
    }
}