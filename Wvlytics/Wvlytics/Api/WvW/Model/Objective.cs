using System;
using System.Collections.Generic;

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

		public string GetNiceName() {
			string niceName;
			if (Names.TryGetValue (id, out niceName))
				return niceName;
			return name;
		}

		public static Dictionary<int,string> Names = new Dictionary<int,string> () {
			{ 1,"Overlook Keep" },
			{ 2,"Valley Keep" },
			{ 3,"Lowlands Keep" },
			{ 4,"Golanta Camp" },
			{ 5,"Pangloss Camp" },
			{ 6,"Speldan Camp" },
			{ 7,"Danelon Camp" },
			{ 8,"Umberglade Camp" },
			{ 9,"Stonemist Castle" },
			{ 10,"Rogue's Camp" },
			{ 11,"Aldon's Tower" },
			{ 12,"Wildcreek Tower" },
			{ 13,"Jerrifer's Tower" },
			{ 14,"Klovan Tower" },
			{ 15,"Langor Tower" },
			{ 16,"Quentin Tower" },
			{ 17,"Mendon's Tower" },
			{ 18,"Anzalia's Tower" },
			{ 19,"Ogrewatch Tower" },
			{ 20,"Veloka Tower" },
			{ 21,"Durio's Tower" },
			{ 22,"Bravost Tower" },
			{ 23,"Garrison" },
			{ 24,"SS Camp" },
			{ 25,"SW Tower" },
			{ 26,"SE Tower" },
			{ 27,"West Keep" },
			{ 28,"NE Tower" },
			{ 29,"NN Camp" },
			{ 30,"NW Tower" },
			{ 31,"East Keep" },
			{ 32,"East Keep" },
			{ 33,"West Keep" },
			{ 34,"SS Camp" },
			{ 35,"SW Tower" },
			{ 36,"SE Tower" },
			{ 37,"Garrison" },
			{ 38,"NW Tower" },
			{ 39,"NN Camp" },
			{ 40,"NE Tower" },
			{ 41,"East Keep" },
			{ 42,"SE Tower" },
			{ 43,"SS Camp" },
			{ 44,"West Keep" },
			{ 45,"SW Tower" },
			{ 46,"Garrison" },
			{ 47,"NW Tower" },
			{ 48,"NW Camp" },
			{ 49,"SW Camp" },
			{ 50,"SE Camp" },
			{ 51,"NE Camp" },
			{ 52,"NW Camp" },
			{ 53,"SW Camp" },
			{ 54,"NE Camp" },
			{ 55,"SE Camp" },
			{ 56,"NN Camp" },
			{ 57,"NE Tower" },
			{ 58,"NW Camp" },
			{ 59,"SW Camp" },
			{ 60,"NE Camp" },
			{ 61,"SE Camp" }
		};
    }
	 
}