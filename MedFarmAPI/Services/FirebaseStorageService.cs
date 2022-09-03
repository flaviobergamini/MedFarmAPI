using Firebase.Auth;
using Firebase.Storage;

namespace MedFarmAPI.Services
{
    public class FirebaseStorageService
    {
        private static string ApiKey = Configuration.Firebase.ApiKey;
        private static string Bucket = Configuration.Firebase.Bucket;
        private static string AuthEmail = Configuration.Firebase.AuthEmail;
        private static string AuthPassword = Configuration.Firebase.AuthPassword;
        public async Task<string> SendImage(IFormFile formFile)
        {
            try
            {
                var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
                var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

                // Constructr FirebaseStorage, path to where you want to upload the file and Put it there
                var task = new FirebaseStorage(
                    Bucket,

                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true,
                })
                    .Child("dataOrdersClient")
                    .Child($"{Guid.NewGuid()}.png")
                    .PutAsync(formFile.OpenReadStream());

                // Track progress of the upload
                task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

                // await the task to wait until upload completes and get the download url
                var downloadUrl = await task;

                return downloadUrl;
            }
            catch
            {
                return null;
            }
        }
    }
}
