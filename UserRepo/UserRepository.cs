using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UserRepo
{
    public static class UserRepository
    {
        const string path = @"C:\Users\nexen\source\repos\UserRepo\users.json";
        /// <summary>
        /// Lista koja sadrzi sve usere
        /// </summary>
        public static List<User> UsersList = new List<User>();


        public static void AddAndSerializeUsers()
        {
            var users = new List<User>();
            if (File.Exists(path))
            {
                Console.WriteLine("Vec postoji lista usera!");
                UsersList = DeserializeUsers();
            }
            else
            {
                Console.WriteLine("Upisivanje novih usera...");

                UsersList = new List<User>()
                {
                    new User(1, "Nemanja", Status.Active),
                    new User(2,"Janko", Status.Active),
                    new User(3, "Marko", Status.Active),
                    new User(4, "Stanko", Status.NoActive),
                    new User(5, "Janko", Status.NoActive),
                    new User(6, "Marko", Status.NoActive),
                    new User(7, "Stojan", Status.Active)
                };
                File.WriteAllText(path, JsonConvert.SerializeObject(UsersList));
            }
        }

        public static List<User> DeserializeUsers()
        {
            string json = File.ReadAllText(path);
            var users = JsonConvert.DeserializeObject<List<User>>(json);
            return users;
        }

        public static bool CreateUser(string name, Status status)
        {
            //save and return id from json file
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                UsersList = JsonConvert.DeserializeObject<List<User>>(json);
            }

            try
            {
                int id = UsersList.Count > 0 ? UsersList.LastOrDefault().ID + 1 : 1;

                User user = new User(id, name, status);

                UsersList.Add(user);

                var serializedJson = JsonConvert.SerializeObject(UsersList, Formatting.Indented);
                File.WriteAllText(path, serializedJson);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public static bool DeleteUser(int id)
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                UsersList = JsonConvert.DeserializeObject<List<User>>(json);
            }

            try
            {
                var user = GetUser(id);
                UsersList.Remove(user);

                File.WriteAllText(path, JsonConvert.SerializeObject(UsersList, Formatting.Indented));

                Console.WriteLine($"Uspesno ste izbrisali korisnika koji ima id {id}");
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static User GetUser(int userId)
        {

            if (userId <= UsersList.Count && userId > 0)
            {
                var user = UsersList.Where(x => x.ID == userId).FirstOrDefault();
                return user;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "We don't have user with that id");
            }
                
        }


        public static List<User> GetActiveUsers()
        {
            UsersList = DeserializeUsers();
            var listActiveUsers = UsersList.Where(user => user.Active == Status.Active).ToList();
            return listActiveUsers;
        }

        public static List<User> GetUsersByName(string name)
        {
            UsersList = DeserializeUsers();

            var listUsers = UsersList.Where(user => user.Name.ToLower() == name.ToLower()).ToList();
            return listUsers;
        }

        public static void PrintUsers(List<User> filterUser)
        {
            foreach (User user in filterUser)
            {
                user.PrintUser();
            }
        }
    }
}
