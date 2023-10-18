using StackExchange.Redis;
using System;

class Program
{
    static void Main(string[] args)
    {
        //https://stackexchange.github.io/StackExchange.Redis/Transactions.html
        
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("connection_string_goes_here");
        // Get a reference to the database
        IDatabase db = redis.GetDatabase();

        // Set multiple keys in Redis
        HashEntry[] hashFields = new HashEntry[]
        {
            new HashEntry("key41", "value41"),
            new HashEntry("key42", "value42"),
            new HashEntry("key43", "value43")
        };
        try
        {
            var transc = db.CreateTransaction();
            //db.HashSet("myhash", hashFields);
            transc.SetAddAsync("{done}key101", "value101");
            transc.SetAddAsync("{done}key102", "value102");
            transc.SetAddAsync("{done}key103", "value103");
            bool success = transc.Execute();
            if (success)
            {
                Console.WriteLine("success");
            }
            else
            {
                Console.WriteLine("tans failed");
            }
        }catch(Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        // Get multiple keys from Redis
        RedisValue[] values = db.HashGet("myhash", new RedisValue[] { "key51", "key52", "key53" });
        //db.get
        // Print the values
        foreach (RedisValue value in values)
        {
            Console.WriteLine(value);
        }
    }
}
