namespace JotterService.Domain;
public class Password
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Title { get; set; }
    // TODO: better type for this than a primitive?
    public string? Url { get; set; }
    public string? Username { get; set; }
    public string? Description { get; set; }
    public string? CustomerNumber { get; set; }
    public string Secret => "**********";

}