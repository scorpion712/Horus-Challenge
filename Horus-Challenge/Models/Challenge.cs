namespace Horus_Challenge.Models;

public class Challenge
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CurrentPoints { get; set; }
    public int TotalPoints { get; set; }
    public double CompletedPercentage => (CurrentPoints * 100) / TotalPoints;
    public double Progress => CompletedPercentage / 100;
    public string Status => $"{CurrentPoints}/{TotalPoints}";
}