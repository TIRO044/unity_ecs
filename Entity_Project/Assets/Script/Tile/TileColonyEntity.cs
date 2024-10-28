namespace Assets.Script.Tile
{
    public class TileColonyEntity
    {
        public float EntitySize;

        public int Width;
        public int Height;

        public void Init()
        {
            var colonyHeight = EntitySize * Height;
            var colonyWidth = EntitySize * Width;

            var entityStartPositionX = -(colonyWidth - EntitySize) / 2;
            var entityStartPositionY = -(colonyHeight - EntitySize) / 2;

            for (int h = 0; h < Height; h++)
            {
                var height = entityStartPositionY + h * EntitySize;
                for (int w = 0; w < Width; w++)
                {
                    var width = entityStartPositionX + w * EntitySize;
                    
                    // create instance
                }        
            }
        }
    }
}