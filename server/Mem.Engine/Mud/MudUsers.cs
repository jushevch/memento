using System.Collections.Generic;
using Mem.Core;

namespace Mem.Engine.Mud
{
    internal static class MudUsers
    {
        private static readonly Dictionary<string, User> users = new ();

        public static Queue<string> ConnectedIds { get; } = new ();

        public static Queue<string> DisconnectedIds { get; } = new ();

        public static IEnumerable<User> Active => users.Values;

        public static User Get(string id) => users[id];

        public static void Add(string id) => users.Add(id, new User(id));

        public static void Remove(string id) => users.Remove(id);
    }
}
