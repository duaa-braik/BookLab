namespace BookLab.Application.Dtos;

public class ErrorResponseDto
{
    public List<ErrorDto> Errors { get; set; }
}

public class ErrorDto
{
    public string Message { get; set; }

    public string Code { get; set; }
}
