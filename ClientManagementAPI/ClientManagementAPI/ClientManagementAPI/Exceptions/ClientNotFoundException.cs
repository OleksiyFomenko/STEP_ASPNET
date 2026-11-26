namespace ClientManagementAPI.Exceptions
{
    public class ClientNotFoundException : Exception
    {
        public ClientNotFoundException(int id)
            : base($"Client with ID {id} was not found.")
        {
        }
    }

}
