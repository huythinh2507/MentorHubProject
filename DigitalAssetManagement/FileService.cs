
using YPP.MH.DigitalAssetManagement.Action;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;

namespace YPP.MH.DigitalAssetManagement
{
    public class FileService
    {
        private readonly HttpClient _httpClient;
        private readonly string uploadApiUrl = "api/upload-assets";
        private readonly string getApiUrl = "api/get-assets";

        public FileService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> UploadAsync(ImageUploadParam fileUpload, Account account)
        {
            if (fileUpload.File == null || fileUpload.File.Length == 0)
            {
                throw new ArgumentException("File cannot be null or empty.", nameof(fileUpload.File));
            }

            using (var content = new MultipartFormDataContent())
            {
                var fileContent = new StreamContent(fileUpload.File.OpenReadStream());
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = nameof(fileUpload.File),
                    FileName = fileUpload.File.FileName
                };

                content.Add(fileContent);

                var metadata = fileUpload.ToParamsDictionary();

                foreach (var kvp in metadata)
                {
                    content.Add(new StringContent(kvp.Value.ToString()), $"metadata[{kvp.Key}]");
                }

                // Add headers
                _httpClient.DefaultRequestHeaders.Add("X-API-Key", account.ApiKey);
                _httpClient.DefaultRequestHeaders.Add("X-Secret-Key", account.SecretKey);
                _httpClient.DefaultRequestHeaders.Add("X-Tenant-ID", account.TenantId);

                // Send the POST request
                var response = await _httpClient.PostAsync(uploadApiUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to upload asset: {response.ReasonPhrase}");
                }

                var result = await response.Content.ReadAsStringAsync();


                return buildSecureUrl(fileUpload, _httpClient.BaseAddress.ToString(), result, account);
            }
        }

        private string buildSecureUrl(ImageUploadParam fileUpload, string baseUrl, string filePath, Account account)
        {
            return baseUrl + getApiUrl + "/"+ account.TenantId + "/"+ fileUpload.ResourceType.ToString().ToLower() +"/" + filePath;
        }

        public async Task<byte[]> GetImageFileAsync(string tenantId, string path, Dictionary<string, string>? options = null)
        {
            var uriBuilder = new UriBuilder($"{_httpClient.BaseAddress}{getApiUrl}/{tenantId}/image/{path}");
            if (options != null && options.Any())
            {
                var query = await new FormUrlEncodedContent(options).ReadAsStringAsync();
                uriBuilder.Query = query;
            }
            var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var imageFileBytes = await response.Content.ReadAsByteArrayAsync();
            return imageFileBytes;
        }
    }

}
