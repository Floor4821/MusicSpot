namespace MusicSpot
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Account floor = new Account("Floor Van Moorsel", "floortje@gmail.com", "coolPASS", 1, "gaypeople.png");
            Account jamey = new Account("Jamey Verlinden", "jameydeblinde@gmail.com", "OmegaIntern28", 0, "NPC.jpg");

            Console.WriteLine(floor);
            Console.WriteLine(jamey);
        }
    }
}
