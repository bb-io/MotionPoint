namespace Apps.MotionPoint.Models.Dtos;

public class PaginationDto<T>
{
    public List<T> Content { get; set; } = new();

    public int Size { get; set; }

    public int NumberOfElements { get; set; }

    public int TotalElements { get; set; }

    public int TotalPages { get; set; }

    public bool Last { get; set; }
}
