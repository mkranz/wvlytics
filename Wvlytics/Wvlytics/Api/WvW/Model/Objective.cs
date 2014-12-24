namespace Wvlytics.Api.WvW.Model
{
    public class Objective
    {
        public int id { get; set; }
        public string name { get; set; }

        public int GetPotentialPointsValue()
        {
            if (name == "Castle")
                return 35;
            if (name == "Keep")
                return 25;
            if (name == "Tower")
                return 10;
            if (name.StartsWith("(("))
                return 0; // assume bloodlust objective

            return 5;
        }
    }
}