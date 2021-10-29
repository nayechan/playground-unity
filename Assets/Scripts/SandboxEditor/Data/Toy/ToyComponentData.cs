using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameEditor.Data
{
    public abstract class ToyComponentData
    {
        private static HashSet<Type> supportedType = 
            new HashSet<Type>()
            {
                typeof(BoxCollider2D),
                typeof(CircleCollider2D),
                typeof(Rigidbody2D), 
                typeof(Transform)
            };
        
        public Component AddDataAppliedToyComponent(GameObject toy)
        {
            var component = AddMatchedTypeToyComponent(toy);
            ApplyDataToToyComponent(component);
            return component;
        }

        public abstract Component AddMatchedTypeToyComponent(GameObject toy);

        public abstract void ApplyDataToToyComponent(Component comp);

        public static ToyComponentData GetToyComponentDataFromComponent(Component component)
        {
            var newToyComponentData = CreateMatchedComponentData(component);
            return newToyComponentData.UpdateByToyComponent(component);
        }
        
        private static ToyComponentData CreateMatchedComponentData(Component comp)
        {
            switch (comp)
            {
                case BoxCollider2D box2d:
                    return new BoxCollider2DData(comp);
                case CircleCollider2D cir2d:
                    return new CircleCollider2DData(comp);
                case Rigidbody2D rb2d:
                    return new Rigidbody2DData(comp);
                case Transform tf:
                    return new TransformData(comp);
                default:
                    return null;
            }
        }

        public abstract ToyComponentData UpdateByToyComponent(Component comp);

        public abstract bool IsMatchedType(Component component);
        public static bool IsSupportedType(Component component)
        {
            return supportedType.Contains(component.GetType());
        }


    }
}