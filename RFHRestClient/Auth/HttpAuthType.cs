namespace RFHRestClient.Auth
{
    public abstract class HttpAuthType
    {
        protected string authorization;
        public string Authorization { get { return authorization; } }

        public virtual void Authenticate()
        {

        }


    }
}
