namespace CloneWhatsBE.Auth;

public class JwtSettingsOptions
{
    public static string SectionName = "JwtSettings";
    public string Secret { get; set; } = "Insert your token here";
}
