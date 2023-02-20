using System;
using Project.Scripts.Core.ECS.Component;
using Project.Scripts.Core.ECS.Entity;

namespace Project.Scripts.Area.Components.Logic
{
    public class FieldComponent : IComponent
    {
        public readonly int MinRelativeCenterPositionX;
        private readonly int MaxRelativeCenterPositionX;
        public readonly int MinRelativeCenterPositionY;
        private readonly int MaxRelativeCenterPositionY;

        public readonly int MaxPositionX;
        public readonly int MaxPositionY;

        public IEntity[,] PositionsWithCard;


        public FieldComponent(int maxRelativeCenterPositionX, int maxRelativeCenterPositionY,
            int minRelativeCenterPositionX, int minRelativeCenterPositionY)
        {
            MaxRelativeCenterPositionX = maxRelativeCenterPositionX;
            MaxRelativeCenterPositionY = maxRelativeCenterPositionY;
            MinRelativeCenterPositionX = minRelativeCenterPositionX;
            MinRelativeCenterPositionY = minRelativeCenterPositionY;

            int centerPosition = 1;
            MaxPositionX = Math.Abs(MinRelativeCenterPositionX) + MaxRelativeCenterPositionX;
            MaxPositionY = Math.Abs(MinRelativeCenterPositionY) + MaxRelativeCenterPositionY;

            PositionsWithCard = new Entity[MaxPositionX + centerPosition,
                MaxPositionY + centerPosition];

            for (int x = 0; x <= MaxPositionX; x++)
            {
                for (int y = 0; y <= MaxPositionY; y++)
                {
                    PositionsWithCard[x, y] = null;
                }
            }
        }
    }
}