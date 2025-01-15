using System;
using System.Threading.Tasks;

  namespace WebsiteProjectMobile.Helpers
{
    public static class Helpers
    {
        public async static Task<bool> HandleRequest(this Task serviceMethod)
        {
            try
            {
                await serviceMethod;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
