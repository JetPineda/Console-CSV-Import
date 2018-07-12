namespace _3esiAssess
{
    public class Group
    {

        public string Name { get; set; }
        public int LocationX { get; set; }
        public int LocationY { get; set; }
        public int Radius { get; set; }

        public Group(string name, int locationX, int locationY, int radius)
        {
            Name = name;
            LocationX = locationX;
            LocationY = locationY;
            Radius = radius;
        }




        

    }
}