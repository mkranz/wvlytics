namespace Wvlytics.Models
{
    public class World
    {
        public int id { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return string.Format("id: {0}, name: {1}", id, name);
        }
    }
}