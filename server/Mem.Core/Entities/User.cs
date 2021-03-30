using System;
using System.Collections.Generic;
using System.Text;

namespace Mem.Core
{
    public enum UserStatus
    {
        Connected,
        InGame,
        GetLogin,
        GetPassword,
        Create,
        GetNameNom,
        GetNameGen,
        GetNameDat,
        GetNameAcc,
        GetNameIns,
        GetNamePre,
        GetGender,
        CreatePassword,
        ConfirmPassword,
        GetEmail,
        ConfirmCheck,
        Launch,
        Quit,
        Disconnected,
    }

    public class User
    {
        public User(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException($"Invalid user ID value.");
            }

            this.Id = id;
        }

        public string Id { get; }

        public UserStatus Status { get; set; }

        public string Prompt { get; set; } = "<br>&gt; ";

        public Queue<string> InputQueue { get; } = new Queue<string>();

        public StringBuilder Output { get; } = new StringBuilder();

        public Credentials Credentials { get; set; }

        public Mobile Character { get; set; }
    }
}
