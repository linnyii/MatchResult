using System.Formats.Asn1;
using System.Runtime.CompilerServices;
using FluentAssertions;
using NSubstitute;

namespace TestProject1;

public class Tests
{
    private IRepository _repo = null!;
    private MatchService _service = null!;

    [SetUp]
    public void Setup()
    {
        _repo = Substitute.For<IRepository>();
        _service = new MatchService(_repo);
    }

    [Test]
    public void HomeGoal_And_FirstHalf()
    {
        _repo.GetLastResult().Returns(new GameMatch(new Score("")));

        var result = _service.UpdateResult(ActionType.HomeGoal);
        
        result.Should().BeEquivalentTo("1:0 (First Half)");
    }
}

public class GameMatch
{
    public Score Score { get; }
    public GameMatch(Score score)
    {
        Score = score;
    }

}

public class Score
{
    public string Result { get; }

    public Score(string result)
    {
        Result = result;
    }
}

public enum ActionType
{
    HomeGoal
}

public class MatchService(IRepository repo)
{
    public string UpdateResult(ActionType homeGoal)
    {
        var lastResult = repo.GetLastResult();
        return "";
    }
}

public interface IRepository
{
    public GameMatch GetLastResult();
}