using SteamKit2;
using SteamKit2.Unified.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteamKit2.Discovery;

namespace sccdownloader
{
    public partial class main : Form
    {
        bool isRunning = true;

        bool isReady = false;

        bool requestOnGoing = false;

        uint appidCurrent = 0;

        /// <summary>
        /// Items to requests per query, 100 is maximum allowed by Steam
        /// </summary>
        uint itemsPerPage = 100;

        List<Game> Games = new List<Game>();

        BindingList<ConfigItem> items = new BindingList<ConfigItem>();

        List<string> names = new List<string>();

        #region "Steam Connection"
        Thread steamThread;

        SteamClient steamClient;
        CallbackManager manager;
        SteamUser steamUser;
        SteamWorkshop steamWorkshop;

        string username = "";
        string password = "";
        string loginkey = "";
        string steamguard;
        string twofactor;
        bool doReconnect = true;
        bool rememberLogin = false;

        void steam_connection()
        {
            var cellid = 0u;

            // if we've previously connected and saved our cellid, load it.
            if ( File.Exists( "cellid.txt" ) )
            {
                if ( !uint.TryParse( File.ReadAllText( "cellid.txt"), out cellid ) )
                {
                    Console.WriteLine( "Error parsing cellid from cellid.txt. Continuing with cellid 0." );
                    Log.w("Error parsing cellid from cellid.txt. Continuing with cellid 0.");
                    cellid = 0;
                }
                else
                {
                    Console.WriteLine( $"Using persisted cell ID {cellid}" );
                    Log.w($"Using persisted cell ID {cellid}");
                }
            }

            var config = SteamConfiguration.Create(b =>
                b.WithCellID(cellid)
                    .WithServerListProvider(new FileStorageServerListProvider("servers_list.bin")));
            Log.w("Created new server list");

            steamClient = new SteamClient(config);
            Log.w("Creating client...");
            steamWorkshop = new SteamWorkshop();
            SteamDirectory.LoadAsync(config);
            Log.w("Loading steam directory");
            steamClient.AddHandler(steamWorkshop);

            manager = new CallbackManager(steamClient);

            steamUser = steamClient.GetHandler<SteamUser>();

            manager.Subscribe<SteamClient.ConnectedCallback>(OnConnected);
            manager.Subscribe<SteamUser.LoggedOnCallback>(OnLoggedOn);
            manager.Subscribe<SteamUser.LoggedOffCallback>(OnLoggedOff);
            manager.Subscribe<SteamClient.DisconnectedCallback>(OnDisconnect);
            manager.Subscribe<SteamUser.UpdateMachineAuthCallback>(OnMachineAuth);
            manager.Subscribe<SteamUser.LoginKeyCallback>(OnLoginKey);

            steamClient.Connect();
            Log.w("Connecting to steam...");
            while (isRunning)
            {
                try {
                    manager.RunWaitCallbacks(TimeSpan.FromSeconds(1));
                }
                catch (Exception)
                {
                }
            }
        }

        void OnConnected(SteamClient.ConnectedCallback callback)
        {
            setStatus("Connected to Steam!");
            Log.w("Connected!");

            /*
            if (callback.Result != EResult.OK)
            {
                MessageBox.Show("Failed to connect to steam");
                Environment.Exit(0);
            } */

            if (string.IsNullOrEmpty(username))
                steamUser.LogOnAnonymous();
            else
            {
                byte[] sentryHash = null;

                if (File.Exists("steamguard.bin"))
                {
                    Log.w("Steam Guard exists, using existing config");
                    // if we have a saved sentry file, read and sha-1 hash it
                    byte[] sentryFile = File.ReadAllBytes("steamguard.bin");
                    sentryHash = CryptoHelper.SHAHash(sentryFile);
                }

                steamUser.LogOn(new SteamUser.LogOnDetails
                {
                    Username = username,
                    Password = password,
                    LoginKey = loginkey,
                    AuthCode = steamguard,
                    TwoFactorCode = twofactor,
                    ShouldRememberPassword = rememberLogin,
                    LoginID = 1
                });
            }
        }

        void OnDisconnect(SteamClient.DisconnectedCallback callback)
        {
            setStatus("Disconnected from Steam!");
            Log.w("disconnected from Steam");

            if (doReconnect && !callback.UserInitiated)
            {
                Task.Delay(TimeSpan.FromSeconds(5)).ContinueWith(x=>steamClient.Connect());
            }
            else if (callback.UserInitiated)
                isRunning = false;
        }

        void OnLoggedOn(SteamUser.LoggedOnCallback callback)
        {
            if (callback.Result != EResult.OK)
            {
                doReconnect = false;

                if (callback.Result == EResult.AccountLogonDenied)
                {
                    using (var dialog = new SteamGuard())
                    {
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            steamguard = dialog.getCodeInput();
                            steamClient.Connect();
                            return;
                        }
                    }
                }
                else if (callback.Result == EResult.AccountLoginDeniedNeedTwoFactor ||
                         callback.Result == EResult.TwoFactorCodeMismatch)
                {
                    using (var dialog = new SteamGuard())
                    {
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            twofactor = dialog.getCodeInput();
                            steamClient.Connect();
                            return;
                        }
                    }
                }

