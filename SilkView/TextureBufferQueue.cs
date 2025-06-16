using System.Collections.Concurrent;

namespace SilkView
{
    public class TextureBufferQueue
    {
        private readonly BlockingCollection<byte[]> _queue = new();

        public void Enqueue(byte[] data)
        {
            _queue.Add(data);
        }

        public byte[] Dequeue()
        {
            return _queue.Take();
        }

        public byte[] TryDequeue()
        {
            if (_queue.TryTake(out var result))
                return result;
            return null;
        }
    }
}
