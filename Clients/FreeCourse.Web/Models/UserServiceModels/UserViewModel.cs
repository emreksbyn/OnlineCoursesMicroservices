namespace FreeCourse.Web.Models.UserServiceModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string City { get; set; }

        public IEnumerable<string> GetUserProperties()
        {
            yield return Username;
            yield return Email;
            yield return City;
        }
    }
}