namespace MusicSpot
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Account floor = new Account("Floor Van Moorsel", "floortje@gmail.com", "coolPASS", 1, "gaypeople.png");
            Account jamey = new Account("Jamey Verlinden", "jameydeblinde@gmail.com", "OmegaIntern28", 0, "NPC.jpg");

            Person kurtCobain = new Person("Kurt Cobain");
            Person kristNovoselic = new Person("Krist Novoselic");
            Person daveGrohl = new Person("Dave Grohl");
            Person davidBowiePerson = new Person("David Bowie");

            List<Person> nirvanaMembers = new List<Person>{kurtCobain, kristNovoselic, daveGrohl};
            List<Person> davidBowieMembers = new List<Person> { davidBowiePerson };
            Artist nirvana = new Artist("Nirvana", nirvanaMembers);
            Artist davidBowie = new Artist("David Bowie", davidBowieMembers);

            Console.WriteLine(floor);
            Console.WriteLine(jamey);
            Console.WriteLine(nirvana);
            Console.WriteLine(davidBowie);
        }
    }
}
