namespace ExamSysWinform.WebService
{
    //using ExamSysWinform.Properties;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Threading;
    using System.Web.Services;
    using System.Web.Services.Description;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;

    [GeneratedCode("System.Web.Services", "4.0.30319.34209"), DebuggerStepThrough, DesignerCategory("code")]
    public class LoginCompletedEventArgs : AsyncCompletedEventArgs
    {
        private object[] results;

        internal LoginCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
            : base(exception, cancelled, userState)
        {
            this.results = results;
        }

        public ClientStudentModel Result
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return (ClientStudentModel)this.results[0];
            }
        }
    }


    [XmlInclude(typeof(ClientModelSuper)), GeneratedCode("System.Web.Services", "4.0.30319.34209"), DebuggerStepThrough, DesignerCategory("code"), WebServiceBinding(Name="StudentServiceSoap", Namespace="http://tempuri.org/")]
    public class StudentService : SoapHttpClientProtocol
    {
        private SendOrPostCallback ChangePwdOperationCompleted;
        private SendOrPostCallback getServerTimeOperationCompleted;
        private SendOrPostCallback getStuScoreOperationCompleted;
        private SendOrPostCallback getTemplateOperationCompleted;
        private SendOrPostCallback LoginOperationCompleted;
        private SendOrPostCallback saveStudentAnwserOperationCompleted;
        private SendOrPostCallback ShowStuPaperOperationCompleted;
        private SendOrPostCallback UpdateScoreOperationCompleted;
        private SendOrPostCallback updateStudentStatusOperationCompleted;
        private bool useDefaultCredentialsSetExplicitly;

        //public event ChangePwdCompletedEventHandler ChangePwdCompleted;

        //public event getServerTimeCompletedEventHandler getServerTimeCompleted;

        //public event getStuScoreCompletedEventHandler getStuScoreCompleted;

        //public event getTemplateCompletedEventHandler getTemplateCompleted;

        [GeneratedCode("System.Web.Services", "4.0.30319.34209")]
        public delegate void LoginCompletedEventHandler(object sender, LoginCompletedEventArgs e);


        public event LoginCompletedEventHandler LoginCompleted;

        //public event saveStudentAnwserCompletedEventHandler saveStudentAnwserCompleted;

        //public event ShowStuPaperCompletedEventHandler ShowStuPaperCompleted;

        //public event UpdateScoreCompletedEventHandler UpdateScoreCompleted;

        //public event updateStudentStatusCompletedEventHandler updateStudentStatusCompleted;

        public StudentService()
        {
            this.Url = "http://202.118.26.80/WebService/StudentService.asmx";
            if (this.IsLocalFileSystemWebService(this.Url))
            {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else
            {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        //public void CancelAsync(object userState)
        //{
        //    base.CancelAsync(userState);
        //}

        //[SoapDocumentMethod("http://tempuri.org/ChangePwd", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        //public bool ChangePwd(ClientStudentModel studentModel)
        //{
        //    return (bool) base.Invoke("ChangePwd", new object[] { studentModel })[0];
        //}

        //public void ChangePwdAsync(ClientStudentModel studentModel)
        //{
        //    this.ChangePwdAsync(studentModel, null);
        //}

        //public void ChangePwdAsync(ClientStudentModel studentModel, object userState)
        //{
        //    if (this.ChangePwdOperationCompleted == null)
        //    {
        //        this.ChangePwdOperationCompleted = new SendOrPostCallback(this.OnChangePwdOperationCompleted);
        //    }
        //    base.InvokeAsync("ChangePwd", new object[] { studentModel }, this.ChangePwdOperationCompleted, userState);
        //}

        //[SoapDocumentMethod("http://tempuri.org/getServerTime", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        //public DateTime getServerTime()
        //{
        //    return (DateTime) base.Invoke("getServerTime", new object[0])[0];
        //}

        //public void getServerTimeAsync()
        //{
        //    this.getServerTimeAsync(null);
        //}

        //public void getServerTimeAsync(object userState)
        //{
        //    if (this.getServerTimeOperationCompleted == null)
        //    {
        //        this.getServerTimeOperationCompleted = new SendOrPostCallback(this.OngetServerTimeOperationCompleted);
        //    }
        //    base.InvokeAsync("getServerTime", new object[0], this.getServerTimeOperationCompleted, userState);
        //}

        //[SoapDocumentMethod("http://tempuri.org/getStuScore", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        ////public Grade[] getStuScore(string key, string stuNumber, string dataSource)
        ////{
        ////    return (Grade[]) base.Invoke("getStuScore", new object[] { key, stuNumber, dataSource })[0];
        ////}

        //public void getStuScoreAsync(string key, string stuNumber, string dataSource)
        //{
        //    this.getStuScoreAsync(key, stuNumber, dataSource, null);
        //}

        //public void getStuScoreAsync(string key, string stuNumber, string dataSource, object userState)
        //{
        //    if (this.getStuScoreOperationCompleted == null)
        //    {
        //        this.getStuScoreOperationCompleted = new SendOrPostCallback(this.OngetStuScoreOperationCompleted);
        //    }
        //    base.InvokeAsync("getStuScore", new object[] { key, stuNumber, dataSource }, this.getStuScoreOperationCompleted, userState);
        //}

        [SoapDocumentMethod("http://tempuri.org/getTemplate", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        public DataSet getTemplate(string key, string studentNumber, string type, string dataSource)
        {
            return (DataSet)base.Invoke("getTemplate", new object[] { key, studentNumber, type, dataSource })[0];
        }

        //public void getTemplateAsync(string key, string studentNumber, string type, string dataSource)
        //{
        //    this.getTemplateAsync(key, studentNumber, type, dataSource, null);
        //}

        //public void getTemplateAsync(string key, string studentNumber, string type, string dataSource, object userState)
        //{
        //    if (this.getTemplateOperationCompleted == null)
        //    {
        //        this.getTemplateOperationCompleted = new SendOrPostCallback(this.OngetTemplateOperationCompleted);
        //    }
        //    base.InvokeAsync("getTemplate", new object[] { key, studentNumber, type, dataSource }, this.getTemplateOperationCompleted, userState);
        //}

        private bool IsLocalFileSystemWebService(string url)
        {
            if ((url == null) || (url == string.Empty))
            {
                return false;
            }
            Uri uri = new Uri(url);
            return ((uri.Port >= 0x400) && (string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0));
        }

        [SoapDocumentMethod("http://tempuri.org/Login", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        public ClientStudentModel Login(ClientStudentModel selectModel)
        {
            return (ClientStudentModel) base.Invoke("Login", new object[] { selectModel })[0];
        }

        public void LoginAsync(ClientStudentModel selectModel)
        {
            this.LoginAsync(selectModel, null);
        }

        public void LoginAsync(ClientStudentModel selectModel, object userState)
        {
            if (this.LoginOperationCompleted == null)
            {
                this.LoginOperationCompleted = new SendOrPostCallback(this.OnLoginOperationCompleted);
            }
            base.InvokeAsync("Login", new object[] { selectModel }, this.LoginOperationCompleted, userState);
        }

        //private void OnChangePwdOperationCompleted(object arg)
        //{
        //    if (this.ChangePwdCompleted != null)
        //    {
        //        InvokeCompletedEventArgs args = (InvokeCompletedEventArgs) arg;
        //        this.ChangePwdCompleted(this, new ChangePwdCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
        //    }
        //}

        //private void OngetServerTimeOperationCompleted(object arg)
        //{
        //    if (this.getServerTimeCompleted != null)
        //    {
        //        InvokeCompletedEventArgs args = (InvokeCompletedEventArgs) arg;
        //        this.getServerTimeCompleted(this, new getServerTimeCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
        //    }
        //}

        //private void OngetStuScoreOperationCompleted(object arg)
        //{
        //    if (this.getStuScoreCompleted != null)
        //    {
        //        InvokeCompletedEventArgs args = (InvokeCompletedEventArgs) arg;
        //        this.getStuScoreCompleted(this, new getStuScoreCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
        //    }
        //}

        //private void OngetTemplateOperationCompleted(object arg)
        //{
        //    if (this.getTemplateCompleted != null)
        //    {
        //        InvokeCompletedEventArgs args = (InvokeCompletedEventArgs) arg;
        //        this.getTemplateCompleted(this, new getTemplateCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
        //    }
        //}

        private void OnLoginOperationCompleted(object arg)
        {
            if (this.LoginCompleted != null)
            {
                InvokeCompletedEventArgs args = (InvokeCompletedEventArgs)arg;
                this.LoginCompleted(this, new LoginCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
            }
        }

        //private void OnsaveStudentAnwserOperationCompleted(object arg)
        //{
        //    if (this.saveStudentAnwserCompleted != null)
        //    {
        //        InvokeCompletedEventArgs args = (InvokeCompletedEventArgs) arg;
        //        this.saveStudentAnwserCompleted(this, new saveStudentAnwserCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
        //    }
        //}

        //private void OnShowStuPaperOperationCompleted(object arg)
        //{
        //    if (this.ShowStuPaperCompleted != null)
        //    {
        //        InvokeCompletedEventArgs args = (InvokeCompletedEventArgs) arg;
        //        this.ShowStuPaperCompleted(this, new ShowStuPaperCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
        //    }
        //}

        //private void OnUpdateScoreOperationCompleted(object arg)
        //{
        //    if (this.UpdateScoreCompleted != null)
        //    {
        //        InvokeCompletedEventArgs args = (InvokeCompletedEventArgs) arg;
        //        this.UpdateScoreCompleted(this, new UpdateScoreCompletedEventArgs(args.Results, args.Error, args.Cancelled, args.UserState));
        //    }
        //}

        //private void OnupdateStudentStatusOperationCompleted(object arg)
        //{
        //    if (this.updateStudentStatusCompleted != null)
        //    {
        //        InvokeCompletedEventArgs args = (InvokeCompletedEventArgs) arg;
        //        this.updateStudentStatusCompleted(this, new AsyncCompletedEventArgs(args.Error, args.Cancelled, args.UserState));
        //    }
        //}

        //[SoapDocumentMethod("http://tempuri.org/saveStudentAnwser", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        //public bool saveStudentAnwser(bool isFirst, ClientExamModel model)
        //{
        //    return (bool) base.Invoke("saveStudentAnwser", new object[] { isFirst, model })[0];
        //}

        //public void saveStudentAnwserAsync(bool isFirst, ClientExamModel model)
        //{
        //    this.saveStudentAnwserAsync(isFirst, model, null);
        //}

        //public void saveStudentAnwserAsync(bool isFirst, ClientExamModel model, object userState)
        //{
        //    if (this.saveStudentAnwserOperationCompleted == null)
        //    {
        //        this.saveStudentAnwserOperationCompleted = new SendOrPostCallback(this.OnsaveStudentAnwserOperationCompleted);
        //    }
        //    base.InvokeAsync("saveStudentAnwser", new object[] { isFirst, model }, this.saveStudentAnwserOperationCompleted, userState);
        //}

        //[SoapDocumentMethod("http://tempuri.org/ShowStuPaper", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        //public DataSet ShowStuPaper(string key, string stuNumber, string dataSource)
        //{
        //    return (DataSet) base.Invoke("ShowStuPaper", new object[] { key, stuNumber, dataSource })[0];
        //}

        //public void ShowStuPaperAsync(string key, string stuNumber, string dataSource)
        //{
        //    this.ShowStuPaperAsync(key, stuNumber, dataSource, null);
        //}

        //public void ShowStuPaperAsync(string key, string stuNumber, string dataSource, object userState)
        //{
        //    if (this.ShowStuPaperOperationCompleted == null)
        //    {
        //        this.ShowStuPaperOperationCompleted = new SendOrPostCallback(this.OnShowStuPaperOperationCompleted);
        //    }
        //    base.InvokeAsync("ShowStuPaper", new object[] { key, stuNumber, dataSource }, this.ShowStuPaperOperationCompleted, userState);
        //}

        //[SoapDocumentMethod("http://tempuri.org/UpdateScore", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        //public int UpdateScore(ClientExamModel model)
        //{
        //    return (int) base.Invoke("UpdateScore", new object[] { model })[0];
        //}

        //public void UpdateScoreAsync(ClientExamModel model)
        //{
        //    this.UpdateScoreAsync(model, null);
        //}

        //public void UpdateScoreAsync(ClientExamModel model, object userState)
        //{
        //    if (this.UpdateScoreOperationCompleted == null)
        //    {
        //        this.UpdateScoreOperationCompleted = new SendOrPostCallback(this.OnUpdateScoreOperationCompleted);
        //    }
        //    base.InvokeAsync("UpdateScore", new object[] { model }, this.UpdateScoreOperationCompleted, userState);
        //}

        //[SoapDocumentMethod("http://tempuri.org/updateStudentStatus", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
        //public void updateStudentStatus(string key, string stuNumber, string dataSource)
        //{
        //    base.Invoke("updateStudentStatus", new object[] { key, stuNumber, dataSource });
        //}

        //public void updateStudentStatusAsync(string key, string stuNumber, string dataSource)
        //{
        //    this.updateStudentStatusAsync(key, stuNumber, dataSource, null);
        //}

        //public void updateStudentStatusAsync(string key, string stuNumber, string dataSource, object userState)
        //{
        //    if (this.updateStudentStatusOperationCompleted == null)
        //    {
        //        this.updateStudentStatusOperationCompleted = new SendOrPostCallback(this.OnupdateStudentStatusOperationCompleted);
        //    }
        //    base.InvokeAsync("updateStudentStatus", new object[] { key, stuNumber, dataSource }, this.updateStudentStatusOperationCompleted, userState);
        //}

        public string Url
        {
            get
            {
                return base.Url;
            }
            set
            {
                if ((this.IsLocalFileSystemWebService(base.Url) && !this.useDefaultCredentialsSetExplicitly) && !this.IsLocalFileSystemWebService(value))
                {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }

        public bool UseDefaultCredentials
        {
            get
            {
                return base.UseDefaultCredentials;
            }
            set
            {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
    }
}

