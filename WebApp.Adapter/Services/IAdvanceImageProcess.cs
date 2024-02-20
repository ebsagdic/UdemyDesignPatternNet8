using System.Drawing;

namespace WebAppAdapter.Services
{
    public interface IAdvanceImageProcess
    {
        void AddWaterMarkImage(Stream stream, string text, string filePath, Color color, Color outlineColor);
        
    }
}
