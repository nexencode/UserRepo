using System;
using System.Collections.Generic;
using System.Text;

namespace UserRepo
{
    public enum Status
    {
        Active,
        NoActive
    }
    public class User
    {
        #region Fields and Properties
        public int ID { get; set; }
        public string Name { get; set; }
        public Status Active { get; set; }
        #endregion

        #region Constructor
        public User()
        {

        }
        public User(int id, string name, Status active)
        {
            this.ID = id;
            this.Name = name;
            this.Active = active;
        }
        #endregion


        public void PrintUser()
        {
            Console.WriteLine($"ID: {ID}, Name: {Name}, Status: {Active}");
        }
    }
}
