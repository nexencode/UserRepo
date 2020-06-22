using System;

namespace UserRepo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=====");

            UserRepository.AddAndSerializeUsers();

            UserRepository.PrintUsers(UserRepository.UsersList);


            Console.WriteLine("=====");

            UserRepository.PrintUsers(UserRepository.UsersList);

            Console.WriteLine("=====");

            UserRepository.CreateUser("Nemanja", Status.NoActive);

            Console.WriteLine("=====");

            UserRepository.PrintUsers(UserRepository.GetUsersByName("Nemanja"));

            Console.WriteLine("=====");

            //Get user with wrong id!

            try
            {
                var a = UserRepository.GetUser(16);
                a.PrintUser();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Sorry, Something went wrong! {ex.Message}");
            }

            Console.WriteLine("=====");

            UserRepository.DeleteUser(4);

            UserRepository.PrintUsers(UserRepository.UsersList);
        }
    }
}
