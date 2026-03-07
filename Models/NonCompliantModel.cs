namespace PeerReviewSample.Models
{
    public class NonCompliantModel
    {
        public List<string> Items = new List<string>(); // Initialized

        public string Name; // No null check in constructor

        public NonCompliantModel(string name)
        {
            Name = name; // No validation for null or empty
        }

        public void AddItem(string item)
        {
            Items.Add(item); // Possible NullReferenceException
        }
    }
}