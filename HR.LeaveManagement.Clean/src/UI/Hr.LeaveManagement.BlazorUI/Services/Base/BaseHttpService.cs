namespace Hr.LeaveManagement.BlazorUI.Services.Base
{
    public class BaseHttpService
    {
        protected IClient _client { get; set; }

        public BaseHttpService(IClient client)
        {
            _client = client;
            
        }


        protected Response<Guid> ConvertApiExceptions<Guid>(ApiException ex)
        {
            Console.WriteLine("ConvertApiExceptions method called"); // add this line

            if (ex.StatusCode == 400)
            {
               // Console.WriteLine("400 error: Invalid data was submitted"); // add this line
                return new Response<Guid>() { Message = "Invalid data was submitted", ValidationErrors = ex.Response, Success = false };
            }
            else if (ex.StatusCode == 404)
            {
              //  Console.WriteLine("404 error: The record was not found."); // add this line
                return new Response<Guid>() { Message = "The record was not found.", Success = false };
            }
            else
            {
               // Console.WriteLine("Unknown error occurred"); // add this line
                return new Response<Guid>() { Message = "Something went wrong, please try again later.", Success = false };
            }
        }
    }
}
