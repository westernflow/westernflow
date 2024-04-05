namespace Data.Enums;

public enum BreadthCategory
{
    Undefined,
    A,
    B,
    C
}

public class BreadthCategoryUtils
{
    public static BreadthCategory GetBreadthCategory(string category)
    {
        return category switch
        {
            "A" => BreadthCategory.A,
            "B" => BreadthCategory.B,
            "C" => BreadthCategory.C,
            _ => BreadthCategory.Undefined
        };
    }

    public static string CreateBreadthCategoryBitmap(List<BreadthCategory> categories)
    {
        List<char> bitmap = new List<char> { '0', '0', '0' };
        
        foreach (var category in categories)
        {
            switch (category)
            {
                case BreadthCategory.A:
                    bitmap[0] = '1';
                    break;
                case BreadthCategory.B:
                    bitmap[1] = '1';
                    break;
                case BreadthCategory.C:
                    bitmap[2] = '1';
                    break;
            }
        }

        return bitmap.Aggregate("", (current, c) => current + c);
    }
    
    public static HashSet<BreadthCategory> GetBreadthCategories(string bitmap)
    {
        var categories = new HashSet<BreadthCategory>();
        for (var i = 0; i < bitmap.Length; i++)
        {
            if (bitmap[i] == '1')
            {
                categories.Add((BreadthCategory) i);
            }
        }

        return categories;
    }
}