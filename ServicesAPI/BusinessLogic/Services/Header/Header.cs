using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.Models.Headers;

namespace ServicesAPI.BusinessLogic.Services.Header
{
    public class Header : IHeader
    {
        public Task<Headers> AddHeaderAsync(Headers header)
        {
            throw new NotImplementedException();
        }

        public Task<Headers> DeletHeaderAsync(int idHead)
        {
            throw new NotImplementedException();
        }

        public Task<Headers> GetHeaderAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Headers> UpdateDescrHeaderAsync(Headers header)
        {
            throw new NotImplementedException();
        }
    }
}
