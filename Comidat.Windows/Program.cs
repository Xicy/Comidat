using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Comidat.Data.Model;
using Comidat.Diagnostics;
using Comidat.Model;
using Comidat.Net;
using Comidat.Net.EventArgs;
using Comidat.Runtime;
using Comidat.Runtime.Command;

namespace Comidat
{
    internal static class Program
    {
        private const int ProgressStep = 7;

        private static void Main(string[] args)
        {
            ExceptionHandler.InstallExceptionHandler();

            var sw = new Stopwatch();
            sw.Start();
            Console.CursorVisible = false;
            Cli.WriteHeader(Localization.Get("Title"), Localization.Get("Header"), ConsoleColor.Red);
            Cli.LoadingTitle();
            Logger.Progress(0, ProgressStep);

            //Logger Settings Up
            Logger.Archive = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Logs");
            Logger.LogFile = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Logs",
                "Comidat.log");
            Logger.Progress(1, ProgressStep);

            //Console commands activating
            var console = new ConsoleCommands();
            //Logger.Hide ^= LogLevel.Debug;
            Logger.Progress(2, ProgressStep);

            //Settings up server 
            IServer server = new TCP();
            server.Connected += ServerOnConnected;
            server.Disconnected += ServerOnDisconnected;
            server.MessageReceived += ServerOnMessageReceived;
            server.StartAsync(new IPEndPoint(IPAddress.Any, 5757));
            Logger.Progress(3, ProgressStep);

            //Settings up Database
            //Global.Database.Database.Migrate();
            Logger.Progress(4, ProgressStep);

            //Seed TBLReaders for testing
            //Global.SeedReaders();
            //Global.SeedTags();
            //Global.Database.SaveChanges();
            Logger.Progress(5, ProgressStep);

            Global.LoadReaders();
            Logger.Progress(6, ProgressStep);

            //Global.SeedForTestFromFile();

            Logger.Progress(ProgressStep, ProgressStep);
            Logger.Info(Localization.Get("Comidat.Program.MainB.LoadingTime"), sw.ElapsedMilliseconds);
            sw.Stop();

            Cli.RunningTitle();

            //Test(server).Wait(0);

            Global.SaveDataBaseAync().Wait(0);

            console.Wait();
        }

        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ee = (Exception)e.ExceptionObject;
            Logger.Exception(ee, ee.Message);
        }

        private static void ServerOnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            //Console.Clear();
            //Logger.Debug(Localization.Get("Comidat.Program.ServerOnMessageReceived.Debug.2"), e.Client, Encoding.UTF8.GetString(e.Message).Trim());
            var packet = new Packet(Encoding.UTF8.GetString(e.Message));
            if (packet.Count == 0) return;
            var r0 = packet.Where(tag => tag.RSSI < 100).OrderBy(tag => tag.RSSI).Join(
                Global.Readers.Values,
                address => address.ReaderMacAddress,
                reader => new MacAddress(reader.rd_mac_address),
                (address, reader) => new { a = address, r = reader }
            );
            try
            {

                var readers = r0.Take(2).ToArray();
                if (readers.Length == 1)
                {
                    var tf = readers[0];
                    Logger.Debug("Tag: {0}\t \t X: {1}\tY: {2}\tZ: {3}", tf.a.MacAddress, tf.r.d_rd_pos_x,
                        tf.r.d_rd_pos_y, tf.r.rd_pos_z);
                    Global.Database.TBLPositions.Add(new TBLPosition
                    {
                        d_XPosition = (int)tf.r.d_rd_pos_x,
                        d_yPosition = (int)tf.r.d_rd_pos_y,
                        TagId = (int)tf.a.ReaderMacAddress.GetLong(),
                        MapId = tf.r.MapId
                    });
                }
                else if (readers.Length == 2)
                {
                    var tf = readers[0];
                    var tl = readers[1];
                    var r1 = Global.Distances[tf.a.RSSI];
                    var r2 = Global.Distances[tf.a.RSSI];
                    var totalStep = r1 + r2;
                    var x = tf.r.d_rd_pos_x + (tl.r.d_rd_pos_x - tf.r.d_rd_pos_x) * (r1 / totalStep);
                    var y = tf.r.d_rd_pos_y + (tl.r.d_rd_pos_y - tf.r.d_rd_pos_y) * (r1 / totalStep);
                    var z = tf.r.rd_pos_z + (tl.r.rd_pos_z - tf.r.rd_pos_z) * (r1 / totalStep);
                    Logger.Debug("Tag: {0}\t \t X: {1}\tY: {2}\tZ: {3}", tf.a.MacAddress, x, y, z);

                    var tagid = Global.Tags.First(t => t.TagMacAddress == tf.a.MacAddress.GetLong().ToString()).Id;
                    Global.Database.TBLPositions.Add(new TBLPosition
                    {
                        d_XPosition = (int)x,
                        d_yPosition = (int)y,
                        TagId = tagid,
                        MapId = tf.r.MapId
                    });
                }
            }
            catch (InvalidOperationException ex)
            {
                Logger.Exception(ex);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Logger.Exception(ex);
            }
