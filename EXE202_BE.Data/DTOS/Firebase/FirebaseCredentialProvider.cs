using Google.Apis.Auth.OAuth2;

public class FirebaseCredentialProvider
{
    public GoogleCredential MessagingCredential { get; set; }
    public GoogleCredential StorageCredential { get; set; }
}