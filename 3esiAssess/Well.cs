namespace _3esiAssess
{
    public class Well
    {

        public string Name { get; set; }
        public int TopHoleX { get; set; }
        public int TopHoleY { get; set; }
        public int BotHoleX { get; set; }
        public int BotHoleY { get; set; }
       

        public Well(string name, int topHoleX, int topHoleY, int botHoleX, int botHoleY)
        {
            Name = name;
            TopHoleX = topHoleX;
            TopHoleY = topHoleY;
            BotHoleX = botHoleX;
            BotHoleY = botHoleY;
        }



    }
}