
namespace DTOs {
    public class FbAccessToken {
        public required string accessToken;
    }
    public class PictureData {
        public int height;            
        public int width;
        public bool is_silhoutte;
        public required string url;
    }

    public class FbPicture {
        public required PictureData data;
    }

    public class FbUserData {
        public required string name { get; set; }
        public required string email { get; set; }
        public required string id { get; set; }
        public required FbPicture picture;

        public static implicit operator FbUserData(string v)
        {
            throw new NotImplementedException();
        }
    }
}