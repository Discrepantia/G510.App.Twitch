using G510.App.API_Contract;
using G510.App.Converter;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Twitch.Net.Model;

namespace G510.App
{
    public class MainWindow_ViewModel : ViewModelBase
    {
        const String TwitchBaseUrl = "https://api.twitch.tv/kraken/";
        DispatcherTimer dispatcherTimer;
        WebClient G510AppClient;
        WebClient DefaultWebClient;

        private String m_UserToWatch;
        public String UserToWatch { get { return m_UserToWatch; } set { m_UserToWatch = value; RaisePorpertyChanged(); } }
        private String m_StateText;
        public String StateText { get { return m_StateText; } set { m_StateText = value; RaisePorpertyChanged(); } }

        private Boolean m_State;
        public Boolean State { get { return m_State; } set { m_State = value; RaisePorpertyChanged(); } }

        private String m_SearchStringStreams;
        public String SearchStringStreams { get { return m_SearchStringStreams; } set { m_SearchStringStreams = value; RaisePorpertyChanged(); } }

        private String m_SearchStringGames;
        public String SearchStringGames { get { return m_SearchStringGames; } set { m_SearchStringGames = value; RaisePorpertyChanged(); } }

        private Streamlist m_StreamsDisplay;
        public Streamlist StreamsDisplay { get { return m_StreamsDisplay; } set { m_StreamsDisplay = value; RaisePorpertyChanged(); } }

        private Gamelist m_GamesDisplay;
        public Gamelist GamesDisplay { get { return m_GamesDisplay; } set { m_GamesDisplay = value; RaisePorpertyChanged(); } }

        private String m_SteamsStringAddition;
        public String SteamsStringAddition { get { return m_SteamsStringAddition; } set { m_SteamsStringAddition = value; RaisePorpertyChanged(); } }

        private Twitch.Net.Model.StreamResult m_CurrentStramG150;
        public Twitch.Net.Model.StreamResult CurrentStramG150 { get { return m_CurrentStramG150; } set { m_CurrentStramG150 = value; RaisePorpertyChanged(); } }

        public static ICommand m_stUserAction;
        public static ICommand stUserAction { get { return m_stUserAction; } }

        LogiLcd myLCD;

        String enabledText = "Enabled";
        String disabledText = "Disabled";

        TimeSpan _updateTimespan = new TimeSpan(0, 0, 5);

        /// <summary>
        /// the designer get on my nerves i override finalize to avoid errors
        /// </summary>
        new void finalize()
        {
            
        }

        private String UserToWatchInternal { get; set; }

        public MainWindow_ViewModel()
        {
            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                //var xx = Assembly.LoadFile(LedPinvoke.DLLPath);

                Initialize();
                InitializeCommands();

                this.PropertyChanged += MainWindow_ViewModel_PropertyChanged;

                LateInitialize();
            }
        }

        ~MainWindow_ViewModel()
        {
            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                myLCD.Dispose();
        }

        public void Initialize()
        {
            StateText = disabledText;

            UserToWatchInternal = "discrepantia";

            myLCD = new LogiLcd("TwitchWatcher", LogiLcd.LcdType.Mono);
            G510AppClient = new WebClient();
            DefaultWebClient = new WebClient();
            G510AppClient.DownloadStringCompleted += client_DownloadStringCompleted;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = _updateTimespan;
            dispatcherTimer.Start();

            StreamsDisplay = new Streamlist();
            GamesDisplay = new Gamelist();
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            dispatcherTimer.IsEnabled = false;
            if (State && myLCD.IsConnected(LogiLcd.LcdType.Mono))
            {
                var _url = new Uri(TwitchBaseUrl + "streams/" + UserToWatchInternal.ToLower());
                G510AppClient.DownloadStringAsync(_url);
            }
            dispatcherTimer.IsEnabled = true;
        }

