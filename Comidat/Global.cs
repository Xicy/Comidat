using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Comidat.Data;
using Comidat.Data.Model;
using Comidat.Diagnostics;
using Comidat.Model;

namespace Comidat
{
    public static class Global
    {
        public static readonly DatabaseContext Database = new DatabaseContext();

        //public static readonly ConcurrentDictionary<MacAddress, object> Esps = new ConcurrentDictionary<MacAddress, object>();
        public static readonly ConcurrentDictionary<MacAddress, TBLReader> Readers =
            new ConcurrentDictionary<MacAddress, TBLReader>();
        //private static readonly ConcurrentDictionary<MacAddress, ConcurrentQueue<ITag>> TBLTags = new ConcurrentDictionary<MacAddress, ConcurrentQueue<ITag>>();

        /*public static void SeedForTestFromFile()
        {
            FileReader fr = new FileReader(Path.Combine(AppContext.BaseDirectory, "seeds.txt"));
            foreach (var line in fr)
            {
                var data = line.Value.Split('\t');
                var esp = new ESP() { Active = true, Batary = 100, Description = "", SSID = data[2], Ip = "", MacId = new MacAddress(data[1]) };
                //Esps.TryAdd(esp.MacId, esp);
                if (data[0].ToLowerInvariant() == "r")
                {
                    var reader = new Reader() { ApIp = "", ESP = esp, X = double.Parse(data[3]), Y = ulong.Parse(data[4]), Z = ulong.Parse(data[5]) };
                    TBLReaders.TryAdd(reader.ESP.MacId, reader);
                }
                else if (data[0].ToLowerInvariant() == "t")
                {
                    //var tag = new Data.Model.Tag{BirthDate = DateTime.Now,ESP = esp,FirstName = data[3],LastName = data[4],TcNumber = long.Parse(data[1])};
                    //TBLTags.TryAdd(esp.MacId, tag);
                }
            }
        }*/
        /*
        public static void SeedReaders()
        {
            var esps = new List<ESP>
            {
                new ESP
                {
                    Active = true,
                    Batary = 100,
                    Description = "Test",
                    SSID = "Reader 1",
                    MacId = new MacAddress("AB:01:11:12:13:10")
                },
                new ESP
                {
                    Active = true,
                    Batary = 100,
                    Description = "Test",
                    SSID = "Reader 2",
                    MacId = new MacAddress("AB:01:11:12:13:11")
                },
                new ESP()
                {
                    Active = true,
                    Batary = 100,
                    Description = "Test",
                    SSID = "Reader 3",
                    MacId = new MacAddress("AB:01:11:12:13:12")
                },
                new ESP
                {
                    Active = true,
                    Batary = 100,
                    Description = "Test",
                    SSID = "Reader 4",
                    MacId = new MacAddress("AB:01:11:12:13:13")
                },
                new ESP()
                {
                    Active = true,
                    Batary = 100,
                    Description = "Test",
                    SSID = "Reader 5",
                    MacId =  new MacAddress("AB:01:11:12:13:14")
                }
            };

            Database.ESPs.AddRange(esps);

            var readers = new List<Reader>
            {
                new Reader
                {
                    ApIp = "127.0.0.1",
                    ESP = esps[0],
                    Name = "Reader1",
                    X = 100,
                    Y = 100,
                    Z = 100,
                    TX = 100,
                    TY = 100
                },
                new Reader
                {
                    ApIp = "127.0.0.2",
                    ESP = esps[1],
                    Name = "Reader2",
                    X = 200,
                    Y = 200,
                    Z = 200,
                    TX = 200,
                    TY = 200
                },
                new Reader
                {
                    ApIp = "127.0.0.3",
                    ESP = esps[2],
                    Name = "Reader3",
                    X = 300,
                    Y = 300,
                    Z = 300,
                    TX = 300,
                    TY = 300
                },
                new Reader
                {
                    ApIp = "127.0.0.4",
                    ESP = esps[3],
                    Name = "Reader4",
                    X = 400,
                    Y = 400,
                    Z = 400,
                    TX = 400,
                    TY = 400
                },
                new Reader
                {
                    ApIp = "127.0.0.5",
                    ESP = esps[4],
                    Name = "Reader5",
                    X = 500,
                    Y = 500,
                    Z = 500,
                    TX = 500,
                    TY = 500
                }
            };

            Database.TBLReaders.AddRange(readers);
        }

        public static void SeedTags()
        {
            var esps = new List<ESP>
            {
                new ESP
                {
                    Active = true,
                    Batary = 100,
                    Description = "Test",
                    SSID = "Tag 1",
                    MacId = new MacAddress("AB:01:11:12:12:10")
                },
                new ESP
                {
                    Active = true,
                    Batary = 100,
                    Description = "Test",
                    SSID = "Tag 2",
                    MacId = new MacAddress("AB:01:11:12:12:11")
                },
                new ESP
                {
                    Active = true,
                    Batary = 100,
                    Description = "Test",
                    SSID = "Tag 3",
                    MacId = new MacAddress("AB:01:11:12:12:12")
                },
                new ESP
                {
                    Active = true,
                    Batary = 100,
                    Description = "Test",
                    SSID = "Tag 4",
                    MacId =  new MacAddress("AB:01:11:12:12:13")
                },
                new ESP
                {
                    Active = true,
                    Batary = 100,
                    Description = "Test",
                    SSID = "Tag 5",
                    MacId = new MacAddress("AB:01:11:12:12:14")
                }
            };

            Database.ESPs.AddRange(esps);

            var readers = new List<Data.Model.Tag>
            {
                new Data.Model.Tag
                {
                    ESP = esps[0],
                    BirthDate = DateTime.Parse("1996/12/5"),
                    FirstName = "Umut",
                    LastName = "Akkaya",
                    MobilePhone = "5366927997",
                    TcNumber = 23887947008,
                    Phone = "2322598686"
                },
                new Data.Model.Tag
                {
                    ESP = esps[1],
                    BirthDate = DateTime.Parse("1996/12/18"),
                    FirstName = "Kadir",
                    LastName = "Kapıcı",
                    MobilePhone = "5366927997",
                    TcNumber = 23887947007,
                    Phone = "2322598686"
                },
                new Data.Model.Tag
                {
                    ESP = esps[2],
                    BirthDate = DateTime.Parse("1983/01/07"),
                    FirstName = "Kazım",
                    LastName = "Fırat",
                    MobilePhone = "9988745632",
                    TcNumber = 23887947006,
                    Phone = "123456789456"
                },
                new Data.Model.Tag
                {
                    ESP = esps[3],
                    BirthDate = DateTime.Parse("1986/06/05"),
                    FirstName = "Fehmi",
                    LastName = "Trakya",
                    MobilePhone = "55555555555",
                    TcNumber = 23887947005,
                    Phone = "1111113559"
                },
                new Data.Model.Tag
                {
                    ESP = esps[4],
                    BirthDate = DateTime.Parse("1990/11/10"),
                    FirstName = "Ali",
                    LastName = "Kara",
                    MobilePhone = "1111111111",
                    TcNumber = 23887947004,
                    Phone = "0000000000"
                },
            };

            Database.TBLTags.AddRange(readers);
        }
        */
        public static void LoadReaders()
        {
            foreach (var esp in Database.TBLReaders)
                try
                {
                    Readers.TryAdd(new MacAddress(esp.rd_mac_address), esp);
                }
                catch (Exception)
                {
                }
        }

