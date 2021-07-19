using System;

namespace SpotterClasses
{
    public class Membre
    {
        private int id;
        private string email, username, motdepasse;

        public int Id { get => id; set => id = value; }
        public string Email { get => email; set => email = value; }
        public string Username { get => username; set => username = value; }
        public string Motdepasse { get => motdepasse; set => motdepasse = value; }
    }
}




