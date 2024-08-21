using YPP.MH.DigitalAssetManagement.Action;

namespace YPP.MH.DigitalAssetManagement
{
    public class Dam
    {
        private readonly HttpClient _httpClient;
        private readonly FileService _fileService;
        private readonly string baseUrl = "http://10.6.20.31:8080/";

        private Account _account;

        public Dam(Account account) : this()
        {
            _account = account;
        }

        public Dam()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
            _fileService = new FileService(_httpClient);
        }

        public async Task<string> UploadAsset(ImageUploadParam param)
        {
            return await _fileService.UploadAsync(param, _account);
        }

     
        public async Task<object> GetAsset(string tenantId, string path, Dictionary<string, string>? options = null)
        {
            return await _fileService.GetImageFileAsync(tenantId, path, options);
        }
    }


}