        public static TBLReader GetReader(MacAddress address)
        {
            Readers.TryGetValue(address, out var ret);
            return ret;
        }

        /*public static void PacketAdd(Packet pack)
        {
            foreach (var tag in pack)
            {
                if (TBLReaders.ContainsKey(tag.ReaderMacAddress))
                    TBLTags.AddOrUpdate(tag.MacAddress, new ConcurrentQueue<ITag>(pack.Where(p => Equals(p.MacAddress, tag.MacAddress))), (mac, exPack) =>
                    {
                        exPack.Enqueue(tag);
                        return exPack;
                    });
            }
        }*/

        /*public static IList<IList<ITag>> GetTags()
        {
            var ret = new List<IList<ITag>>();
            foreach (var macAddress in TBLTags.Keys.ToList())
            {
                if (!TBLTags.TryGetValue(macAddress, out var tagQueue) || tagQueue.Count % 2 != 0) continue;
                R1:
                if (!tagQueue.TryDequeue(out var tag1)) continue;
                if(!TBLReaders.ContainsKey(tag1.ReaderMacAddress)) goto R1;
                var tmp = new List<ITag> { tag1 };
                R2:
                if (tagQueue.TryDequeue(out var tag2) && !tag1.Equals(tag2))
                {
                    if (!TBLReaders.ContainsKey(tag2.ReaderMacAddress)) goto R2;
                    tmp.Add(tag2);
                    ret.Add(tmp);
                }
                else
                    tagQueue.Enqueue(tag1);
            }
            return ret;
        }*/

        public static async Task SaveDataBaseAync()
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(30));
                try
                {
                    Database.SaveChangesAsync().Wait(0);
                }
                catch (Exception exception)
                {
                    Logger.Exception(exception);
                }
            }
        }
    }
}