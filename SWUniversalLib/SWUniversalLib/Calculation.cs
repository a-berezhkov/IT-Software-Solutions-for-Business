namespace SWUniversalLib
{
    public static class Calculation
    {
        public static int getQuantityForProduct(int productType, int materialType, int count, float width, float length)
        {
            if (count <= 0 || width <= 0 || length <= 0)
                return -1;

            float productModifier;
            switch (productType)
            {
                case 1:
                    productModifier = 1.1F;
                    break;
                case 2:
                    productModifier = 2.5F;
                    break;
                case 3:
                    productModifier = 8.43F;
                    break;
                default:
                    return -1;
            }

            float materialModifier;
            switch (materialType)
            {
                case 1:
                    materialModifier = 1 - 0.3F / 100;
                    break;
                case 2:
                    materialModifier = 1 - 0.12F / 100;
                    break;
                default:
                    return -1;
            }

            return (int)Math.Ceiling(count * width * length * productModifier / materialModifier);
        }
    }
}