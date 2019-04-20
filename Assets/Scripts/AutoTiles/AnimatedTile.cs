namespace SealPoint.AutoTiles
{
#if UNITY_EDITOR
    using UnityEditor;
#endif
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Tilemaps;

    public class AnimatedTile : Tile {
        public float Speed = 1;
        public float StartTimeea;
        public Sprite[] Sprites;

        public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData) {
            tileAnimationData.animatedSprites = Sprites;
            tileAnimationData.animationSpeed = Speed;
            tileAnimationData.animationStartTime = StartTime; 
            return true;
        }
        
#if UNITY_EDITOR
        [MenuItem("Assets/Create/Tiles/AnimatedTile")]
        protected static void CreateTileAsset()
        {
            var className = typeof(AnimatedTile).Name;
            var path = EditorUtility.SaveFilePanelInProject("Save " + className, "New " + className, "Asset",
                "Save " + className, "Assets");
            if (path == "")
            {
                return;
            }

            AssetDatabase.CreateAsset(CreateInstance<AnimatedTile>(), path);
        }
#endif
    }
}