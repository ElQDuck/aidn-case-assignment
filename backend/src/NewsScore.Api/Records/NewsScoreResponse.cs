namespace NewsScore.Api.Records;

public class NewsScoreResponse(int score)
{
    public int Score { get; init; } = score;
}