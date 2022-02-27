namespace JotterService.Domain;

public class CypherText 
{
    public CypherText(string text, byte[] iv)
    {
        Text = text;   
        Iv = iv;
    }
    public string Text { get; set; }
    public byte[] Iv { get; set; }
}