                MessageBox.Show("Failed to connect to steam");
                Environment.Exit(0);
            }
            else if (callback.Result == EResult.OK)
            {
                isReady = true;
                doReconnect = true;
                steamguard = twofactor = "";

                setStatus("Logged to Steam!");
            }
            else
            {
                
            }
        }

        private void OnLoginKey(SteamUser.LoginKeyCallback callback)
        {
            if (rememberLogin)
                File.WriteAllText("login.dat", $"{username}|{callback.LoginKey}");
        }

        void OnLoggedOff(SteamUser.LoggedOffCallback callback)
        {
        }

        void OnMachineAuth(SteamUser.UpdateMachineAuthCallback callback)
        {
            int fileSize;
            byte[] sentryHash;
            using (var fs = File.Open("steamguard.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                fs.Seek(callback.Offset, SeekOrigin.Begin);
                fs.Write(callback.Data, 0, callback.BytesToWrite);
                fileSize = (int)fs.Length;

                fs.Seek(0, SeekOrigin.Begin);
                using (var sha = new SHA1CryptoServiceProvider())
                {
                    sentryHash = sha.ComputeHash(fs);
                }
            }

            // inform the steam servers that we're accepting this sentry file
            steamUser.SendMachineAuthResponse(new SteamUser.MachineAuthDetails
            {
                JobID = callback.JobID,

                FileName = callback.FileName,

                BytesWritten = callback.BytesToWrite,
                FileSize = fileSize,
                Offset = callback.Offset,

                Result = EResult.OK,
                LastError = 0,

                OneTimePassword = callback.OneTimePassword,

                SentryFileHash = sentryHash,
            });
        }
        #endregion

        void setStatus(string text)
        {
            try
            {
                Invoke(new MethodInvoker(delegate
                {
                    currentStatus.Text = text;
                }));
            }
            catch (Exception)
            {
            }
        }

        void setStatus(string text, params object[] args)
        {
            setStatus(string.Format(text, args));
        }

        async void startNewRequest()
        {
            if (!isReady)
            {
                MessageBox.Show("Waiting for connection!");
                return;
            }

            if (requestOnGoing)
            {
                MessageBox.Show("Waiting for results");
                return;
            }

            if (!string.IsNullOrWhiteSpace(inputAppID.Text))
            {
                if (!uint.TryParse(inputAppID.Text.Trim(), out appidCurrent))
                {
                    var game = Games.Where(x => x.Name == inputAppID.Text).FirstOrDefault();

                    if (game != null)
                        appidCurrent = game.AppID;
                    else
                    {
                        MessageBox.Show("Invalid AppID");
                        return;
                    }
                }
            }
            else
            {
                appidCurrent = 0;
            }

            requestOnGoing = true;

            items.Clear();

            await sendRequest();
        }

        /// <summary>
        /// Sends request for controller configs
        /// </summary>
        async Task<bool> sendRequest(bool is_new = true)
        {
            requestOnGoing = true;

            try
            {
                uint pageCurrent = 1;
                uint totalPages = 0;

                var service = steamClient.GetHandler<SteamUnifiedMessages>().CreateService<IPublishedFile>();

                do
                {
                    if (totalPages > 0)
                        setStatus("Requesting page {0} of {1}", pageCurrent, totalPages);
                    else
                        setStatus("Requesting page {0}", pageCurrent, totalPages);

                    var query = new CPublishedFile_QueryFiles_Request
                    {
                        return_vote_data = true,
                        return_children = true,
                        return_for_sale_data = true,
                        return_kv_tags = true,
                        return_metadata = true,
                        return_tags = true,
                        return_previews = true,
                        appid = is_new ? 241100 : appidCurrent,
                        page = pageCurrent,
                        numperpage = itemsPerPage,
                        query_type = 11,
                        filetype = (uint)EWorkshopFileType.GameManagedItem,
                    };

                    if (is_new)
                    {
                        if (appidCurrent > 0)
                            query.required_kv_tags.Add(new CPublishedFile_QueryFiles_Request.KVTag() { key = "app", value = appidCurrent.ToString() });

                        query.required_kv_tags.Add(new CPublishedFile_QueryFiles_Request.KVTag() { key = "visibility", value = "public" });
                    }

                    var callback = await service.SendMessage(x => x.QueryFiles(query));
                    var response = callback.GetDeserializedResponse<CPublishedFile_QueryFiles_Response>();

                    totalPages = (uint)Math.Ceiling(response.total / (double)itemsPerPage);
                    pageCurrent++;

                    foreach (var item in response.publishedfiledetails)
                    {
                        var info = new ConfigItem
                        {
                            App = item.app_name,
                            Name = item.title,
                            FileName = item.filename.Split('/').Last(),
                            URL = item.file_url,
                            RatesUp = item.vote_data != null ? item.vote_data.votes_up : 0,
                            RatesDown = item.vote_data != null ? item.vote_data.votes_down : 0,
                            Details = item
                        };

                        foreach (var tag in item.kvtags)
                        {
                            if (tag.key == "app" || tag.key == "appid")
                            {
                                uint app = 0;

                                if (!uint.TryParse(tag.value, out app))
                                {
                                    info.App = $"(NON-STEAM) {tag.value}";
                                }
                                else
                                {
                                    var game = Games.Where(x => x.AppID == app).FirstOrDefault();

                                    if (game != null)
                                    {
                                        info.App = game.Name;
                                    }
                                    // We don't know actual name
                                    else
                                    {
                                        info.App = $"AppID {app}";
                                    }
                                }
                            }
                            else if (tag.key == "visibility" || tag.key == "deleted" || tag.key == "owner" || tag.key == "autosave")
                                continue;
                        }

                        Invoke(new MethodInvoker(delegate
                        {
                            items.Add(info);
                        }));
                    }
                }
                while (pageCurrent <= totalPages);
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                setStatus("Complete!");

                requestOnGoing = false;
            }

            return true;
        }

        public main()
        {
            InitializeComponent();

            if (File.Exists("login.dat"))
            {
                Log.w("Login data exists, using existing data.");
                var f = File.ReadAllText("login.dat").Split('|');
                username = f[0];
                loginkey = f[1];
                rememberLogin = true;
            }
            else
            {
                Log.w("Opening login form");
                using (var dialog = new SteamLoginFrm())
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        username = dialog.getUsername();
                        password = dialog.getPassword();
                        rememberLogin = dialog.getRememberLogin();
                    }
                }
            }

            steamThread = new Thread(steam_connection);
            steamThread.Start();

            if (File.Exists("games.txt"))
            {
                foreach (var line in File.ReadAllLines("games.txt"))
                {
                    var parts = line.Split(new char[] { '\t' }, 2);

                    Games.Add(new Game
                    {
                        AppID = uint.Parse(parts[0]),
                        Name = parts[1]
                    });
                }
            }
        }

        private void main_Load(object sender, EventArgs e)
        {
            configItemBindingSource.DataSource = items;

            var source = new AutoCompleteStringCollection();
            source.AddRange(Games.Select(x => x.Name).ToArray());

            inputAppID.AutoCompleteCustomSource = source;
            inputAppID.AutoCompleteSource = AutoCompleteSource.CustomSource;
            inputAppID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }

        private void get_Click(object sender, EventArgs e)
        {
            startNewRequest();
        }

        async private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            try
            {
                ConfigItem item = datagridConfigs.Rows[e.RowIndex].DataBoundItem as ConfigItem;

                if (item != null)
                {
                    if (item.URL == "")
                    {
                        var callback = await steamWorkshop.RequestInfo(241100, item.Details.publishedfileid);
                        var itemInfo = callback.Items.FirstOrDefault();
                        var ticket = await steamClient.GetHandler<SteamApps>().GetAppOwnershipTicket(241100);
                        var decryptKey = await steamClient.GetHandler<SteamApps>().GetDepotDecryptionKey(241100, 241100);
                        var cdn = new CDNClient(steamClient, ticket.Ticket);
                        // var servers = cdn.FetchServerList();
                        var servers = await cdn.FetchServerListAsync();
                        await cdn.ConnectAsync(servers.First());
                        await cdn.AuthenticateDepotAsync(241100, decryptKey.DepotKey);
                        var manifest = await cdn.DownloadManifestAsync(241100, itemInfo.ManifestID);
                        manifest.DecryptFilenames(decryptKey.DepotKey);
                        if (manifest.Files.Count == 0)
                        {
                            MessageBox.Show("Steam Refused Download Request");
                            return;
                        }
                        var chunk = cdn.DownloadDepotChunkAsync(241100, manifest.Files.First().Chunks.First());
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            using (var wc = new WebClient())
                            {
                                using (var io = saveFileDialog1.OpenFile())
                                {
                                    io.Write(chunk.Result.Data, 0, chunk.Result.Data.Length);
                                    MessageBox.Show("Download Done!");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            using (var wc = new WebClient())
                            {
                                wc.DownloadFile(new Uri(item.URL), saveFileDialog1.FileName);
                                MessageBox.Show("Download Done!");
                            }
                        }
                    }
                }
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show("Timeout! This can happen if you're using anonymous account and trying to download.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Downloading Config: {ex.ToString()}");
            }
        }

        private void main_FormClosed(object sender, FormClosedEventArgs e)
        {
            steamClient.Disconnect();
        }

        private void inputAppID_TextChanged(object sender, EventArgs e)
        {
        }

        private void inputAppID_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                startNewRequest();
            }
        }
    }

}
