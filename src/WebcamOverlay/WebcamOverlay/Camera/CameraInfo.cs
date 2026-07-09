namespace WebcamOverlay.Camera
{
    public sealed class CameraInfo
    {
        public CameraInfo(string name, string monikerString)
        {
            Name = name;
            MonikerString = monikerString;
        }

        public string Name { get; }

        public string MonikerString { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}