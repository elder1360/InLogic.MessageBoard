using MediatR;

namespace Inlogic.MessageBoard.Console.CommandQuery.Queries;
internal class DisplayWallQueryHandler(IInMemoryStateStore inMemoryStateStore) : IRequestHandler<DisplayWallQuery>
{
  private readonly IInMemoryStateStore _inMemoryStateStore = inMemoryStateStore;

  public async Task Handle(DisplayWallQuery request, CancellationToken cancellationToken)
  {
    var userWall = _inMemoryStateStore.GetUserWall(request.UserName);
    var messages = userWall.Values.SelectMany(x => x).OrderByDescending(x => x.Timestamp);
    foreach (var message in messages)
    {
      var projectName = userWall.Where(w => w.Value.Contains(message)).Select(w => w.Key).FirstOrDefault();
      System.Console.WriteLine($"{projectName} - {message.UserName}: {message.Content} ( {Math.Round((DateTime.Now - message.Timestamp).TotalMinutes, 0)} Minutes ago )");
    }
    await Task.CompletedTask;
  }
}
