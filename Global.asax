<%@ Application Language="C#" %>
<%@ Import Namespace="System.Data.SqlClient"  %>
<script runat="server">

    public class Time_Task
    {
        public event System.Timers.ElapsedEventHandler ExecuteTask;

        private static readonly Time_Task _task = null;
        private System.Timers.Timer _timer = null;
        private int _interval = 60000;

        public int Interval
        {
            set
            {
                _interval = 60000;
            }
            get
            {
                return _interval;
            }
        }

        static Time_Task()
        {
            _task = new Time_Task();
        }

        public static Time_Task Instance()
        {
            return _task;
        }

        public void Start()
        {
            if (_timer == null)
            {
                _timer = new System.Timers.Timer(_interval);
                _timer.Elapsed += new System.Timers.ElapsedEventHandler(_timer_Elapsed);
                _timer.Enabled = true;
                _timer.Start();
            }
        }

        protected void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (null != ExecuteTask)
            {
                ExecuteTask(sender, e);
            }
        }

        public void Stop()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
                _timer = null;
            }
        }
    }



    void Application_Start(object sender, EventArgs e) 
    {
        // 在应用程序启动时运行的代码

        Time_Task.Instance().ExecuteTask += new System.Timers.ElapsedEventHandler(Global_ExecuteTask);
        Time_Task.Instance().Interval = 60000;//表示间隔1分钟
        Time_Task.Instance().Start();


        //int intHour = DateTime.Now.Hour;
        //int intMinute = DateTime.Now.Minute;
        //int intSecond = DateTime.Now.Second; // 定制时间,在00：00：00 的时候执行
        //int iHour = 13; int iMinute = 51; int iSecond = 1; // 设置 每天的00：00：00开始执行程序
        //if (intHour == iHour && intMinute == iMinute)
        //{
        //    string a = iHour.ToString() + iMinute.ToString() + iSecond.ToString() + "--" + intHour.ToString() + intMinute.ToString() + intSecond.ToString();
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //    con.Open();
        //    string sql = "insert into taskchaoqiday values('" + DateTime.Now.Hour + "-"+intMinute+"-"+intSecond+"')";
        //    SqlCommand cmd = new SqlCommand(sql, con);
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //}
        
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  在应用程序关闭时运行的代码
        string dd = "0";


        
        
        Session.Clear();
        Session.Abandon();

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // 在出现未处理的错误时运行的代码

    }

    void Session_Start(object sender, EventArgs e) 
    {
     


    }

    void Session_End(object sender, EventArgs e) 
    {
        // 在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为 InProc 时，才会引发 Session_End 事件。
        // 如果会话模式设置为 StateServer 
        // 或 SQLServer，则不会引发该事件。

        //Hashtable h = (Hashtable)Application["Online"];

        //if (h[Session.SessionID] != null)
        //    h.Remove(Session.SessionID);

        //Application["Online"] = h;

        //Session.Abandon();

        //Hashtable hOnline = (Hashtable)Application["Online"];
        //if (hOnline[Session.SessionID] != null)
        //{
        //    hOnline.Remove(Session.SessionID);//清除当前SessionID
        //    Application.Lock();
        //    Application["Online"] = hOnline;
        //    Application.UnLock();
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        //    con.Open();
        //    string sql = "delete from logininfo2";
        //    SqlCommand cmd = new SqlCommand(sql, con);
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //}
       
        //Session.Abandon();
        
    }
    void Global_ExecuteTask(object sender, System.Timers.ElapsedEventArgs e)
    {
      
       int intHour = e.SignalTime.Hour;
       int intMinute = e.SignalTime.Minute;
       int intSecond = e.SignalTime.Second; // 定制时间,在00：00：00 的时候执行
       int iHour = 8; int iMinute = 8; int iSecond = 22; // 设置 每天的00：00：00开始执行程序
       if (intHour == iHour && intMinute == iMinute )
       {
           SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
           con.Open();
           DateTime time3 = DateTime.Now;
           string sql21 = "select hdate from tb_Holiday where convert(varchar,hdate,23)='" + time3.ToString("yyyy-MM-dd") + "' and name !='工作日'";
           SqlCommand cmd21 = new SqlCommand(sql21, con);
           SqlDataReader dr21 = cmd21.ExecuteReader();
           if (dr21.Read())
           {
               dr21.Close();
               con.Close();
           }

           else
           {

               dr21.Close();
               string sql = "select * from taskchaoqiday  where day='" + DateTime.Now.ToShortDateString() + "'";
               SqlCommand cmd = new SqlCommand(sql, con);
               SqlDataReader dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   con.Close();
               }
               else
               {

                   dr.Close();

                   string sql2 = "insert into taskchaoqiday values('" + DateTime.Now.ToShortDateString() + "','"+DateTime.Now+"')";
                   SqlCommand cmd2 = new SqlCommand(sql2, con);
                   cmd2.ExecuteNonQuery();
                   con.Close();

                   Common.searchwhere sx4 = new Common.searchwhere();
                   string sjt1 = sx4.ShiXiao("1");


                   //Response.Write(sjt1);

               }
           }
           con.Close();
       }
   
    }
    
     
       
</script>