#if DEBUG
            string debug =
 String.Format(Localization.Get("Comidat.Program.ServerOnMessageReceived.Debug.0"), e.Client, packet[0].MacAddress, 2412);
            foreach (var tag in r0)
                debug +=
 String.Format(Localization.Get("Comidat.Program.ServerOnMessageReceived.Debug.1"), tag.a.ReaderMacAddress, tag.a.RSSI, tag.r.ReaderName, Helper.CalculateDistance(FSPL.MeterAndMegaHertz, tag.a.RSSI, 2412));

            //Logger.Debug(Localization.Get("Comidat.Program.ServerOnMessageReceived.Debug.2"), e.Client, Encoding.UTF8.GetString(e.Message).Trim());
            Logger.Debug(debug);
#endif
        }

        private static void ServerOnDisconnected(object sender, DisconnectedEventArgs e)
        {
            Logger.Info(Localization.Get("Comidat.Program.ServerOnDisconnected.Info"), e.Client);
        }

        private static void ServerOnConnected(object sender, ConnectedEventArgs e)
        {
            Logger.Info(Localization.Get("Comidat.Program.ServerOnConnected.Info"), e.Client);
        }


        private static async Task Test(IServer server)
        {
            var r = new Random(DateTime.Now.Second);
            var client = new TcpClient();
            client.Connect("localhost", 5757);
            string[] data =
            {
                ";-76;12;DE:4F:22:10:D5:61\r\n;-73;12;DE:4F:22:11:15:65\r\n;-76;12;DE:4F:22:11:30:DD\r\n;-78;12;00:E0:4C:5F:81:78\r\n;-72;12;00:E0:4C:5F:B2:B4\r\n;-74;12;10:22:33:44:09:43\r\n;-73;12;10:22:33:44:09:49\r\n;-56;12;00:E0:4C:5F:B0:D8\r\n;-59;12;00:30:0A:C2:98:0C\r\n;-93;12;C0:25:E9:FB:3F:B9\r\n;-58;12;00:E0:4C:5F:91:EC\r\n;-77;12;C4:E9:84:A5:03:04\r\n;-76;12;DE:4F:22:11:15:65\r\n;-75;12;DE:4F:22:10:D5:61\r\n;-78;12;DE:4F:22:11:30:DD\r\n;-72;12;10:22:33:44:09:49\r\n;-72;12;00:E0:4C:5F:B2:B4\r\n;-63;12;00:E0:4C:5F:B0:D8\r\n;-78;12;00:E0:4C:5F:81:78\r\n;-59;12;00:30:0A:C2:98:0C\r\n;-90;12;C0:25:E9:FB:3F:B9\r\n;-48;12;00:E0:4C:5F:91:EC\r\n;-75;12;10:22:33:44:09:43\r\n;-93;12;A4:2B:B0:DB:3F:4B\r\n;-82;12;C4:E9:84:A5:03:04\r\n;-77;12;DE:4F:22:11:30:DD\r\n;-76;12;DE:4F:22:11:15:65\r\n;-76;12;DE:4F:22:10:D5:61\r\n;-54;12;00:E0:4C:5F:B0:D8\r\n;-73;12;10:22:33:44:09:49\r\n;-71;12;00:E0:4C:5F:B2:B4\r\n;-73;12;10:22:33:44:09:43\r\n;-58;12;00:E0:4C:5F:91:EC\r\n;-58;12;00:30:0A:C2:98:0C\r\n;-92;12;C0:25:E9:FB:3F:B9\r\n;-75;12;00:E0:4C:5F:81:78\r\n;-82;12;C4:E9:84:A5:03:04",
                ";-88;9;DE:4F:22:11:15:65\r\n;-91;9;18:A6:F7:25:5E:F0\r\n;-50;9;00:E0:4C:5F:B2:B4\r\n;-83;9;10:22:33:44:09:43\r\n;-67;9;00:E0:4C:5F:91:EC\r\n;-87;9;5C:6A:80:87:C0:39\r\n;-75;9;00:30:0A:C2:98:0C\r\n;-82;9;C0:25:E9:FB:3F:B9\r\n;-71;9;00:E0:4C:5F:81:78\r\n;-68;9;00:E0:4C:5F:B0:D8\r\n;-77;9;10:22:33:44:09:49\r\n;-92;9;C4:E9:84:A5:03:04\r\n;-91;9;64:66:B3:B7:83:A6",
                ";-75;12;DE:4F:22:10:D5:61\r\n;-73;12;DE:4F:22:11:15:65\r\n;-78;12;DE:4F:22:11:30:DD\r\n;-58;12;00:E0:4C:5F:91:EC\r\n;-74;12;10:22:33:44:09:43\r\n;-55;12;00:E0:4C:5F:B0:D8\r\n;-74;12;00:E0:4C:5F:B2:B4\r\n;-93;12;C0:25:E9:FB:3F:B9\r\n;-60;12;00:30:0A:C2:98:0C\r\n;-75;12;00:E0:4C:5F:81:78\r\n;-73;12;10:22:33:44:09:49\r\n;-83;12;C4:E9:84:A5:03:04\r\n;-77;12;DE:4F:22:11:15:65\r\n;-73;12;DE:4F:22:10:D5:61\r\n;-78;12;DE:4F:22:11:30:DD\r\n;-72;12;00:E0:4C:5F:B2:B4\r\n;-74;12;10:22:33:44:09:43\r\n;-60;12;00:30:0A:C2:98:0C\r\n;-94;12;C0:25:E9:FB:3F:B9\r\n;-75;12;00:E0:4C:5F:81:78\r\n;-73;12;10:22:33:44:09:49\r\n;-59;12;00:E0:4C:5F:91:EC\r\n;-62;12;00:E0:4C:5F:B0:D8\r\n;-95;12;A4:2B:B0:DB:3F:4B\r\n;-84;12;C4:E9:84:A5:03:04",
                ";-71;11;DE:4F:22:11:15:65\r\n;-67;11;DE:4F:22:10:D5:61\r\n;-71;11;DE:4F:22:11:30:DD\r\n;-90;11;18:A6:F7:25:5E:F0\r\n;-57;11;00:E0:4C:5F:91:EC\r\n;-52;11;10:22:33:44:09:43\r\n;-51;11;00:E0:4C:5F:B0:D8\r\n;-74;11;00:E0:4C:5F:B2:B4\r\n;-61;11;10:22:33:44:09:49\r\n;-54;11;00:30:0A:C2:98:0C\r\n;-76;11;C0:25:E9:FB:3F:B9\r\n;-80;11;00:E0:4C:5F:81:78\r\n;-84;11;C4:E9:84:A5:03:04\r\n;-73;11;DE:4F:22:11:30:DD\r\n;-66;11;DE:4F:22:10:D5:61\r\n;-71;11;DE:4F:22:11:15:65\r\n;-87;11;18:A6:F7:25:5E:F0\r\n;-53;11;10:22:33:44:09:43\r\n;-80;11;00:E0:4C:5F:81:78\r\n;-54;11;10:22:33:44:09:49\r\n;-53;11;00:30:0A:C2:98:0C\r\n;-76;11;C0:25:E9:FB:3F:B9\r\n;-74;11;00:E0:4C:5F:B2:B4\r\n;-50;11;00:E0:4C:5F:B0:D8\r\n;-49;11;00:E0:4C:5F:91:EC\r\n;-84;11;C4:E9:84:A5:03:04\r\n;-71;11;DE:4F:22:11:15:65\r\n;-67;11;DE:4F:22:10:D5:61\r\n;-73;11;DE:4F:22:11:30:DD\r\n;-90;11;18:A6:F7:25:5E:F0\r\n;-53;11;10:22:33:44:09:49\r\n;-72;11;00:E0:4C:5F:B2:B4\r\n;-81;11;00:E0:4C:5F:81:78\r\n;-80;11;C0:25:E9:FB:3F:B9\r\n;-56;11;00:30:0A:C2:98:0C\r\n;-48;11;00:E0:4C:5F:91:EC\r\n;-53;11;10:22:33:44:09:43\r\n;-51;11;00:E0:4C:5F:B0:D8",
                ";-72;13;DE:4F:22:11:30:DD\r\n;-68;13;DE:4F:22:11:15:65\r\n;-67;13;DE:4F:22:10:D5:61\r\n;-80;13;18:A6:F7:25:5E:F0\r\n;-57;13;10:22:33:44:09:49\r\n;-67;13;00:E0:4C:5F:B2:B4\r\n;-81;13;00:E0:4C:5F:81:78\r\n;-52;13;00:30:0A:C2:98:0C\r\n;-82;13;C0:25:E9:FB:3F:B9\r\n;-64;13;00:E0:4C:5F:91:EC\r\n;-55;13;00:E0:4C:5F:B0:D8\r\n;-60;13;10:22:33:44:09:43\r\n;-80;13;C4:E9:84:A5:03:04\r\n;-90;13;A4:2B:B0:DB:3F:4B\r\n;-66;13;DE:4F:22:10:D5:61\r\n;-71;13;DE:4F:22:11:15:65\r\n;-75;13;DE:4F:22:11:30:DD\r\n;-85;13;18:A6:F7:25:5E:F0\r\n;-76;13;00:E0:4C:5F:81:78\r\n;-60;13;10:22:33:44:09:49\r\n;-68;13;00:E0:4C:5F:B2:B4\r\n;-50;13;00:30:0A:C2:98:0C\r\n;-81;13;C0:25:E9:FB:3F:B9\r\n;-56;13;00:E0:4C:5F:B0:D8\r\n;-67;13;00:E0:4C:5F:91:EC\r\n;-62;13;10:22:33:44:09:43\r\n;-79;13;C4:E9:84:A5:03:04\r\n;-91;13;A4:2B:B0:DB:3F:4B\r\n;-71;13;DE:4F:22:11:15:65\r\n;-70;13;DE:4F:22:10:D5:61\r\n;-75;13;DE:4F:22:11:30:DD\r\n;-84;13;18:A6:F7:25:5E:F0\r\n;-75;13;00:E0:4C:5F:81:78\r\n;-59;13;10:22:33:44:09:49\r\n;-68;13;00:E0:4C:5F:B2:B4\r\n;-51;13;00:30:0A:C2:98:0C\r\n;-82;13;C0:25:E9:FB:3F:B9\r\n;-54;13;00:E0:4C:5F:B0:D8",
                ";-93;9;18:A6:F7:25:5E:F0\r\n;-91;9;DE:4F:22:11:15:65\r\n;-93;9;DE:4F:22:10:D5:61\r\n;-75;9;00:E0:4C:5F:81:78\r\n;-82;9;10:22:33:44:09:49\r\n;-89;9;E8:37:7A:DD:51:29\r\n;-69;9;00:E0:4C:5F:B0:D8\r\n;-89;9;5C:6A:80:87:C0:39\r\n;-74;9;00:30:0A:C2:98:0C\r\n;-83;9;C0:25:E9:FB:3F:B9\r\n;-52;9;00:E0:4C:5F:B2:B4\r\n;-80;9;10:22:33:44:09:43\r\n;-58;9;00:E0:4C:5F:91:EC\r\n;-93;9;C4:E9:84:A5:03:04\r\n;-89;9;64:66:B3:B7:83:A6",
                ";-72;12;DE:4F:22:11:15:65\r\n;-75;12;DE:4F:22:11:30:DD\r\n;-74;12;DE:4F:22:10:D5:61\r\n;-70;12;00:E0:4C:5F:B2:B4\r\n;-49;12;00:E0:4C:5F:91:EC\r\n;-75;12;10:22:33:44:09:43\r\n;-55;12;00:E0:4C:5F:B0:D8\r\n;-74;12;10:22:33:44:09:49\r\n;-58;12;00:30:0A:C2:98:0C\r\n;-79;12;C0:25:E9:FB:3F:B9\r\n;-77;12;00:E0:4C:5F:81:78\r\n;-79;12;C4:E9:84:A5:03:04\r\n;-76;12;DE:4F:22:10:D5:61\r\n;-77;12;DE:4F:22:11:15:65\r\n;-77;12;DE:4F:22:11:30:DD\r\n;-73;12;00:E0:4C:5F:B2:B4\r\n;-75;12;00:E0:4C:5F:81:78\r\n;-73;12;10:22:33:44:09:49\r\n;-61;12;00:30:0A:C2:98:0C\r\n;-74;12;10:22:33:44:09:43\r\n;-51;12;00:E0:4C:5F:91:EC\r\n;-54;12;00:E0:4C:5F:B0:D8\r\n;-83;12;C4:E9:84:A5:03:04\r\n;-92;12;A4:2B:B0:DB:3F:4B\r\n;-75;12;DE:4F:22:10:D5:61\r\n;-77;12;DE:4F:22:11:15:65\r\n;-76;12;DE:4F:22:11:30:DD\r\n;-63;12;00:E0:4C:5F:B0:D8\r\n;-75;12;00:E0:4C:5F:81:78\r\n;-51;12;00:E0:4C:5F:91:EC\r\n;-74;12;10:22:33:44:09:43\r\n;-60;12;00:30:0A:C2:98:0C\r\n;-74;12;10:22:33:44:09:49\r\n;-79;12;C4:E9:84:A5:03:04\r\n;-93;12;A4:2B:B0:DB:3F:4B\r\n;-72;12;00:E0:4C:5F:B2:B4",
                ";-68;10;DE:4F:22:11:30:DD\r\n;-69;10;DE:4F:22:11:15:65\r\n;-69;10;DE:4F:22:10:D5:61\r\n;-91;10;18:A6:F7:25:5E:F0\r\n;-63;10;10:22:33:44:09:49\r\n;-81;10;00:E0:4C:5F:B2:B4\r\n;-55;10;00:E0:4C:5F:91:EC\r\n;-61;10;10:22:33:44:09:43\r\n;-93;10;5C:6A:80:87:C0:39\r\n;-51;10;00:30:0A:C2:98:0C\r\n;-67;10;C0:25:E9:FB:3F:B9\r\n;-82;10;00:E0:4C:5F:81:78\r\n;-47;10;00:E0:4C:5F:B0:D8\r\n;-68;10;C4:E9:84:A5:03:04\r\n;-81;10;A4:2B:B0:DB:3F:4B\r\n;-69;10;DE:4F:22:11:15:65\r\n;-69;10;DE:4F:22:11:30:DD\r\n;-68;10;DE:4F:22:10:D5:61\r\n;-71;10;00:E0:4C:5F:81:78\r\n;-63;10;10:22:33:44:09:49\r\n;-80;10;00:E0:4C:5F:B2:B4\r\n;-50;10;00:30:0A:C2:98:0C\r\n;-65;10;C0:25:E9:FB:3F:B9\r\n;-50;10;00:E0:4C:5F:B0:D8\r\n;-62;10;10:22:33:44:09:43\r\n;-54;10;00:E0:4C:5F:91:EC\r\n;-83;10;A4:2B:B0:DB:3F:4B\r\n;-75;10;C4:E9:84:A5:03:04",
                ";-72;11;DE:4F:22:11:30:DD\r\n;-67;11;DE:4F:22:10:D5:61\r\n;-71;11;DE:4F:22:11:15:65\r\n;-79;11;00:E0:4C:5F:81:78\r\n;-54;11;10:22:33:44:09:49\r\n;-74;11;00:E0:4C:5F:B2:B4\r\n;-53;11;00:30:0A:C2:98:0C\r\n;-75;11;C0:25:E9:FB:3F:B9\r\n;-50;11;00:E0:4C:5F:B0:D8\r\n;-51;11;10:22:33:44:09:43\r\n;-48;11;00:E0:4C:5F:91:EC\r\n;-83;11;C4:E9:84:A5:03:04",
                ";-71;13;DE:4F:22:11:15:65\r\n;-69;13;DE:4F:22:10:D5:61\r\n;-75;13;DE:4F:22:11:30:DD\r\n;-84;13;18:A6:F7:25:5E:F0\r\n;-60;13;10:22:33:44:09:43\r\n;-64;13;00:E0:4C:5F:91:EC\r\n;-54;13;00:E0:4C:5F:B0:D8\r\n;-93;13;E8:37:7A:DD:51:29\r\n;-49;13;00:30:0A:C2:98:0C\r\n;-83;13;C0:25:E9:FB:3F:B9\r\n;-65;13;00:E0:4C:5F:B2:B4\r\n;-58;13;10:22:33:44:09:49\r\n;-74;13;00:E0:4C:5F:81:78\r\n;-78;13;C4:E9:84:A5:03:04\r\n;-92;13;A4:2B:B0:DB:3F:4B",
                ";-90;9;DE:4F:22:10:D5:61\r\n;-88;9;DE:4F:22:11:15:65\r\n;-90;9;18:A6:F7:25:5E:F0\r\n;-70;9;00:E0:4C:5F:B0:D8\r\n;-72;9;00:E0:4C:5F:81:78\r\n;-78;9;10:22:33:44:09:49\r\n;-64;9;00:E0:4C:5F:91:EC\r\n;-82;9;10:22:33:44:09:43\r\n;-72;9;C0:25:E9:FB:3F:B9\r\n;-76;9;00:30:0A:C2:98:0C\r\n;-47;9;00:E0:4C:5F:B2:B4\r\n;-93;9;A4:2B:B0:DB:3F:4B\r\n;-90;9;C4:E9:84:A5:03:04\r\n;-90;9;64:66:B3:B7:83:A6"
            };
            while (true)
            {
                client.Client.Send(Encoding.UTF8.GetBytes(data[r.Next(data.Length)]), SocketFlags.None);
                await Task.Delay(10);
            }

            // await client.DisconnectAsync();
        }
    }
}