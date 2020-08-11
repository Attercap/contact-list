namespace ContactList.Business
{
    /// <summary>
    /// Base properties for dto objects (used primarily for success/error messaging)
    /// </summary>
    public class DtoReturnBase
    {
        public bool HasErrors { get; set; }
        public string DtoMessage { get; set; }

        public DtoReturnBase(bool hasErrors, string dtoMessage)
        {
            HasErrors = hasErrors;
            DtoMessage = dtoMessage;
        }

        public DtoReturnBase()
        {
            HasErrors = true;
            DtoMessage = "An unknown error occured.";
        }
    }
}