        int __channelNamePosition = 0;
        int __channelStatusPosition = 0;
        int __gameNamePosition = 0;
        const int ammountdisplaychars = 30;
        const int stepsizeDisplayOverflow = 5;
        bool colorred;
        private void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (State)
            {
                dispatcherTimer.IsEnabled = false;
                if (e.Error != null)
                {
                    
                    if (!colorred)
                    {
                        colorred = true;
                        myLCD.StoreColor();
                        myLCD.ChangeColor(100, 0, 0);
                    }
                    myLCD.MonoClear();
                    myLCD.MonoSetText(2, "Channel not found");
                    myLCD.Update();
                }
                else
                    if (!e.Cancelled && myLCD.IsConnected(LogiLcd.LcdType.Mono))
                    {
                        try
                        {
                            CurrentStramG150 = JsonConvert.DeserializeObject<Twitch.Net.Model.StreamResult>(e.Result);

                            string channelName = "Channel: " + CurrentStramG150.Stream.Channel.DisplayName;
                            string streamStatus = CurrentStramG150.Stream.Channel.Status;
                            string gameName = CurrentStramG150.Stream.Game;

                            #region simulate MessageBoard

                            //quick and dirty
                            if (streamStatus.Length > ammountdisplaychars)
                            {
                                if (__channelStatusPosition > ammountdisplaychars)
                                    __channelStatusPosition = 0;
                                else
                                    __channelStatusPosition += stepsizeDisplayOverflow;
                            }
                            else
                                __channelStatusPosition = 0;


                            if (gameName.Length > ammountdisplaychars)
                            {
                                if (__gameNamePosition > ammountdisplaychars)
                                    __gameNamePosition = 0;
                                else
                                    __gameNamePosition += stepsizeDisplayOverflow;
                            }
                            else
                                __gameNamePosition = 0;

                            if (channelName.Length > ammountdisplaychars)
                            {
                                if (__channelNamePosition > ammountdisplaychars)
                                    __channelNamePosition = 0;
                                else
                                    __channelNamePosition += stepsizeDisplayOverflow;
                            }
                            else
                                __channelNamePosition = 0;

                            #endregion

                            myLCD.MonoSetText(0, channelName.Substring(__channelNamePosition));
                            myLCD.MonoSetText(1, gameName.Substring(__gameNamePosition));
                            myLCD.MonoSetText(2, streamStatus.Substring(__channelStatusPosition));


                            //I dont think that this part overflows
                            myLCD.MonoSetText(3, "Viewers: " + CurrentStramG150.Stream.Viewers);

                            if (colorred)
                            {
                                myLCD.RestoreColor();
                                colorred = false;
                            }

                            myLCD.Update();
                        }
                        catch {
                            myLCD.MonoSetBackground(new byte[LogiLcd.MonoHeight * LogiLcd.MonoWidth]);
                            if (!colorred)
                            {
                                colorred = true;
                                myLCD.StoreColor();
                                myLCD.ChangeColor(100, 0, 0);
                            }
                            myLCD.MonoClear();
                            myLCD.MonoSetText(2, "Channel offline:");
                            myLCD.Update();
                        }
                    }

                dispatcherTimer.IsEnabled = true;
            }
        }


        public void LateInitialize()
        {

        }


        public void InitializeCommands()
        {
            m_stUserAction = UserAction = new BaseCommand(User_Action);
        }

        public void User_Action(object obj)
        {
            String Action = "";
            if (obj != null)
                Action = obj.ToString();
            if (obj is MultiValueHolder)
            {
                Action = (obj as MultiValueHolder).Parameter.ToString();
            }

            try
            {

                switch (Action)
                {
                    case "UPDATETARGET":

                        if (!String.IsNullOrEmpty(UserToWatch))
                        {
                            UserToWatchInternal = UserToWatch;
                            State = true;
                        }

                        //myLCD.MonoSetText(0, new String(' ', LogiLcd.MonoWidth));
                        //if (myLCD.IsConnected(LogiLcd.LcdType.Mono))
                        //{
                        //    myLCD.MonoSetText(0, "Channel: " + UserToWatch);
                        //    myLCD.Update();
                        //}


                        break;

                    case "CHANGESTATE":

                        State = !State;

                        break;

                    case "SEARCHSTREAMS":
                        //https://api.twitch.tv/kraken/streams?limit=25&offset=0
                        try
                        {
                            var _uri = new Uri(TwitchBaseUrl + "search/streams/?limit=100&offset=0&type=suggest&q=\"" + SearchStringStreams + "\"");
                            String resultString = DefaultWebClient.DownloadString(_uri);

                            var result = JsonConvert.DeserializeObject<Streamlist>(resultString);

                            StreamsDisplay = result;

                        }
                        catch { SearchStringStreams = "Can't search for this String"; }
                        break;

                    case "STREAMSLOADNEXT":

                        try
                        {//https://api.twitch.tv/kraken/streams?limit=25&offset=0

                            var _uri = new Uri(StreamsDisplay.Links["next"].ToString());
                            String resultString = DefaultWebClient.DownloadString(_uri);

                            var result =
                                JsonConvert.DeserializeObject<Streamlist>(resultString);

                            StreamsDisplay.Total = result.Total;

                            if (result.Streams.Count > 0)
                            {
                                foreach (var item in result.Streams)
                                {
                                    StreamsDisplay.Streams.Add(item);
                                }

                                StreamsDisplay.Links = result.Links;
                                
                            }
                            else
                            {
                                //dont let the list oversize
                                var limit = StreamsDisplay.Total - StreamsDisplay.Streams.Count;
                                if (limit < 0)
                                    StreamsDisplay.Links["next"] = "";
                                else
                                    StreamsDisplay.Links["next"] = (TwitchBaseUrl + "search/streams/?limit="+limit+"&offset="+StreamsDisplay.Streams.Count+"&type=suggest&q=\"" + SearchStringStreams + "\"");
                            }


                        }
                        catch { }

                        break;

                    case "SEARCHGAMES":
                        //https://api.twitch.tv/kraken/games?limit=25&offset=0
                        try
                        {
                            var _uri = new Uri(TwitchBaseUrl + "search/games/?hls=true&type=suggest&q=" + SearchStringGames);
                            String resultString = DefaultWebClient.DownloadString(_uri);

                            var result =
                                JsonConvert.DeserializeObject<Gamelist>(resultString);

                            GamesDisplay = result;

                        }
                        catch { SearchStringGames = "Can't search for this String"; }
                        break;

                    case "GAMESLOADNEXT":

                        try
                        {//https://api.twitch.tv/kraken/games?limit=25&offset=0

                            String resultString = DefaultWebClient.DownloadString(new Uri(StreamsDisplay.Links["next"].ToString()));

                            var result =
                                JsonConvert.DeserializeObject<Gamelist>(resultString);

                            GamesDisplay.Total = result.Total;

                            if (result.Games.Count > 0)
                            {
                                foreach (var item in result.Games)
                                {
                                    GamesDisplay.Games.Add(item);
                                }
                            }
                            else
                            {
                                //dont let the list oversize
                                var limit = StreamsDisplay.Total - GamesDisplay.Games.Count;
                                if (limit < 0)
                                    StreamsDisplay.Links["next"] = "";
                                else
                                    StreamsDisplay.Links["next"] = (TwitchBaseUrl + "search/games/?limit=" + limit + "&offset=" + GamesDisplay.Games.Count + "&type=suggest&q=\"" + SearchStringGames + "\"");
                            }


                        }
                        catch { }

                        break;

                    case "CHANGEG510STALKEDCHANNEL":

                        try
                        {
                            UserToWatchInternal = ((obj as MultiValueHolder).Values.First() as Stream).Channel.DisplayName;
                            State = true;
                        }
                        catch
                        {
                        }


                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine("<" + this.GetType().ToString() + ">" + new StackTrace().GetFrame(0).ToString() + " Action: " + Action + " Error: " + ex.Message);
            }
        }


        //I dont understand the Generc TwitchList
        public class Streamlist : ViewModelBase
        {
            [JsonProperty("streams")]
            public ObservableCollection<Twitch.Net.Model.Stream> m_Streams;

            public ObservableCollection<Twitch.Net.Model.Stream> Streams { get { return m_Streams; } set { m_Streams = value; RaisePorpertyChanged(); } }


            [JsonProperty("_links")]
            public Dictionary<string, object> m_Links;
            public Dictionary<string, object> Links { get { return m_Links; } set { m_Links = value; RaisePorpertyChanged(); } }

            [JsonProperty("_total")]
            public long m_Total;
            public long Total { get { return m_Total; } set { m_Total = value; RaisePorpertyChanged(); } }
        }

        public class Gamelist : ViewModelBase
        {
            [JsonProperty("games")]
            public ObservableCollection<Twitch.Net.Model.Game> m_Games;

            public ObservableCollection<Twitch.Net.Model.Game> Games { get { return m_Games; } set { m_Games = value; RaisePorpertyChanged(); } }


            [JsonProperty("_links")]
            public Dictionary<string, object> m_Links;
            public Dictionary<string, object> Links { get { return m_Links; } set { m_Links = value; RaisePorpertyChanged(); } }

            [JsonProperty("_total")]
            public long m_Total;
            public long Total { get { return m_Total; } set { m_Total = value; RaisePorpertyChanged(); } }
        }

        void MainWindow_ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "State":
                    StateText = State ? enabledText : disabledText;
                    if (!State)
                    {
                        if (myLCD.IsConnected(LogiLcd.LcdType.Mono))
                        {
                            myLCD.MonoSetText(1, "Disabled");
                            myLCD.Update();
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        public ICommand UserAction { get; set; }
    }
}
